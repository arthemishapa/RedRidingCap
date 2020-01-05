using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrial : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 1.0f;

    Animator animation;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // let the gameObject fall down
        gameObject.transform.position = new Vector3(0, 0.01f, 20);
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                animation.SetInteger("condition", 1);
            else animation.SetInteger("condition", 0);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        //// Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}

