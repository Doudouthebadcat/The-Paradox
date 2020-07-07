using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isjumping;
    private bool isGrounded;
    private bool isWalledRight;
    private bool isWalledLeft;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Transform WallCheckTopRight;
    public Transform WallCheckBottomRight; 
    public Transform WallCheckTopLeft;
    public Transform WallCheckBottomLeft;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

     void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        isWalledRight = Physics2D.OverlapArea(WallCheckTopRight.position, WallCheckBottomRight.position);
        isWalledLeft = Physics2D.OverlapArea(WallCheckTopLeft.position, WallCheckBottomLeft.position);

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isjumping = true;
        }
        if (Input.GetButtonDown("Jump") && isWalledRight || Input.GetButtonDown("Jump") && isWalledLeft)
        {
            isjumping = true;
        }
        
        MovePlayer(horizontalMovement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if(isjumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isjumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if( _velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
