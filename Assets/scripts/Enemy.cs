using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;  // Радиус обнаружения игрока
    public float speed = 2f;  // Скорость врага
    public Transform player;  // Ссылка на игрока
    private SpriteRenderer spriteRenderer;  // Ссылка на компонент отображения спрайта
    private bool isFacingRight = true;  // Флаг направления врага

    void Start()
    {
        currentHealth = MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (IsPlayerDetected(distanceToPlayer))
        {
            FollowPlayer(distanceToPlayer);
            FlipSpriteTowardsPlayer();
        }
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
            spriteRenderer.flipX = shouldFaceRight;
            isFacingRight = shouldFaceRight;
        }
    }
    public float currentHealth;
    public float MaxHealth = 100f;
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        Destroy(this.gameObject, 0.5f);
    }
}
