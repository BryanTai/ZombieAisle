using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenController : MonoBehaviour
{
	private enum State
	{
		VICTORY,
		DIALOGUE,
		FINISHED
	}

	[SerializeField] private DialogueController _dialogueController;
	private State _state = State.VICTORY;

	void Update()
	{
		switch(_state)
		{
			case State.FINISHED:
				return;
			case State.VICTORY:
				if(Input.GetButtonDown("Shoot"))
				{
					_state = State.DIALOGUE;
					_dialogueController.ShowDialogue("Survivor", "Whew, we made it!");
				}
			break;
			case State.DIALOGUE:
				if(Input.GetButtonDown("Shoot"))
				{
					_state = State.FINISHED;
					_dialogueController.HideDialogue(); //TODO: PAss in Callback function!
				}
			break;
		}
	}

	private void ReturnToRest()
	{
		GameStateController.instance.ChangeGameState(GameState.REST);
	}
}
