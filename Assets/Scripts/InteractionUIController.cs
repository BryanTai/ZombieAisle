using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIController : MonoBehaviour
{
	//[SerializeField] private Collider2D _interactionCollider;
	[SerializeField] private GameObject _interactSprite;

	private PlayerController _player;

	private void Start()
	{
		_player = GameController.instance.Player;
		SetSpriteActive(false);
	}

	public void SetSpriteActive(bool isActive)
	{
		_interactSprite.SetActive(isActive);
	}

	private void StartInteraction()
	{
		SetSpriteActive(false);
	}

}
