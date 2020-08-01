using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    public const float MAX_Y_VALUE = 10;
    public const float MIN_Y_VALUE = -12;

    private void Update()
    {
        float newY = Mathf.Clamp(playerTransform.position.y, MIN_Y_VALUE, MAX_Y_VALUE);
        transform.position = new Vector3(0, newY, transform.position.z);
    }
}
