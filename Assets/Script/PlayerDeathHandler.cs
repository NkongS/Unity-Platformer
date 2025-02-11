using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    Animator animator;
    public float deathDelay = 2f;
    private bool isDead = false;
    Move moveScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<Move>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Hazard"))
        {
            TriggerDeath();
        }
        else if (!isDead && collision.gameObject.CompareTag("Enemy"))
        {
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
            if (enemyAnimator != null && !enemyAnimator.GetBool("skullDead"))
            {
                TriggerDeath();
            }
        }
    }

    void TriggerDeath()
    {
        if (animator == null)
        {
            Debug.LogError("Animator not assigned.");
            return;
        }
        isDead = true;
        animator.SetBool("isDead", true);
        if (moveScript != null)
        {
            moveScript.enabled = false;
        }
        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}