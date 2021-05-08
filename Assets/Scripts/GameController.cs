using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public static GameController instance;

	public const float ZOMBIES_AT_BARRIER_X_POS = 4.25f; // Zombies stop here to hit barriers
	public const float BARRIER_X_POS = 4f; //Spawn Barriers here
	public const float GAMEOVER_X_POSITION = 0f; // If a zombie reaches this point, game over
	public const string PLAYER_TAG = "Player";
	public const string SURVIVOR_TAG = "Survivor";
	public const string POINTOFINTEREST_TAG = "PointOfInterest";
	//TODO: Save button names too!

	public readonly Vector3 BEHIND_BARRIER_START_POSITION = new Vector3(0, 0, 0); //y = 20 for Dialogue testing

	public bool gamePlaying { get; private set; }

	[Header("Barriers")]
	[SerializeField] private Barrier _barrierPrefab;
	[SerializeField] private GameObject _barrierParent; //TODO: just using one large Barrier for now
	//private readonly List<float> _barrierXPositions = new List<float>() { -10.5f, -5.5f, -0.5f, 4.5f, 9.5f }; //This list must be in Ascending order! 
	//private List<Barrier> _barriers = new List<Barrier>();
	public Barrier MainBarrier { get; private set; }
	public PlayerController Player { get; private set; }

	[Header("Settings")]
	[SerializeField] private Button ControlToggleDebugButton;
	[SerializeField] private Text ControlToggleDebugText;

	[Header("Controllers")]
	public GameplayUIController UIController;
	public CameraController CameraController;

	private float _startTime, _elapsedTime;

	private ZombieSpawner _zombieSpawner;

	private TimeSpan _timePlaying;

	private CinematicState _currentCinematicState;


	private void Awake()
	{
		instance = this;

		CheckComponents();
	}

	private void CheckComponents()
	{
		if(UIController == null)
		{
			Debug.LogError("[GameController] UIController missing!");
		}

		if(CameraController == null)
		{
			Debug.LogError("[GameController] CameraController missing!");
		}

		if(_barrierPrefab == null)
		//if (BarrierPrefab == null || _barrierXPositions == null)
		{
			Debug.LogError("[GameController] Barriers missing!");
		}
	}

	private void Start()
	{
		gamePlaying = false;
		//TODO: Spawn in a new player instead of using the scene player!
		Player = FindObjectOfType<PlayerController>();
		_zombieSpawner = GetComponent<ZombieSpawner>();

		//Set up Barriers
		//int count = 0;
		//foreach(float xPos in _barrierXPositions)
		//{
		//    Barrier newBarrier = Instantiate(BarrierPrefab, BarrierParent.transform);
		//    newBarrier.transform.position = new Vector3(xPos, BARRIER_Y_POS);
		//    newBarrier.gameObject.name = "Barrier " + count.ToString();
		//    count++;
		//    _barriers.Add(newBarrier);
		//    Debug.LogFormat("Created {0} at xPos {1}", newBarrier.gameObject.name, xPos);
		//}
		_currentCinematicState = CinematicState.NONE;


		Barrier newBarrier = Instantiate(_barrierPrefab, _barrierParent.transform);
		newBarrier.transform.position = new Vector3(BARRIER_X_POS, 0);
		MainBarrier = newBarrier;
		Debug.LogFormat("Created {0} at xPos {1}", newBarrier.gameObject.name, newBarrier.transform.position.x);

		//Debug Controls
		if (ControlToggleDebugButton != null)
		{
			ControlToggleDebugButton.onClick.AddListener(OnToggleControlsPressed);
		}

		Player.SetPosition(BEHIND_BARRIER_START_POSITION);

//TODO: Trigger the start through an Interaction!
		StartCoroutine(DelayedStart(3.0f));
	}

	private IEnumerator DelayedStart(float delaySeconds)
	{
		UIController.ShowAnnouncement("Defend the store!", delaySeconds);
		yield return new WaitForSeconds(delaySeconds);
		BeginGame();
	}

	private void Update()
	{
		if(gamePlaying)
		{
			_elapsedTime = Time.time - _startTime;
			_timePlaying = TimeSpan.FromSeconds(_elapsedTime);

			//string timeToDisplay = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
		}
	}

	private void BeginGame()
	{
		gamePlaying = true;
		_startTime = Time.time;

		_zombieSpawner.ToggleSpawner(true);
	}

	//Player has lost the game
	public void TriggerGameOver()
	{
		if(gamePlaying == true)
		{
			_zombieSpawner.ToggleSpawner(false);
			UIController.ShowGameOverText();
			gamePlaying = false;
		}
	}

	//Player has successfully defeated all the zombies for a night
	public void TriggerRoundOver()
	{
		//TODO: Implement!
	}

	private void OnToggleControlsPressed()
	{
		Player.ToggleControls();

		if (ControlToggleDebugText != null)
		{
			string newText = "Toggle ";
			newText += Player.useMouseAndKeyboard ? "Mouse & Key" : "Controller";
			ControlToggleDebugText.text = newText;
		}
	}

#region BARRIER
	public bool IsBarrierUp()
	{
		return MainBarrier.IsBarrierUp();
	}

	public void HitBarrier(int damage)
	{
		if (IsBarrierUp())
		{
			MainBarrier.OnHit(damage);
		}
	}

	//public Barrier GetBarrierFromXPosition(float xPos)
	//{
	//    int total = _barrierXPositions.Count;
	//    for (int i = 0; i < total;  i++)
	//    {
	//        float barrierXPosition = _barrierXPositions[i];

	//        if(xPos <= barrierXPosition)
	//        {
	//            return _barriers[i];
	//        }
	//    }
	//    Debug.LogFormat("xPos {0} is really far right!", xPos);
	//    return _barriers[total - 1];
	//}
	#endregion
}
