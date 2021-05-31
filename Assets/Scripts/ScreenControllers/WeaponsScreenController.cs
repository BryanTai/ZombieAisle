using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsScreenController : MonoBehaviour
{
	[SerializeField] private Button _closeButton;

	private void Start()
	{
		_closeButton.onClick.AddListener(OnCloseButtonClicked);
	}

	private void OnCloseButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.RESTDAY);
	}
}
