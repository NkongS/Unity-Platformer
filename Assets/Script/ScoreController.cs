using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    Animator animator;
    private int score = 0;
    Move moveScript;
    private bool isPortalOpen = false;
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 10;

    void Start()
    {
        animator = GameObject.Find("Portal").GetComponent<Animator>();
        moveScript = GetComponent<Move>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score++;
            FindFirstObjectByType<GameSession>().AddToScore(coinValue);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Debug.Log("Score: " + score);
            Destroy(collision.gameObject);
        }

        if (score == 3 || score > 3)
        {
            PortalOpen();
        }
    }


    void PortalOpen()
    {
        animator.SetBool("isOpen", true);
        isPortalOpen = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isPortalOpen && collider.gameObject.CompareTag("Portal"))
        {
            EnterPortal();
        }
    }

    void EnterPortal()
    {
        Debug.Log("You win!");

        if (moveScript != null)
        {
            moveScript.enabled = false;
        }

        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}