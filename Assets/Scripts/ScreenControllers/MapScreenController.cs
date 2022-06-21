using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreenController : RestStateController
{
	private const int MAX_RESERVED_DEFENDERS = 8;
	private const int TOTAL_SLOT_CHOICES = 4;
	private const int DEFENDER_SLOT_CAPACITY = 4;

	private enum HighlightState
	{
		HIDE = 0,
		TOP = 1,
		MIDDLE = 2,
		BOTTOM = 3,
		RESERVE = 4,
	}


	[Header("Highlight References")]
	[SerializeField] private GameObject[] _highlights = new GameObject[TOTAL_SLOT_CHOICES]; 

	[Header("Defender Slots")]
	[SerializeField] private SlotManager _slotManager;

	private HighlightState _currentHighlightState;

	

//TODO: are these arrays necessary? Store the MapDefender arrays in the Defender Slots themselves
	private MapDefender[] _reservedDefenders = new MapDefender[MAX_RESERVED_DEFENDERS];
	private MapDefender[][] _assignedDefenders = new MapDefender[][]
	{
		new MapDefender[DEFENDER_SLOT_CAPACITY],
		new MapDefender[DEFENDER_SLOT_CAPACITY],
		new MapDefender[DEFENDER_SLOT_CAPACITY],
	};

	protected override void InitReferences()
	{
		base.InitReferences();

		InitDefenderData();

		InitHighlightState();
	}

	protected override void SaveChanges()
	{
		//TODO: implement
		Debug.LogWarning("[MapScreenController] Not implemented yet!");
	}

	private void InitDefenderData()
	{
		_slotManager.InitDefenderData();
	}

	private void InitHighlightState()
	{

	}

	private void OnDefenderSlotClicked()
	{

	}
}
