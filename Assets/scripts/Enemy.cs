using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    public float detectionRange = 10f; // расстояние, на котором враг замечает игрока
    public float attackRange = 0.5f; // расстояние, на котором враг может атаковать игрока
    public float movementSpeed = 5f; // скорость передвижения врага
    public int attackDamageEnemy = 5; // урон от атаки врага

    private Transform hero; // позиция игрока
    private NavMeshAgent agent; // компонент для передвижения по навмешу


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        hero = GameObject.FindGameObjectWithTag("Player").transform; // получаем позицию игрока по тегу "Player"
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, hero.position); // вычисляем расстояние до игрока

        if (distance <= detectionRange) // если игрок находится в зоне видимости врага
        {
            Vector3 direction = (hero.position - transform.position).normalized; // вычисляем направление к игроку
            transform.Translate(direction * movementSpeed * Time.deltaTime); // передвигаем врага в направлении игрока

            if (distance <= attackRange) // если игрок находится в зоне атаки врага
            {
                Attack(); // атакуем игрока
            }
        }
    }


    public Transform attackPointEnemy;
    public LayerMask Player;
    void Attack()
    {
        // в данном случае мы просто выведем сообщение в консоль, но здесь может быть любая другая логика атаки игрока

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointEnemy.position, attackRange, Player);

        foreach (Collider2D hero in hitEnemies)
        {
            hero.GetComponent<heromove>().TakeDamage(attackDamageEnemy);
        }
        Debug.Log("Enemy attacked player for " + attackDamageEnemy + " damage.");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPointEnemy == null)
            return;
        Gizmos.DrawWireSphere(attackPointEnemy.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //animator.Play("isDamaged");
        //animator.SetTrigger("isDamaged");
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        //animator.Play("Die");
        //animator.SetBool("Die", true);
        Destroy(this.gameObject,0.5f);
    }
}
//public Transform Player;
//private Rigidbody2D rb;
//private Vector2 movement;
//private int speed = 5;
//public float chaseDistance = 5f;

//void Start()
//{
//    currentHealth = maxHealth;
//    rb = this.GetComponent<Rigidbody2D>();
//}

//void Update()
//{
//    float distance = Vector2.Distance(transform.position, Player.position);

//    if (distance < chaseDistance)
//    {
//        Vector3 direction = Player.position - transform.position;
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//        direction.Normalize();
//        movement = direction;
//    }
//    else
//    {
//        movement = Vector2.zero;
//    }
//}

//private void FixedUpdate()
//{
//    MoveChar(movement);
//}

//private void MoveChar(Vector2 direction)
//{
//    rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
//}


