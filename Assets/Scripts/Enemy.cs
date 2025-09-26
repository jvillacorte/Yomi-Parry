using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;

    private bool isDead = false;

    private Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void takeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth > 0)
        {
            animator.SetBool("isHurt", true);
        }
        else
        {
            Die();
        }
    }

    public void EndHurt()
    {
    animator.SetBool("isHurt", false);
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");
        animator.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

    }

    public void DestroyAfterDeath()
    {
        Debug.Log("Destroying enemy object.");
        Destroy(gameObject);
    }
}
