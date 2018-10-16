using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool onGroundSentinel;
    bool jumpingSentinel;
    bool bounceSentinel;
    float timeJumping;
    float timeBouncing;
    public SpriteRenderer sprite;
    public float impulse;
    public float retard;
    public float retardOnAir;
    public float jumpImpulse;
    public float minJump;
    public float maxJump;
    public float bounceForce;
    public float maxBouncing;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGroundSentinel = true;
        }
        else if (collision.gameObject.CompareTag("Bouncer"))
        {
            timeBouncing = Time.time;
            bounceSentinel = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGroundSentinel = false;
        }
    }

    private void Awake()
    {
        timeJumping = -minJump;
        timeBouncing = -maxBouncing;
        rb = this.GetComponent<Rigidbody2D>();
        onGroundSentinel = false;
        jumpingSentinel = false;
        rb.freezeRotation = true;
        bounceSentinel = false;
    }

    private void FixedUpdate()
    {
        if (onGroundSentinel && Input.GetButton("Jump"))
        {
            onGroundSentinel = false;
            timeJumping = Time.time;
            jumpingSentinel = true;
        }
        float xMove = Input.GetAxis("Horizontal");
        Vector2 vxMove = new Vector2(xMove, 0.0F);
        if (!Input.GetButton("Jump"))
        {
            jumpingSentinel = false;
        }
        if (timeBouncing + maxBouncing < Time.time)
        {
            bounceSentinel = false;
        }
        if (!bounceSentinel)
        {
            if (onGroundSentinel)
            {
                rb.AddForce(vxMove * impulse);
                rb.AddForce(Vector2.right * rb.GetPointVelocity(this.transform.position) * -retard);
            }
            else
            {
                rb.AddForce(vxMove * impulse * retardOnAir);
                rb.AddForce(Vector2.right * rb.GetPointVelocity(this.transform.position) * -retard * retardOnAir);
            }
            if ((jumpingSentinel || timeJumping + minJump > Time.time) && !(timeJumping + maxJump < Time.time))
            {
                rb.AddForce(Vector2.up * jumpImpulse);
            }
        }
    }
}
