using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRadius = 5f; // Радиус обнаружения игрока
    public float attackRange = 1f; // Дальность атаки
    public float moveSpeed = 2f; // Скорость передвижения врага
    public int attackPower = 10; // Сила удара врага
    private GameObject player; // Ссылка на игрока
    private bool playerDetected = false; // Флаг, обозначающий, обнаружен ли игрок
    private bool playerInRange = false; // Флаг, обозначающий, находится ли игрок в зоне атаки

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Находим игрока по тегу
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position); // Вычисляем расстояние до игрока

        // Если игрок находится в зоне обнаружения
        if (distanceToPlayer < detectionRadius)
        {
            playerDetected = true;

            // Если игрок находится в зоне атаки
            if (distanceToPlayer < attackRange)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        }
        else
        {
            playerDetected = false;
            playerInRange = false;
        }

        // Если игрок обнаружен
        if (playerDetected)
        {
            // Сближаемся с игроком
            Vector2 targetPosition = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Если игрок в зоне атаки, атакуем его
            //if (playerInRange)
            //{
            //    Attack();
            //}
        }
    }

    //void Attack()
    //{
    //    // Наносим игроку урон
    //    player.GetComponent<PlayerController>().TakeDamage(attackPower);
    //}
}
