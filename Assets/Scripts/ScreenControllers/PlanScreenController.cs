using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanScreenController : RestStateController
{
	[SerializeField] private Button _startNightButton;

	protected override void InitReferences()
	{
		base.InitReferences();
		_startNightButton.onClick.AddListener(OnStartButtonClicked);
	}

	private void OnStartButtonClicked()
	{
		SaveChanges();
		_restController.SetToNight();
	}

	protected override void SaveChanges()
	{
		//TODO: implement
		Debug.LogWarning("[PlanScreenController] Not implemented yet!");
	}
}
