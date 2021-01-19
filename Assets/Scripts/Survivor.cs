using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Non Playable Character, the other Survivors
public class Survivor : Interactable
{
	[SerializeField] private InteractionUIController _interactionController;

	private void Awake()
	{
		InteractText = "TALK"; //TODO: placeholder!
	}


#region Interactable
	public override void Interact()
	{
		//TODO: start dialogue!
		Debug.Log("STARTING DIALOGUE!!!");

		//Tell the GameController that we're in Dialogue mode
	}

#endregion

	public bool CanTalkTo()
	{
		//TODO: Check if there are available dialogue options! -btai
		return true;
	}

	

}
