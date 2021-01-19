using UnityEngine;

//Something that the Player can interact with by moving close to it and pressing the "Interact" key
public abstract class Interactable : MonoBehaviour
{
	public string InteractText;
	public abstract void Interact();
}
