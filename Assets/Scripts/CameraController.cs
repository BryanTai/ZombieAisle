﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform playerTransform;

	[SerializeField] private Camera _mainCamera;

	public const float MAX_Y_VALUE = 22;
	public const float MIN_Y_VALUE = -12;

	private const float GAMEPLAY_SIZE = 9.25f;
	private const float DIALOGUE_SIZE = 5f;

	private void Start()
	{
		SetCameraToGameplay();
	}

	private void Update()
	{
		float newY = Mathf.Clamp(playerTransform.position.y, MIN_Y_VALUE, MAX_Y_VALUE);
		transform.position = new Vector3(0, newY, transform.position.z);
	}

	public void SetCameraToDialogue()
	{
		_mainCamera.orthographicSize = DIALOGUE_SIZE;
	}

	public void SetCameraToGameplay()
	{
		_mainCamera.orthographicSize = GAMEPLAY_SIZE;
	}

}
