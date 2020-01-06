using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    private Rigidbody2D _playerRB;
    private Vector2 leftStickInput;
    private Vector2 rightStickInput;
    
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
        Vector2 currentMovement = leftStickInput * moveSpeed * Time.deltaTime; 
        _playerRB.MovePosition(_playerRB.position + currentMovement);

        if(rightStickInput.magnitude > 0f)
        {
            Vector3 currentRotation = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
            Quaternion playerRotation = Quaternion.LookRotation(currentRotation, Vector3.forward);

            _playerRB.SetRotation(playerRotation);
        }
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //TODO: Read Mouse input too
        rightStickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));
    }
}
