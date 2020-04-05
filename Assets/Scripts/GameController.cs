using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public bool gamePlaying { get; private set; }

    [SerializeField] private Button ControlToggleDebugButton;
    [SerializeField] private Text ControlToggleDebugText;

    private float _startTime, _elapsedTime;

    private PlayerController _player;
    private ZombieSpawner _zombieSpawner;

    TimeSpan timePlaying;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gamePlaying = false;
        _player = FindObjectOfType<PlayerController>();
        _zombieSpawner = GetComponent<ZombieSpawner>();

        //Debug Controls
        if (ControlToggleDebugButton != null)
        {
            ControlToggleDebugButton.onClick.AddListener(OnToggleControlsPressed);
        }

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
