using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Inspector Variables
    [Header("Basic Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;

    //Instance Variables
    private Vector2 moveDirection;

    private Rigidbody2D rb;

    private bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    void FixedUpdate()
    {
        if (canMove) Move();
    }

    void CheckInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 directionVector = new Vector2(moveX, moveY);
        moveDirection = directionVector.normalized;

        animator.SetFloat("X", moveX);
        animator.SetFloat("Y", moveY);

        if (directionVector != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
