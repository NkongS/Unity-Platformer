using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] float runSpeed = 2.0f;
    [SerializeField] float jumpSpeed = 4.0f;
    [SerializeField] float climbSpeed = 2.0f;

    Vector2 move;

    Rigidbody2D rb;

    Animator theAnimator;

    CapsuleCollider2D theCollider;

    float gravityScaleAtStart;

    private int jumpCount = 0;
    private const int maxJumps = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        theAnimator = GetComponent<Animator>();
        theCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        flipSprite();
        runJohn();
        climbLadder();
        if (theCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { jumpCount = 0; }
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        // Debug.Log("Move: " + move);
    }

    void OnJump(InputValue value)
    {
        // if(!theCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }
        if (value.isPressed && jumpCount < maxJumps)
        {
            rb.linearVelocityY += jumpSpeed;
            jumpCount++;
        }
    }

    void climbLadder()
    {
        if (!theCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        { 
            rb.gravityScale = gravityScaleAtStart;
            theAnimator.SetBool("isClimbing", false);
            return; 
        }

        rb.linearVelocityY = move.y * climbSpeed;
        rb.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon;

        theAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void runJohn()
    {
        Vector2 playerVelocity = new Vector2(move.x * runSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            theAnimator.SetBool("isRunning", true);
        }
        else
        {
            theAnimator.SetBool("isRunning", false);
        }
    }

    void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f);
        }
    }
}
