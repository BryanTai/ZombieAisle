using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for a Screen Controller that appears during the Rest GameState
public abstract class RestStateController : MonoBehaviour
{
	[SerializeField] protected Button _closeButton;

	protected RestScreenController _restController;

	private void Start()
	{
		InitReferences();
	}

	protected virtual void InitReferences()
	{
		_closeButton.onClick.AddListener(OnCloseButtonClicked);
		_restController = GameStateController.instance.RestController;
	}

	private void OnCloseButtonClicked()
	{
		SaveChanges();
		_restController.SwitchRestState(RestState.NONE);
	}

	protected abstract void SaveChanges();
}
