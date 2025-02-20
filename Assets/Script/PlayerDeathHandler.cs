using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    Animator animator;
    public float deathDelay = 2f;
    private bool isDead = false;
    Move moveScript;
    Shooting shootScript;
    [SerializeField] AudioClip deathSFX;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<Move>();
        shootScript = GetComponent<Shooting>();
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
        moveScript.enabled = false;
        shootScript.enabled = false;
        
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        isDead = true;
        
        animator.SetBool("isDead", true);

        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathDelay);
        FindFirstObjectByType<GameSession>().ProcessPlayerDeath();{}
    }
}