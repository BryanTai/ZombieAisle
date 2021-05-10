using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private const float DEFEND_STORE_X_POSITION = 0.5f;
	private const float MAX_Y_DISTANCE = 8.5f;

	[SerializeField] private WeaponController _weaponController;
	private float _moveSpeed = 5.0f;

	public bool useMouseAndKeyboard = false;

	private Rigidbody2D _playerRB;
	private Vector2 _movementVector;
	private Vector2 _rightJoystickInput;
	private Vector2 _mouseDirection;

	public void SetPosition(Vector3 newPosition)
	{
		this.gameObject.transform.position = newPosition;
	}
	
	private void Start()
	{
		_playerRB = GetComponent<Rigidbody2D>();
		_weaponController.InitializeWeapon();
	}

	private void Update()
	{
		GetPlayerInput();
	}

	private void FixedUpdate()
	{
		Vector2 currentMovement = _movementVector * _moveSpeed * Time.deltaTime; 
		Vector2 newPosition = _playerRB.position + currentMovement;

		if(Mathf.Abs(newPosition.y) > MAX_Y_DISTANCE)
		{
			newPosition = Vector2.ClampMagnitude(newPosition, MAX_Y_DISTANCE);
		}

		_playerRB.MovePosition(newPosition);
	}

	private void GetPlayerInput()
	{
		_movementVector = new Vector2(0, Input.GetAxis("Vertical"));

		//if(Input.GetButtonDown("Shoot")) //only true once
		if (Input.GetButton("Shoot")) //true as long as button is held
		{
			_weaponController.ShootWeapon();
		}
	}

	public void ToggleControls()
	{
		useMouseAndKeyboard = !useMouseAndKeyboard;
	}
}
