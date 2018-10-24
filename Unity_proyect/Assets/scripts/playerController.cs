using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool availableToSpawnPortals;
    bool groundFlag;
    bool jumpFlag;
    float xSpeed;
    float xAnti;
    float jumpStart;

    string actualPortalState;
    public SpriteRenderer heroSprite;
    public GameObject targetCanvas;
    public GameObject targetGame;
    public GameObject PortalEnterPrefab;
    public GameObject PortalExitPrefab;
    public Animator APlayer;

    public float maxWalkSpeed;
    public float xImpulse;
    public float xRetard;
    public float xRetardOnAir;
    public float YImpulse;
    public float jumpMinDelay;
    public float jumpMaxDelay;

    private void Start()
    {
        rb.freezeRotation = true;
        actualPortalState = "portalEnter";
        availableToSpawnPortals = true;

        groundFlag = false;
        jumpFlag = false;
        jumpStart = -jumpMaxDelay;
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            groundFlag = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            groundFlag = false;
    }

    public void EnableSpawnPoints()
    {
        availableToSpawnPortals = true;
    }

    public void EnableEnterPortal()
    {
        actualPortalState = "portalEnter";
    }

    public void EnableExitPortal()
    {
        actualPortalState = "portalExit";
    }

    private void Update()
    {
        // Try to place portal
        if (Input.GetButtonDown("Fire1") && availableToSpawnPortals)
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
            if (hit.collider != null)
            {
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
                        availableToSpawnPortals = false;
                    }
                }
            }

        }
        // Target follows mouse
        Vector3 mouseInCanvas = Input.mousePosition;
        Vector2 mouseInGame = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        targetCanvas.transform.position = mouseInCanvas;
        targetGame.transform.position = mouseInGame;
    }

    private void FixedUpdate()
    {
        // X speed
        xSpeed = Input.GetAxis("Horizontal") * maxWalkSpeed;
        xAnti = rb.velocity.x * (groundFlag ? 1 : xRetardOnAir);
        rb.AddForce(Vector2.right * (xSpeed * xImpulse - xAnti * xRetard));
        // X Animation
        APlayer.SetFloat("Speed", Mathf.Abs(xSpeed));
        if (xSpeed != 0)
            heroSprite.flipX = xSpeed < 0;

        // Y speed
        if (groundFlag && Input.GetButton("Jump") && !jumpFlag)
            jumpStart = Time.time;  // Start jumping
        if (Time.time - jumpStart <= jumpMaxDelay && (jumpFlag || Time.time - jumpStart < jumpMinDelay))
            rb.AddForce(Vector2.up * YImpulse);
        jumpFlag = Input.GetButton("Jump");
        // Y Animation
        APlayer.SetBool("Grounded", groundFlag);
    }
}
