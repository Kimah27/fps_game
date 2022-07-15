using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject playerHUD;

    public float speed;
    public float crouchSpeed = 9f;
    public float walkSpeed = 18f;
    public float sprintSpeed = 27f;

    public float height;
    public float crouchHeight = 2f;
    public float standingHeight = 4f;

    public float gravity = -45f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool canDoubleJump;

    //public int HP;
    //public int score;
    //public int level;

    //public bool isAlive = true;
    //public bool key = false;

    void Start()
    {
        //HP = 100;
        //score = 0;
        playerHUD = GameObject.FindGameObjectWithTag("HUD");
        //level = 1;
    }

    void Update()
    {
        //if (isAlive)
        if(GameManagerScript.Instance.Alive)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                velocity.z = 0f;
                velocity.x = 0f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            // Decrease player movement speed and crouch when Left CTRL is held down
            // Increase player movement speed when LEFT SHIFT is held down
            if (Input.GetButton("Crouch"))
            {
                speed = crouchSpeed;
                controller.height = crouchHeight;
            }
            else if (Input.GetButton("Sprint"))
            {
                speed = sprintSpeed;
                controller.height = standingHeight;
            }
            else
            {
                speed = walkSpeed;
                controller.height = standingHeight;
            }

            controller.Move(move * speed * Time.deltaTime);

            // Check if player is grounded and can double jump
            if(isGrounded)
            {
                canDoubleJump = true;
            }
            
            // Check if player is grounded, if so, player will jump when SPACEBAR is pressed
            if (Input.GetButtonDown("Jump"))
            {
                if(isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                        canDoubleJump = false;
                    }
                }

            }

            velocity.y += gravity * Time.deltaTime;


            controller.Move(velocity * Time.deltaTime);


            if (GameManagerScript.Instance.Health <= 0)
            {
                StartCoroutine(PlayerDeath());
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "LowJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight * 15) * -2f * gravity);
                break;
            case "HighJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight*25) * -2f * gravity);
                break;
            case "TinyJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight * 5) * -2f * gravity);
                break;
            case "ForwardJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight * 7) * -2f * gravity);
                velocity.z = (Mathf.Sqrt((jumpHeight * 10) * -2f * gravity));
                break;
            case "BackwardsJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight * 7) * -2f * gravity);
                velocity.z = -(Mathf.Sqrt((jumpHeight * 10) * -2f * gravity));
                break;
            case "LeftJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight * 7) * -2f * gravity);
                velocity.x = -(Mathf.Sqrt((jumpHeight * 10) * -2f * gravity));
                break;
            case "RightJumpPad":
                velocity.y = Mathf.Sqrt((jumpHeight * 7) * -2f * gravity);
                velocity.x = (Mathf.Sqrt((jumpHeight * 10) * -2f * gravity));
                break;
        }
    }

    public IEnumerator PlayerDeath()
    {
        //isAlive = false;
        GameManagerScript.Instance.Alive = false;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MenuScene");
    }

    public void WhenHit()
    {
        playerHUD.GetComponent<HUDController>().UpdateHP();
	}
}
