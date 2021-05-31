﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreenController : MonoBehaviour
{
	[SerializeField] private Button _closeButton;

	private void Start()
	{
		_closeButton.onClick.AddListener(OnCloseButtonClicked);
	}

	private void OnCloseButtonClicked()
	{
		SaveChanges();
		GameStateController.instance.ChangeGameState(GameState.RESTDAY);
	}

	private void SaveChanges()
	{
		//TODO: implement
	}
}
