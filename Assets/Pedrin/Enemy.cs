using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    private bool isFacingRight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        //Player direção
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        //Deteccao se player esta acima
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << player.gameObject.layer);

        if (rb.linearVelocityX <= 0 && isFacingRight) flip();
        else if (rb.linearVelocityX > 0 && !isFacingRight) flip();

        if (isGrounded)
        {
            //Seguir jogador
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

            //if ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);

            //Se tem um gap(vão)
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new UnityEngine.Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);

            //If plataforma acima
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            if (!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
