using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanRow : MonoBehaviour
{
	public delegate void OnHoursChanged();

	[Header("Cosmetic Elements")]
	[SerializeField] private Text _rowTitle;

	[Header("Input Elements")]
	[SerializeField] private Text _hoursText;
	[SerializeField] private Button _plusButton;
	[SerializeField] private Button _minusButton;

	public int Hours { get { return _hours; } }

	private int _hours;
	private OnHoursChanged _onHoursChangedCallback;

	public void Init(int initHours, OnHoursChanged callback)
	{
		_onHoursChangedCallback = callback;
		_hours = initHours;
		OnHoursButtonPressed();
	}

	public void TogglePlusButton(bool enabled)
	{
		_plusButton.interactable = enabled;
	}

	#region Button Callbacks
	public void OnPlusButtonPressed()
	{
		_hours++;
		OnHoursButtonPressed();
	}

	public void OnMinusButtonPressed()
	{
		_hours--;
		OnHoursButtonPressed();
	}

	private void OnHoursButtonPressed()
	{
		_minusButton.interactable = (_hours > 0);
		_hours = Mathf.Max(0, _hours);
		_hoursText.text = _hours.ToString();
		_onHoursChangedCallback.Invoke();
	}
	#endregion
}
