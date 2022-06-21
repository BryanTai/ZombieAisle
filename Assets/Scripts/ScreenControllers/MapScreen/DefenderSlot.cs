using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSlot : MonoBehaviour, Selectable
{
	public SlotManager Manager;
	public int SlotIndex;
	public int TotalSlots;
	[SerializeField] private GameObject _highlight;
	[SerializeField] private Transform _gridParent;
	[SerializeField] private Button _slotButton;

	private MapDefender[] _assignedDefenders;
	private int _lastDefenderIndex = 0;

	public void AssignDefender(MapDefender defender)
	{
		if(defender == null) return;

		_assignedDefenders[_lastDefenderIndex] = defender;
		_lastDefenderIndex++;

		defender.transform.SetParent(_gridParent, false);
	}

	private void Awake()
	{
		if(Manager == null)
		{
			Debug.LogError("Missing Manager!");
		}
		_assignedDefenders = new MapDefender[TotalSlots];
		_slotButton.onClick.AddListener(OnSelected);
		_highlight.SetActive(false);
	}

	public void OnSelected()
	{
		//TODO: Implement!
		//Show highlight effects!
		Manager.OnSelectableClicked(this);
		_highlight.SetActive(true);
	}

	public void OnDeselect()
	{
		//TODO: Implement!
		_highlight.SetActive(false);
	}

}
