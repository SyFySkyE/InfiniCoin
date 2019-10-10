using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private bool isAlive = true; // Delete SerializeField
    [SerializeField] private float xConstraint = 4.5f;
    [SerializeField] private float jumpForce = 10f;

    Rigidbody playerrb;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Movement();
            CheckForJump();
            ConstrainMovement();
        }
    }

    private void Movement()
    {
        playerrb.velocity = Vector3.forward * forwardSpeed;
        float xAxis = Input.GetAxis("Horizontal");
        float xAdjustment = xAxis * Time.deltaTime * horizontalSpeed;
        Vector3 adjustedPos = new Vector3(xAdjustment, 0f, 0f);
        playerrb.AddRelativeForce(adjustedPos, ForceMode.Impulse); // Change to changing velocity
    }

    private void CheckForJump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            playerrb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
        if (playerrb.velocity.y == 0)
        {
            isGrounded = true;
        }
    }

    private void ConstrainMovement()
    {
        Vector3 currentPos = transform.position;
        if (transform.position.x >= xConstraint)
        {
            transform.position = new Vector3(xConstraint, currentPos.y, currentPos.z);
        }
        else if (transform.position.x <= -xConstraint)
        {
            transform.position = new Vector3(-xConstraint, currentPos.y, currentPos.z);
        }
    }
}
