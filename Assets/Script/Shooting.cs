using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    Animator playerAnimator;
    public GameObject Bullet;
    public Transform BulletSpawnPoint;
    public float bulletSpeed = 1.5f;
    public int maxBullets = 3;
    public float cooldownTime = 2f;

    private int bulletCount;
    private float cooldownTimer;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        bulletCount = maxBullets;
        cooldownTimer = 0f;
    }

    private void Update()
    {
        if (bulletCount == 0)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                bulletCount = maxBullets;
                cooldownTimer = 0f;
                Debug.Log("Reloaded");
            }
        }
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && bulletCount > 0)
        {
            playerAnimator.SetBool("isShooting", true);
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        float direction = transform.localScale.x > 0 ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * bulletSpeed, rb.linearVelocity.y);

        if (direction == -1)
        {
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x *= -1;
            bullet.transform.localScale = bulletScale;
        }

        Destroy(bullet, 3f);

        bulletCount--;

        Debug.Log("Bullets: " + bulletCount);

        if (bulletCount == 0)
        {
            Debug.Log("Reloading...");
        }

        StartCoroutine(ResetShooting());
    }

    IEnumerator ResetShooting()
    {
        yield return new WaitForSeconds(3f);
        playerAnimator.SetBool("isShooting", false);
    }
}