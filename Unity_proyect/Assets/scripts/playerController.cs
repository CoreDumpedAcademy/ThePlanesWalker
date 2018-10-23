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
    bool aviableToSpawnPortals;
    float timeJumping;
    float timeBouncing;
    string actualPortalState;
    public SpriteRenderer heroSprite;
    public GameObject targetCanvas;
    public GameObject targetGame;
    public GameObject PortalEnterPrefab;
    public GameObject PortalExitPrefab;
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

    private void Start()
    {
        onGroundSentinel = false;
        jumpingSentinel = false;
        rb.freezeRotation = true;
        bounceSentinel = false;
        actualPortalState = "portalEnter";
        aviableToSpawnPortals = true;
        timeJumping = -minJump;
        timeBouncing = -maxBouncing;
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void EnableSpawnPoints()
    {
        aviableToSpawnPortals = true;
    }

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

    private void Update()
    {
        Vector3 mouseInCanvas = Input.mousePosition;
        Vector2 mouseInGame = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        targetCanvas.transform.position = mouseInCanvas;
        targetGame.transform.position = mouseInGame;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mouseInGame - new Vector2(transform.position.x, transform.position.y)));
        Debug.Log(hit.collider.gameObject.tag);
        if (Input.GetButtonDown("Fire1") && aviableToSpawnPortals && hit.collider != null)
            if (hit.collider.gameObject.CompareTag("Ground") || hit.collider.gameObject.CompareTag("Bouncer"))
            {
                if (actualPortalState == "portalEnter")
                {
                    actualPortalState = "portalExit";
                    Instantiate(PortalEnterPrefab, hit.point, Quaternion.Euler(0, 0, 0));
                }
                else if (actualPortalState == "portalExit")
                {
                    actualPortalState = "portalEnter";
                    Instantiate(PortalExitPrefab, hit.point, Quaternion.Euler(0, 0, 0));
                    aviableToSpawnPortals = false;
                }
            }
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
