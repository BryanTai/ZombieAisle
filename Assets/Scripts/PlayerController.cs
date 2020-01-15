using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WeaponController weaponController;
    public float moveSpeed = 10.0f;

    public bool useMouseAndKeyboard = false;
    
    private Rigidbody2D _playerRB;
    private Vector2 movementVector;
    private Vector2 rightJoystickInput;
    private Vector2 mouseDirection;
    
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
        Vector2 currentMovement = movementVector * moveSpeed * Time.deltaTime; 
        _playerRB.MovePosition(_playerRB.position + currentMovement);

        if (useMouseAndKeyboard)
        {
            float mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion playerRotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
            _playerRB.SetRotation(playerRotation);
        }
        else
        {
            if (rightJoystickInput.magnitude > 0f)
            {
                Vector3 currentRotation = Vector3.left * rightJoystickInput.x + Vector3.up * rightJoystickInput.y;
                Quaternion playerRotation = Quaternion.LookRotation(currentRotation, Vector3.forward);
                _playerRB.SetRotation(playerRotation);
            }
        }
        
    }

    private void GetPlayerInput()
    {
        movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(useMouseAndKeyboard)
        {
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            //Player aiming
            rightJoystickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));

        }

        //if(Input.GetButtonDown("Shoot")) //only true once
        if (Input.GetButton("Shoot")) //true as long as button is held
        {
            weaponController.ShootWeapon();
        }
    }
}
