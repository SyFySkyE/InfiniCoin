using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float xConstrain = 5.5f;
    [SerializeField] private float gravityModifier = 3f;
    [SerializeField] private float secondsBeforeReload = 1.5f;

    [SerializeField] private SceneManager sceneManager;

    private Rigidbody playerRB;
    private Animator playerAnim;

    private bool canJump = true;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) MoveForward();
        else
        {
            StartCoroutine(StartGameOverSequence());
        }
        MoveHorizontally();
        Jump();
    }

    private void MoveForward()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, forwardSpeed);
        playerAnim.SetFloat("Speed_f", 0.7f);
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
            playerAnim.SetTrigger("Jump_trig");
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
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
        }
    }

    private IEnumerator StartGameOverSequence()
    {
        yield return new WaitForSeconds(secondsBeforeReload);
        sceneManager.ReloadScene();
    }
}
