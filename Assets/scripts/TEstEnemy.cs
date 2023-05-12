using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEstEnemy : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 10;
    public float attackDelay = 1f;

    private float lastAttackTime = 0f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (CanAttack())
        {
            Attack();
        }
    }

    bool CanAttack()
    {
        if (Time.time > lastAttackTime + attackDelay)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            return distance < attackRange;
        }
        return false;
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hitObject in hitObjects)
        {
            if (hitObject.CompareTag("Player"))
            {
                heromove playerHealth = hitObject.GetComponent<heromove>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var t = new Vector2(transform.position.x + 2, transform.position.y) ;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
