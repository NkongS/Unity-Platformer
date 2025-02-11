using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            enemyAnimator.SetBool("skullDead", true);
            StartCoroutine(DestroyAfterAnimation(enemyAnimator, collision.gameObject));

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterAnimation(Animator animator, GameObject enemy)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(enemy);
    }
}