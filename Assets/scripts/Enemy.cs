using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;  // Радиус обнаружения игрока
    public float speed = 2f;  // Скорость врага
    public Transform player;  // Ссылка на игрока
    private SpriteRenderer spriteRenderer;  // Ссылка на компонент отображения спрайта
    private bool isFacingRight = true;  // Флаг направления врага
    public int enemyAttackDamage = 5;
    public Animator anim;
    public Transform attackPoint;
    public LayerMask Player;
    public float attackRange;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("static");
        currentHealth = MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        Distance();
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (IsPlayerDetected(distanceToPlayer))
        {
            FollowPlayer(distanceToPlayer);
            FlipSpriteTowardsPlayer();
        }
      
        
    }
    void OnDrawGizmosSelected()
    {
        if (  attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
        if (shouldFaceRight == isFacingRight)
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
    void Distance()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= attackRange)
            Atack();
    }
    void Atack()
    {
       // anim.SetTrigger("Attack");

        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, Player);


        hitEnemies.GetComponent<heromove>().TakeDamage(enemyAttackDamage);
        
       
    }

}
