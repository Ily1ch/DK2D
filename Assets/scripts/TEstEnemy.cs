using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEstEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer=Mathf.Infinity;
    private heromove currentPlayerHp;
    private Animator anim;

    public float currentHealth;
    public float timeToDie = 0;
    public float detectionRadius;
    public float speed ;
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
        {
            FollowPlayer(distanceToPlayer);
            FlipSpriteTowardsPlayer();
        }
        else
        {
            anim.SetBool("walk",false);
        }
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetBool("walk", false);
                anim.Play("attacking");
                //anim.StopPlayback();

                //anim.SetBool("attacking 0",true);
            }
        } 
    }

    private bool PlayerInSight()
    {
        
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x*colliderDistance, 
            new Vector3( boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {

            currentPlayerHp = hit.transform.GetComponent<heromove>();
        }
        return hit.collider!=null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x*colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
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

    //public float attackRange = 1f;
    //public int attackDamage = 10;
    //public float attackDelay = 1f;

    //private float lastAttackTime = 0f;
    //private GameObject player;

    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}

    //void Update()
    //{
    //    if (CanAttack())
    //    {
    //        Attack();
    //    }
    //}

    //bool CanAttack()
    //{
    //    if (Time.time > lastAttackTime + attackDelay)
    //    {
    //        float distance = Vector2.Distance(transform.position, player.transform.position);
    //        return distance < attackRange;
    //    }
    //    return false;
    //}

    //void Attack()
    //{
    //    lastAttackTime = Time.time;
    //    Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange);
    //    foreach (Collider2D hitObject in hitObjects)
    //    {
    //        if (hitObject.CompareTag("Player"))
    //        {
    //            heromove playerHealth = hitObject.GetComponent<heromove>();
    //            if (playerHealth != null)
    //            {
    //                playerHealth.TakeDamage(attackDamage);
    //            }
    //        }
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    var t = new Vector2(transform.position.x + 2, transform.position.y) ;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
