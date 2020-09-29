using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Eventually make some parent classes or interfaces for other enemy types
public class GruntController : MonoBehaviour
{
	public enum GruntState
	{
		Idle,
		Walking,
		AttackingBarrier,
		AttackingBase
	}

	public GruntState CurrentState;

	[Header("Tweakable Values")]
	public int HitPoints = 3;
	public float Speed = 1.0f;
	public float AttackDelay = 1.0f;
	public int AttackStrength = 2; //TODO: split this into Player and Barrier, for specialist enemies

	[HideInInspector]
	public Barrier BarrierToAttack;

	[Header("References")]
	[SerializeField] private ParticleSystem _deathBloodSplash;
	[SerializeField] private Rigidbody2D _rigidBody;
	[SerializeField] private Collider2D _collider;

	private SimplePF2D.Path _path;
	private Vector3 _nextPoint;
	private bool _isStationary;
	private Vector3 _endGoalPosition;
	private SimplePathFinding2D _pathFinding2D;

	private bool _gameIsOver;
	private bool _isDead;
	private float _deathAnimationRuntime = 0.5f;

	private void Start()
	{
		_nextPoint = Vector3.zero;
	}

	public void Initialize(SimplePathFinding2D pathFinding2D, Vector3 endGoalPosition)
	{
		//TODO: Set a target barrier!
		_pathFinding2D = pathFinding2D;
		_path = new SimplePF2D.Path(_pathFinding2D);
		_endGoalPosition = endGoalPosition;

		CreateNewPathToEndGoal();

		BarrierToAttack = GameController.instance.MainBarrier;

		Debug.LogFormat("[GruntController] - New grunt {0}: startPosition {1} endPosition {2} going for {3}",
			gameObject.name, transform.position, endGoalPosition, BarrierToAttack.gameObject.name);

		CurrentState = GruntState.Walking;
	}

	private void CreateNewPathToEndGoal()
	{
		_path.CreatePath(transform.position, _endGoalPosition);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//TODO hit by projectile?
	}

	private void Update()
	{
		if(_gameIsOver || _isDead)
		{
			return;
		}

		if(CurrentState == GruntState.AttackingBarrier)
		{
			//TODO: Keep attacking the barrier!
			StopMoving();
		}
		else if(transform.position.y >= GameController.GAMEOVER_Y_POSITION)
		{
			//TODO: Zombies win!!
			SetToIdleState();
			GameController.instance.TriggerGameOver();
			_gameIsOver = true;
		}

		else if(BarrierToAttack != null && BarrierToAttack.HitPoints > 0 
			&& transform.position.y >= GameController.ZOMBIES_AT_BARRIER_Y_POS)
		{
			//Start attacking the barrier!!
			CurrentState = GruntState.AttackingBarrier;
			StopMoving();
			StartCoroutine(StartAttackingBarrier());
			Debug.LogFormat("[Grunt] - {0} is now Attacking {1}! ", gameObject.name, BarrierToAttack.gameObject.name);
		}

		else if(CurrentState != GruntState.AttackingBase && transform.position.y >= GameController.ZOMBIES_AT_BARRIER_Y_POS)
		{
			//Start attacking the base!!
			CurrentState = GruntState.AttackingBase;
			CreateNewPathToEndGoal();
		}

		else if(_path.IsGenerated())
		{
			if(_isStationary)
			{
				if(_path.GetNextPoint(ref _nextPoint))
				{
					//Get the next point in the path as a world position
					_rigidBody.velocity = _nextPoint - transform.position;
					_rigidBody.velocity = _rigidBody.velocity.normalized;
					_rigidBody.velocity *= Speed;
					_isStationary = false;
				}
				else
				{
					//End of the path!
					Debug.Log("[Grunt] - End of path! " + gameObject.name);
					SetToIdleState();
				}
			}
			else
			{
				Vector3 delta = _nextPoint - transform.position;
				if(delta.magnitude <= 0.2f)
				{
					StopMoving();
				}
			}
		}
		else
		{
			//No path! TODO: why are some grunts just not moving??
			//Debug.Log("[Grunt] - No path! " + gameObject.name);
			SetToIdleState();
		}
	}

	private void StopMoving()
	{
		_rigidBody.velocity = Vector3.zero;
		_isStationary = true;
	}

	private void SetToIdleState()
	{
		StopMoving();
		CurrentState = GruntState.Idle;
	}

	public void TakeDamage(int damage)
	{
		//TODO: show some animation, maybe a colour flash
		HitPoints -= damage;

		if(HitPoints <= 0)
		{
			OnDeath();
		}
	}

	private IEnumerator StartAttackingBarrier()
	{
	//    //TODO: If theBarrier is not active, start moving forward again!
	//    if(!BarrierToAttack.isActiveAndEnabled)
	//    {
	//        return null;
	//    }

		//This should break out when the Grunt switches states. TODO: Test this with a single grunt!!
		while(CurrentState == GruntState.AttackingBarrier && BarrierToAttack.HitPoints > 0)
		{
			yield return new WaitForSeconds(AttackDelay);
			BarrierToAttack.OnHit(AttackStrength);
		}

		//Barrier is destroyed!
		CurrentState = GruntState.Walking;
	}

	private void OnDeath()
	{
		StopMoving();
		_isDead = true;
		_collider.enabled = false;

		StartCoroutine(PlayDeathAnimation());
	}

	private IEnumerator PlayDeathAnimation()
	{
		_deathBloodSplash.gameObject.SetActive(true);
		_deathBloodSplash.Play();

		yield return new WaitForSeconds(_deathAnimationRuntime);
		Destroy(this.gameObject);
	}
}
