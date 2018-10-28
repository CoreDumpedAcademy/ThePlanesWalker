using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour {

    private float maxWalkSpeed = 1;
    private float jumpSpeed = 1;
    //private float vSpeed = 0;
    SpriteRenderer pato;

    Animator animator;
    Vector3 playerVelocity;

    void Start()
    {
        playerVelocity = Vector3.zero;
        maxWalkSpeed = 7;
        jumpSpeed = 7;
        pato = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerVelocity.x = Input.GetAxis("Horizontal") * maxWalkSpeed;

        animator.SetFloat("Forward", Mathf.Abs(playerVelocity.x));
        animator.SetFloat("Jump", Mathf.Abs(playerVelocity.y));


        if (playerVelocity.x < 0)
            pato.flipX = true;
        else if(playerVelocity.x > 0)
            pato.flipX = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y = jumpSpeed;
        }
        else
        {
            playerVelocity.y = GetComponent<Rigidbody2D>().velocity.y;
        }

        GetComponent<Rigidbody2D>().velocity = playerVelocity;
    }

}
