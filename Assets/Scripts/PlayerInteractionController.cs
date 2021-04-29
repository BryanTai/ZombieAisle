using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
	private Interactable _currentInteractableObject = null;
	private UIController _uiController;

	private void Start()
	{
		_uiController = GameController.instance.UIController;
	}

	private void Update()
	{
		//TODO: Check Interact button press && current obj is not null
		//then SendMessage to the other object
		
		if(Input.GetButtonDown("Interact") && _currentInteractableObject != null)
		{
			//TODO: Stop player movement
			// if Survivor, show Dialogue overlay
			// if POI, show
			//Either way, call the Interact function
			_uiController.SetInteractTextActive(false);
			_currentInteractableObject.Interact();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("ENTERED TRIGGER!");
		_currentInteractableObject = other.gameObject.GetComponent<Interactable>();

		if(_currentInteractableObject == null)
		{
			return;
		}

		Survivor survivor = _currentInteractableObject as Survivor;

		if(survivor != null)
		{
			//TODO: Show TALK UI over GameObject
			
			_uiController.SetInteractText(survivor.InteractText);
			_uiController.SetInteractTextActive(true);
		}

		
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("EXITED TRIGGER!");

		if(other.CompareTag(GameController.PLAYER_TAG))
		{
			//TODO: Show interact UI over GameObject
			_currentInteractableObject = null;
			_uiController.SetInteractTextActive(false);
		}
	}
}
