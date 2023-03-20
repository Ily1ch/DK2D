using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    public float detectionRange = 10f; // ����������, �� ������� ���� �������� ������
    public float attackRange = 0.5f; // ����������, �� ������� ���� ����� ��������� ������
    public float movementSpeed = 5f; // �������� ������������ �����
    public int attackDamageEnemy = 5; // ���� �� ����� �����

    private Transform hero; // ������� ������
    private NavMeshAgent agent; // ��������� ��� ������������ �� �������


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        hero = GameObject.FindGameObjectWithTag("Player").transform; // �������� ������� ������ �� ���� "Player"
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, hero.position); // ��������� ���������� �� ������

        if (distance <= detectionRange) // ���� ����� ��������� � ���� ��������� �����
        {
            Vector3 direction = (hero.position - transform.position).normalized; // ��������� ����������� � ������
            transform.Translate(direction * movementSpeed * Time.deltaTime); // ����������� ����� � ����������� ������

            if (distance <= attackRange) // ���� ����� ��������� � ���� ����� �����
            {
                Attack(); // ������� ������
            }
        }
    }


    public Transform attackPointEnemy;
    public LayerMask Player;
    void Attack()
    {
        // � ������ ������ �� ������ ������� ��������� � �������, �� ����� ����� ���� ����� ������ ������ ����� ������

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


