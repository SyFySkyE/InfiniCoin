using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float xConstrain = 5.5f;

    private Rigidbody playerRB;

    private bool canJump = true;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) MoveForward();
        MoveHorizontally();
        Jump();
    }

    private void MoveForward()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, forwardSpeed);
    }

    private void MoveHorizontally()
    {
        if (transform.position.x >= xConstrain)
        {
            transform.position = new Vector3(xConstrain, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xConstrain)
        {
            transform.position = new Vector3(-xConstrain, transform.position.y, transform.position.z);
        }
        float xRaw = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector3(xRaw * horizontalSpeed, playerRB.velocity.y, playerRB.velocity.z);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
