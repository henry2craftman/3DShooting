using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float clampXPos = 10;
    [SerializeField] float clampYPosMin = -3f;
    [SerializeField] float clampYPosMax = 8f;
    [SerializeField] float roll;
    [SerializeField] float pitch;
    [SerializeField] float yaw;
    float h, v;
    //private void OnEnable()
    //{
    //    movement.Enable();
    //}

    //private void OnDisable()
    //{
    //    movement.Disable();
    //}

    void Update()
    {
        MovePlayer();

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        roll = h * Time.fixedDeltaTime * rotationSpeed;
        pitch = v * Time.fixedDeltaTime * rotationSpeed;
        yaw = h * Time.fixedDeltaTime * rotationSpeed;

        transform.localRotation = Quaternion.Euler(pitch, -yaw, roll);
    }

    private void MovePlayer()
    {
        //float hNewInputSystem = movement.ReadValue<Vector2>().x;
        //float vNewInputSystem = movement.ReadValue<Vector2>().y;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        float xOffset = h * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -clampXPos, clampXPos);

        float yOffset = v * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, clampYPosMin, clampYPosMax);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0);
    }
}
