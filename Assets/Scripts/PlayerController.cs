using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private WeaponController _weaponController;
	private float _moveSpeed = 5.0f;

	public bool useMouseAndKeyboard = false;

	private Rigidbody2D _playerRB;
	private Vector2 _movementVector;
	private Vector2 _rightJoystickInput;
	private Vector2 _mouseDirection;
	
	private void Start()
	{
		_playerRB = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		GetPlayerInput();
	}

	private void FixedUpdate()
	{
		Vector2 currentMovement = _movementVector * _moveSpeed * Time.deltaTime; 
		_playerRB.MovePosition(_playerRB.position + currentMovement);

		if (useMouseAndKeyboard)
		{
			float mouseAngle = Mathf.Atan2(_mouseDirection.y, _mouseDirection.x) * Mathf.Rad2Deg - 90;
			Quaternion playerRotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
			_playerRB.SetRotation(playerRotation);
		}
		else
		{
			if (_rightJoystickInput.magnitude > 0f)
			{
				Vector3 currentRotation = Vector3.left * _rightJoystickInput.x + Vector3.up * _rightJoystickInput.y;
				Quaternion playerRotation = Quaternion.LookRotation(currentRotation, Vector3.forward);
				_playerRB.SetRotation(playerRotation);
			}
		}
		
	}

	private void GetPlayerInput()
	{
		_movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if(useMouseAndKeyboard)
		{
			_mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		}
		else
		{
			//Player aiming
			_rightJoystickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));

		}

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
