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

    private float startTime, elapsedTime;

    private PlayerController _player;

    TimeSpan timePlaying;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gamePlaying = false;
        _player = FindObjectOfType<PlayerController>();

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
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            //string timeToDisplay = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        }
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
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
