using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeyLogger : MonoBehaviour
{
	[SerializeField] private DialogueController _dialogueController;

	private void Update()
	{
		if(Input.GetButtonDown("DebugShowDialogue"))
		{
			_dialogueController.ShowDialogue("TESTING", "Testing dialogue!");
		}
		else if(Input.GetButtonDown("DebugHideDialogue"))
		{
			_dialogueController.HideDialogue();
		}
	}
}
