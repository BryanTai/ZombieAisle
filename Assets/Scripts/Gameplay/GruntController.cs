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
		AttackingBase //Barrier is down, zombies in the base. Last stand for the player
	}

	public GruntState CurrentState;

	[Header("Tweakable Values")]
	public int HitPoints = 3;
	public float DefaultWalkSpeed = 1.0f;
	public float InsideBaseWalkSpeed = 0.75f;
	public float AttackDelay = 1.0f;
	public int AttackStrength = 2; //TODO: split this into Player and Barrier, for specialist enemies

	[HideInInspector]
	public Barrier BarrierToAttack;

	[Header("References")]
	[SerializeField] private ParticleSystem _deathBloodSplash;
	[SerializeField] private Rigidbody2D _rigidBody;
	[SerializeField] private Collider2D _collider;

	private GameplayController _gameController;

	private Vector3 _defaultAttackDirection = Vector3.left;

	private SimplePF2D.Path _path;
	private Vector3 _nextPoint;
	private bool _isStationary;
	private Vector3 _endGoalPosition;
	private SimplePathFinding2D _pathFinding2D; //TODO: Simplify pathfinding, just move left

	private bool _gameIsOver;
	private bool _isDead;
	private float _deathAnimationRuntime = 0.5f;

	private void Start()
	{
		_nextPoint = Vector3.zero;
	}

	public void Initialize(GameplayController gameController)
	{
		_gameController = gameController;
		CurrentState = GruntState.Walking;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//TODO hit by projectile?
	}

	private void Update()
	{
		if(_gameIsOver || _isDead)
		{
			SetToIdleState();
			return;
		}

		if(CurrentState == GruntState.AttackingBarrier)
		{
			//TODO: Keep attacking the barrier!
			StopMoving();
		}
		else if(transform.position.x <= GameplayController.GAMEOVER_X_POSITION)
		{
			// Zombies win!!
			SetToIdleState();
			GameplayController.instance.TriggerGameOver();
			_gameIsOver = true;
		}

		else if(_gameController.IsBarrierUp() 
			&& transform.position.x <= GameplayController.ZOMBIES_AT_BARRIER_X_POS)
		{
			//Start attacking the barrier!!
			CurrentState = GruntState.AttackingBarrier;
			StopMoving();
			StartCoroutine(StartAttackingBarrier());
			Debug.LogFormat("[Grunt] - {0} is now Attacking BARRIER! ", gameObject.name);
		}

		else if(!_gameController.IsBarrierUp() && CurrentState != GruntState.AttackingBase && transform.position.x <= GameplayController.ZOMBIES_AT_BARRIER_X_POS)
		{
			//Start attacking the base!!
			Debug.LogFormat("[Grunt] - {0} is now Attacking BASE! ", gameObject.name);
			CurrentState = GruntState.AttackingBase;
		}

		else if(CurrentState == GruntState.AttackingBase)
		{
			_rigidBody.velocity = _defaultAttackDirection * InsideBaseWalkSpeed;
			//TODO: Attack the PLAYER!
		}

		else if(CurrentState == GruntState.Walking)
		{
			_rigidBody.velocity = _defaultAttackDirection * DefaultWalkSpeed;
		}

		else
		{
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
		HitPoints -= damage;

		if(HitPoints <= 0)
		{
			OnDeath();
		}

		StartCoroutine(PlayDamagedAnimation());
	}

	private IEnumerator StartAttackingBarrier()
	{
	//    //TODO: If theBarrier is not active, start moving forward again!
	//    if(!BarrierToAttack.isActiveAndEnabled)
	//    {
	//        return null;
	//    }

		//This should break out when the Grunt switches states.
		while(CurrentState == GruntState.AttackingBarrier && _gameController.IsBarrierUp())
		{
			yield return new WaitForSeconds(AttackDelay);
			//TODO: This needs to be LOCKED to prevent multiple zombies calling on the same frame
			_gameController.HitBarrier(AttackStrength);
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

	private IEnumerator PlayDamagedAnimation()
	{
		//TODO: create a Hit by bullet animation! Perhaps a colour flash
		yield return new WaitForSeconds(0.01f);
	}

	private IEnumerator PlayDeathAnimation()
	{
		_deathBloodSplash.gameObject.SetActive(true);
		_deathBloodSplash.Play();

		yield return new WaitForSeconds(_deathAnimationRuntime);
		Destroy(this.gameObject); //TODO: make sure nothing's referencing this...
	}
}
