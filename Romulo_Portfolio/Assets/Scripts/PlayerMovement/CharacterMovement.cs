using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;

    public float jumpForce;

    public float playerGravity = 8;
    
    CharacterController controller;

    Vector3 move;

    void Start()
    {
        move = Vector3.zero;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        move.x = 0;
        move.z = 0;

        move += Input.GetAxis("Horizontal") * transform.right * speed;
        move += Input.GetAxis("Vertical") * transform.forward * speed;
        move.y -= playerGravity;

        controller.Move(move * Time.deltaTime);

    }

    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            move.y = jumpForce;
        }
    }

}
