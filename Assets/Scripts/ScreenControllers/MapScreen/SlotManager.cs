using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
	private const int RESERVED_DEFENDER_SLOT_INDEX = 0;

	[Header("Prefab References")]
	[SerializeField] private MapDefender _mapDefenderPrefab;

	[Header("Defender Slot References")]
	[SerializeField] private DefenderSlot[] _defenderSlots;

	private Selectable _previouslySelected;

	public void InitDefenderData()
	{
		//TODO: implement
		//Load saved Defenders and put them in proper slots

		_previouslySelected = null;

		int totalDummyDefenders = 1;

		for(int i = 0; i < totalDummyDefenders; i++)
		{
			MapDefender newDummy = Instantiate(_mapDefenderPrefab);
			newDummy.Init(this);
			_defenderSlots[RESERVED_DEFENDER_SLOT_INDEX].AssignDefender(newDummy);
		}
	}

	public void OnSelectableClicked(Selectable clickedObject)
	{
		if(clickedObject == null)
		{
			Debug.LogError("[SlotManager] Something really buggered up");
			return;
		}

		if(clickedObject == _previouslySelected)
		{
			//Selected the same thing twice
			_previouslySelected.OnDeselect();
			_previouslySelected = null;
			return;
		}

		if(_previouslySelected == null)
		{
			_previouslySelected = clickedObject;
			return;
		}

		DefenderSlot clickedSlot = clickedObject as DefenderSlot;
		if(clickedSlot != null)
		{
			OnDefenderSlotClicked(clickedSlot);
			return;
		}

		MapDefender clickedDefender = clickedObject as MapDefender;
		if(clickedDefender != null)
		{
			OnMapDefenderClicked(clickedDefender);
			return;
		}

		Debug.LogError($"[SlotManager] OnSelectableClicked should not hit this point! Missing a Type handle?");
	}

	private void OnDefenderSlotClicked(DefenderSlot clickedSlot)
	{
		Debug.Log($"[SlotManager] Clicked Slot {clickedSlot.SlotIndex}");

		DefenderSlot slot = _previouslySelected as DefenderSlot;
		if(slot != null)
		{
			//Clicked a slot and then another slot. Switch focus to new slot.
			_previouslySelected.OnDeselect();
			_previouslySelected = clickedSlot;
			return;
		}

		MapDefender defender = _previouslySelected as MapDefender;
		if(defender != null)
		{
			//Assign the Defender to the Slot!
			clickedSlot.AssignDefender(defender);
			clickedSlot.OnDeselect();
			_previouslySelected.OnDeselect();
			_previouslySelected = null;
			return;
		}

		Debug.LogError($"[SlotManager] Should not hit this point! Missing a Type handle?");
	}

	private void OnMapDefenderClicked(MapDefender clickedDefender)
	{
		Debug.Log($"[SlotManager] Clicked Defender {clickedDefender.Name}");

		MapDefender defender = _previouslySelected as MapDefender;
		if(defender != null)
		{
			//Clicked a defender and then another defender. Switch focus to new defender.
			_previouslySelected.OnDeselect();
			_previouslySelected = clickedDefender;
			return;
		}

		DefenderSlot slot = _previouslySelected as DefenderSlot;
		if(slot != null)
		{
			//Assign the Defender to the Slot!
			slot.AssignDefender(clickedDefender);
			clickedDefender.OnDeselect();
			_previouslySelected.OnDeselect();
			_previouslySelected = null;
			return;
		}

		Debug.LogError($"[SlotManager] Should not hit this point! Missing a Type handle?");
	}
}
