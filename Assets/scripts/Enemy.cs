using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float speed = 2f;
    public Transform player;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    public int enemyAttackDamage = 5;
    public Animator anim;
    public Transform attackPoint;
    public LayerMask Player;
    public float attackRange;
    public float attackCooldown = 2f; // время между атаками
    private bool canAttack = true; // флаг, позволяющий атаковать или нет
    public float currentHealth;
    public float MaxHealth = 100f;

    void Start()
    {
        currentHealth = MaxHealth;
        anim = GetComponent<Animator>();
        anim.Play("static");
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (IsPlayerDetected(distanceToPlayer))
        {
            FollowPlayer(distanceToPlayer);
            FlipSpriteTowardsPlayer();

            // проверяем, можно ли атаковать
            if (canAttack && Vector2.Distance(player.transform.position, transform.position) <= attackRange)
            {
                StartCoroutine(AttackCooldown()); // запускаем корутину с задержкой
                Attack();
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // запрещаем атаку на время задержки
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // разрешаем атаковать снова
    }

    private bool IsPlayerDetected(float distanceToPlayer)
    {
        return distanceToPlayer <= detectionRadius;
    }

    private void FollowPlayer(float distanceToPlayer)
    {
        Vector2 direction = player.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void FlipSpriteTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        bool shouldFaceRight = (direction.x > 0);
        if (shouldFaceRight != isFacingRight)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            isFacingRight = shouldFaceRight;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(this.gameObject, 0.5f);
    }

    void Attack()
    {
        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, Player);
        if (hitEnemies != null)
        {
            hitEnemies.GetComponent<heromove>().TakeDamage(enemyAttackDamage);
        }
    }
}
