using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    private Rigidbody2D _playerRB;
    private Vector2 leftStickInput;
    
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
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
