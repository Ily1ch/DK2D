using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    private float cooldownTimer = Mathf.Infinity;
    private heromove currentPlayerHp;
    private Animator anim;

    public float currentHealth;
    public float timeToDie;
    public float detectionRadius;
    public float speed;
    public float distanceStopToAttack;
    public Transform player;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (IsPlayerDetected(distanceToPlayer))
            FlipSpriteTowardsPlayer();

        if (IsPlayerDetected(distanceToPlayer) && distanceToPlayer > distanceStopToAttack)
            FollowPlayer(distanceToPlayer);
        else
            anim.SetBool("walk", false);

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetBool("walk", false);
                anim.SetTrigger("attacking");

                // Spawn projectile
                Vector2 direction = player.position - transform.position;
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, range, playerLayer);

        if (hit.collider != null)
        {
            currentPlayerHp = hit.transform.GetComponent<heromove>();
        }

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            currentPlayerHp.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("takedamage");

        if (currentHealth <= 0)
            Die();
        else
        {
            StartCoroutine(TemporarySpeedReduction());
        }
    }

    private IEnumerator TemporarySpeedReduction()
    {
        speed = 0f;
        yield return new WaitForSeconds(2f);
        speed = 3f;
    }

    private void Die()
    {
        speed = 0;
        anim.SetTrigger("die");
        Destroy(gameObject, timeToDie);
    }

    private bool IsPlayerDetected(float distanceToPlayer)
    {
        return distanceToPlayer <= detectionRadius;
    }

    private void FollowPlayer(float distanceToPlayer)
    {
        anim.SetBool("walk", true);
        Vector2 direction = player.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void FlipSpriteTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        bool shouldFaceRight = (direction.x > 0);

        if (shouldFaceRight != isFacingRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            isFacingRight = shouldFaceRight;
        }
    }
}
