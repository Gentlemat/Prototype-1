using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float rpm;
    [SerializeField] private int horsePower = 20000;
    [SerializeField] private float turnSpeed = 25f;

    [SerializeField] Transform centerOfMass;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] private TextMeshProUGUI speedometerText;
    [SerializeField] private TextMeshProUGUI rpmText;
    private Rigidbody playerRb;
    private int wheelsOnGround;

    private float horizontalInput;
    private float forwardInput;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (isOnGround())
        {
            //Move the vehicle forward
            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
            //Rotate the vehicle
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f);
            speedometerText.text = "Speed: " + speed + "km/h";

            rpm = Mathf.Round((speed % 30)) * 40;
            rpmText.text = "RPM: " + rpm;
        }
    }

    private bool isOnGround()
    {
        wheelsOnGround = 0;

        foreach (var wheel in allWheels)
        {
            if (wheel.isGrounded)
                wheelsOnGround++;
        }

        if (wheelsOnGround == 4)
            return true;

        return false;
    }
}
