using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Linq;

public class heromove : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 1;
    [SerializeField] private TMPro.TextMeshProUGUI info;
    public HealthBar healthBar;
    public VectorValue pos;
    public AudioSource hitSound;
    public AudioSource Jump1;
    public AudioSource shagplayer;
    public AudioSource SwordSound;
    public AudioSource somersault;
    public AudioSource dash;
    public AudioSource FireBall;
    public RuntimeAnimatorController playerController;
    public RuntimeAnimatorController style2PlayerController;
    private RuntimeAnimatorController activeController;

    public float attackCooldown = 0.5f;
    private float lastAttackTime = -Mathf.Infinity;

    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        transform.position = pos.initialValue;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activeController = playerController;
        anim.runtimeAnimatorController = activeController;
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            RefreshInfo();
        else
            if (Input.GetKeyUp(KeyCode.Tab))
            info.gameObject.transform.parent.gameObject.SetActive(false);
        Walk();
        Reflect();
        Jump();
        CheckingGround();
        Dash();
        Somersault();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        if (currentHealth < maxHealth)
        {
            RegenerateHealth();
        }
        healthBar.SetHealth(currentHealth);

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchController();
        }
    }
    private void RefreshInfo()
    {
        info.gameObject.transform.parent.gameObject.SetActive(true);
        info.text = this.ToString();
    }
    private void SwitchController()
    {
        if (activeController == playerController)
        {
            anim.runtimeAnimatorController = style2PlayerController;
            activeController = style2PlayerController;
        }
        else
        {
            anim.runtimeAnimatorController = playerController;
            activeController = playerController;
        }
    }
    //-------�� �����------
    public float TimeDelay = 2;
    public float TimeDelayHP;
    void RegenerateHealth()
    {
        TimeDelayHP += Time.deltaTime;
        if (TimeDelayHP >= TimeDelay)
        {
            currentHealth++;
            TimeDelayHP = 0;
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
    //walk and Reflect
    public Vector2 moveVector;
    public int speed = 3;
    public bool faceRight = true;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // Поворот персонажа по оси X

        // Поворот attackPoint вместе с персонажем
        attackPoint.localScale = new Vector3(-attackPoint.localScale.x, attackPoint.localScale.y, attackPoint.localScale.z);
    }



    //Jump
    public int jumpCount = 0;
    public int maxJumpValue = 2;
    public int jumpForce = 10;
    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && (onGround || (++jumpCount < maxJumpValue)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (onGround) { jumpCount = 0; }
    }

    //CheckingGround
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }


    //Dash
    public int dashForce = 1000;
    public float dashCooldown = 1f;
    private float lastDashTime = -Mathf.Infinity;
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            anim.StopPlayback();
            anim.Play("dash");

            rb.velocity = Vector2.zero;

            if (!faceRight)
            {
                rb.AddForce(Vector2.left * dashForce);
            }
            else
            {
                rb.AddForce(Vector2.right * dashForce);
            }

            lastDashTime = Time.time;
        }
    }
    //Somersault
    public int SomersaultForce = 1000;
    public float SomersaultCooldown = 1f;
    private float lastSomersaultTime = -Mathf.Infinity;
    void Somersault()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > lastSomersaultTime + SomersaultCooldown)
        {
            anim.StopPlayback();
            anim.Play("Somersault");

            rb.velocity = Vector2.zero;

            if (!faceRight)
            {
                rb.AddForce(Vector2.left * SomersaultForce);
            }
            else
            {
                rb.AddForce(Vector2.right * SomersaultForce);
            }

            lastSomersaultTime = Time.time;
        }
    }
    //Takedamage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hitSound.Play();
        //healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            Die();


    }
    void Die()
    {
        //animator.Play("Die");
        //animator.SetBool("Die", true);
        //Destroy(this.gameObject, 0.5f);
        //SceneManager.LoadScene(1);
        LoadPlayer();
    }

    //Attack
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask EnemyGhost;
    public LayerMask breakableWall;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    void Attack()
    {
        anim.SetTrigger("Attack");
        SwordSound.Play();
        Collider2D[] hitEnemiesGhost = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] breakableWalls = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, breakableWall);

        Collider2D[] allEnemies = hitEnemiesGhost.Concat(hitEnemies).ToArray();

        foreach (Collider2D enemy in allEnemies)
        {
            EnemyGhost enemyGhost = enemy.GetComponent<EnemyGhost>();
            TEstEnemy enemyTest = enemy.GetComponent<TEstEnemy>();

            if (enemyGhost != null)
            {
                enemyGhost.TakeDamageGhost(attackDamage);
            }
            else if (enemyTest != null)
            {
                enemyTest.TakeDamage(attackDamage);
            }
        }

        foreach (Collider2D wall in breakableWalls)
        {
            BreakableWall breakable = wall.GetComponent<BreakableWall>();
            if (breakable != null)
            {
                breakable.TakeDamageWall(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public override string ToString()
    {
        return $"Скорость: {speed}\nСила атаки: {attackDamage}\nМаксимально хп: {maxHealth} \nТекущее хп: {currentHealth}";
    }
    public void PlayJumpSound()
    {
        Jump1.Play();
    }
    public void PlaySomersaultSound()
    {
        somersault.Play();
    }
    public void PlayDashSound()
    {
        dash.Play();
    }
    public void PlayFireBallSound()
    {
        FireBall.Play();
    }
    public void PlayFootstepSound()
    {
        shagplayer.Play();
    }
    //SaveSystem

    public void SavePlayer() //для кнопки сохранить
    {
        SaveSystem.SavePlayer(this);
    }


    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.currentHealth;
        maxHealth = data.maxHealth;
        attackDamage = data.attackDamage;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }
}
