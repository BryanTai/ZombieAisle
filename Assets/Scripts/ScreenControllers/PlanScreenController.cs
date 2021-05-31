using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanScreenController : MonoBehaviour
{
	[SerializeField] private Button _closeButton;
	[SerializeField] private Button _startNightButton;

	private void Start()
	{
		_closeButton.onClick.AddListener(OnCloseButtonClicked);
		_startNightButton.onClick.AddListener(OnStartButtonClicked);
	}

	private void OnCloseButtonClicked()
	{
		SaveChanges();
		GameStateController.instance.ChangeGameState(GameState.RESTDAY);
	}

	private void OnStartButtonClicked()
	{
		SaveChanges();
		GameStateController.instance.ChangeGameState(GameState.RESTNIGHT);
	}

	private void SaveChanges()
	{
		//TODO: implement
	}
}
