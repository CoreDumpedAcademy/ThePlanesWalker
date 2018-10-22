using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 playerVelocity;
    bool onGroundSentinel;
    bool jumpingSentinel;
    bool bounceSentinel;
    float timeJumping;
    float timeBouncing;
    public SpriteRenderer heroSprite;
    public GameObject targetSprite;
    public GameObject targetSprite2;
    public Animator APlayer;
    public float maxWalkSpeed = 2;
    public float impulse;
    public float retard;
    public float retardOnAirMultiplayer;
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

    private void Update()
    {
        targetSprite.transform.position = Input.mousePosition;
        targetSprite2.transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
    }

    private void FixedUpdate()
    {
        playerVelocity.x = Input.GetAxis("Horizontal") * maxWalkSpeed;
        APlayer.SetFloat("Speed", Mathf.Abs(playerVelocity.x));
   
        if (playerVelocity.x < 0)
            heroSprite.flipX = true;
        else if (playerVelocity.x > 0)
            heroSprite.flipX = false;

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
                rb.AddForce(vxMove * impulse);
                rb.AddForce(Vector2.right * rb.GetPointVelocity(this.transform.position) * -retard * retardOnAirMultiplayer);
            }
            if ((jumpingSentinel || timeJumping + minJump > Time.time) && !(timeJumping + maxJump < Time.time))
            {
                rb.AddForce(Vector2.up * jumpImpulse);
            }
        }
        APlayer.SetBool("Grounded", onGroundSentinel);
    }
}
