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
    public AudioSource hitSound;
    public AudioSource SwordSound;
    public float currentHealth;
    public float timeToDie = 0;
    public float detectionRadius;
    public float speed ;
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
        if (IsPlayerDetected(distanceToPlayer)&&distanceToPlayer > distanceStopToAttack)
            FollowPlayer(distanceToPlayer);
        else
            anim.SetBool("walk",false);

        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetBool("walk", false);
                SwordSound.Play();
                anim.SetTrigger("attacking");
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
        hitSound.Play();
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
        yield return new WaitForSeconds(2f); // Установите продолжительность задержки по своему усмотрению
        speed = 3f; // Восстановите исходное значение скорости
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
        Vector2 movement = new Vector2(direction.normalized.x * speed * Time.deltaTime, 0f); // Движение только по оси X
        transform.Translate(movement);
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

            Debug.Log("New Scale X: " + transform.localScale.x); // Отладочный вывод
            isFacingRight = shouldFaceRight;
        }
    }

   
}
