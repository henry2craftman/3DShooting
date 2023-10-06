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
    float roll, pitch, yaw;
    float h, v;
    [SerializeField] GameObject[] lasers;
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

        FireLaser();
    }

    private void FireLaser()
    {
        if(Input.GetButton("Fire1"))
        {
            // Laser On
            SetLaserActive(true);
        }
        else
        {
            // Laser Off
            SetLaserActive(false);
        }
    }

    private void SetLaserActive(bool isActive)
    {
        foreach(var laser in lasers)
        {
            var particle = laser.GetComponent<ParticleSystem>().emission;
            particle.enabled = isActive;
        }
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
