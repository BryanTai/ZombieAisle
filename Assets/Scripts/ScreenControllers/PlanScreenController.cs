using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanScreenController : RestStateController
{
	private const int TOTAL_HOURS_IN_DAY = 12;
	[SerializeField] private Button _startNightButton;

	[Header("Hour Text fields")]
	[SerializeField] private Text _hoursAvailableText;
	[SerializeField] private PlanRow[] _planRows = new PlanRow[3];

	private int _totalHoursRemaining;

	protected override void InitReferences()
	{
		base.InitReferences();
		_startNightButton.onClick.AddListener(OnStartButtonClicked);

		InitPlanRows();
	}

	private void InitPlanRows()
	{
		foreach(PlanRow planRow in _planRows)
		{
			planRow.Init(0, OnHoursChanged);
		}

		OnHoursChanged();
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

	#region Input Functions

	public void OnHoursChanged()
	{
		int totalHoursAssigned = 0;
		foreach(PlanRow planRow in _planRows)
		{
			totalHoursAssigned += planRow.Hours;
		}

		_totalHoursRemaining = TOTAL_HOURS_IN_DAY - totalHoursAssigned;
		_totalHoursRemaining = Mathf.Max(0, _totalHoursRemaining);
		_hoursAvailableText.text = _totalHoursRemaining.ToString();

		bool canAssignHours = _totalHoursRemaining > 0;

		foreach(PlanRow planRow in _planRows)
		{
			planRow.TogglePlusButton(canAssignHours);
		}
	}

	#endregion
}
