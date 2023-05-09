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
    public float attackCooldown = 2f; // ����� ����� �������
    private bool canAttack = true; // ����, ����������� ��������� ��� ���
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

            // ���������, ����� �� ���������
            if (canAttack && Vector2.Distance(player.transform.position, transform.position) <= attackRange)
            {
                StartCoroutine(AttackCooldown()); // ��������� �������� � ���������
                Attack();
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // ��������� ����� �� ����� ��������
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // ��������� ��������� �����
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
