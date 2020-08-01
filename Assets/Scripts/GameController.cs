using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public const float ZOMBIES_AT_BARRIER_Y_POS = 12.5f; // Zombies stop here to hit barriers
    public const float BARRIER_Y_POS = 13f; //Spawn Barriers here
    public const float GAMEOVER_Y_POSITION = 15f; // If a zombie reaches this point, game over

    public bool gamePlaying { get; private set; }

    [Header("Barriers")]
    public Barrier BarrierPrefab;
    public GameObject BarrierParent;
    public List<float> BarrierXPositions;

    private List<Barrier> Barriers = new List<Barrier>();

    [Header("Settings")]
    [SerializeField] private Button ControlToggleDebugButton;
    [SerializeField] private Text ControlToggleDebugText;

    [Header("Controllers")]
    public UIController UIController;

    private float _startTime, _elapsedTime;

    private PlayerController _player;
    private ZombieSpawner _zombieSpawner;

    TimeSpan timePlaying;

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

        if(BarrierPrefab == null || BarrierXPositions == null)
        {
            Debug.LogError("[GameController] Barriers missing!");
        }
    }

    private void Start()
    {
        gamePlaying = false;
        //TODO: Spawn in a new player instead of using the scene player!
        _player = FindObjectOfType<PlayerController>();
        _zombieSpawner = GetComponent<ZombieSpawner>();

        //Set up Barriers
        foreach(float xPos in BarrierXPositions)
        {
            Barrier newBarrier = Instantiate(BarrierPrefab, BarrierParent.transform);
            newBarrier.transform.position = new Vector3(xPos, BARRIER_Y_POS);
            Barriers.Add(newBarrier);
        }

        //Debug Controls
        if (ControlToggleDebugButton != null)
        {
            ControlToggleDebugButton.onClick.AddListener(OnToggleControlsPressed);
        }

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
            timePlaying = TimeSpan.FromSeconds(_elapsedTime);

            //string timeToDisplay = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        }
    }

    private void BeginGame()
    {
        gamePlaying = true;
        _startTime = Time.time;

        _zombieSpawner.ToggleSpawner(true);
    }

    private void OnToggleControlsPressed()
    {
        _player.ToggleControls();

        if (ControlToggleDebugText != null)
        {
            string newText = "Toggle ";
            newText += _player.useMouseAndKeyboard ? "Mouse & Key" : "Controller";
            ControlToggleDebugText.text = newText;
        }
    }
}
