using UnityEngine;
using System.Collections;

public class EnemyGhost : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private heromove currentPlayerHp;
    private Animator anim;

    public float currentHealth;
    public float timeToDie = 0;
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
        //anim.SetBool("attacking 0", false);
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
                //anim.StopPlayback();

                //anim.SetBool("attacking 0",true);
            }
        }
    }

    private bool PlayerInSight()
    {

        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {

            currentPlayerHp = hit.transform.GetComponent<heromove>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            currentPlayerHp.TakeDamage(damage);
        }
    }
    public void TakeDamageGhost(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("takedamage");
        if (currentHealth <= 0)
            Die();
        else
        {
            //под сомнением !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            StartCoroutine(TemporarySpeedReduction());
        }
    }
    //под сомнением !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private IEnumerator TemporarySpeedReduction()
    {
        speed = 0f;
        yield return new WaitForSeconds(2f); // ”становите продолжительность задержки по своему усмотрению
        speed = 3f; // ¬осстановите исходное значение скорости
    }

    void Die()
    {
        speed = 0;
        anim.SetTrigger("die");
        Destroy(this.gameObject, timeToDie);
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
            spriteRenderer.flipX = !spriteRenderer.flipX;
            isFacingRight = shouldFaceRight;
        }
    }
}
