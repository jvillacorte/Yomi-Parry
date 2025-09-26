using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    float horizontalMovement;
    bool isGrounded;
    float groundRadius = 0.2f; // Updated ground radius
    [SerializeField] private Animator _animator;
    bool facingRight = true;
    public GameObject attackHitbox;

    public float attackCooldown = 0.5f; // Time in seconds between attacks
    private float lastAttackTime = 0f;

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);

            // Ground check
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
            // Set animator parameters for blend trees
            _animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
            _animator.SetFloat("yVelocity", rb.linearVelocity.y);
            _animator.SetBool("isJumping", !isGrounded);
            _animator.SetBool("isRunning", Mathf.Abs(horizontalMovement) > 0.01f);


            // Cancel attack if airborne
            if (_animator.GetBool("isAttacking") && !isGrounded)
            {
                _animator.SetBool("isAttacking", false);
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!PauseMenu.isPaused)
        {
            horizontalMovement = context.ReadValue<Vector2>().x;

            if (horizontalMovement != 0)
            {
                _animator.SetBool("isRunning", true);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jump called. isGrounded: {isGrounded}, performed: {context.performed}");
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!PauseMenu.isPaused && context.performed && isGrounded)
        {
            if (Time.time >= lastAttackTime + attackCooldown && !_animator.GetBool("isAttacking"))
            {
                _animator.SetBool("isAttacking", true);
                lastAttackTime = Time.time; // reset cooldown timer
                Debug.Log("Attack performed");
            }
        }
    }

    public void endAttack()
    {
        _animator.SetBool("isAttacking", false);
    }

    void FixedUpdate()
    {
        if (horizontalMovement > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        facingRight = !facingRight;
    }

    public void enableHitbox()
    {
        attackHitbox.GetComponent<Collider2D>().enabled = true;
    }

    public void disableHitbox()
    {
        attackHitbox.GetComponent<Collider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().takeDamage(20);
        }
    }
}