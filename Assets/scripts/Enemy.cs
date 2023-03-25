using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 5.0f;
    public float chaseRange = 5.0f;
    public float stopRange = 1.0f;

    private bool isChasing = false;

    void Start()
    {
        // Находим игрока по тэгу "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return; // игрок не найден, выходим из метода
        }

        // Вычисляем расстояние между врагом и игроком
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            // Поворачиваем врага в сторону игрока
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.right = direction;
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, 0);


            if (distanceToPlayer > stopRange)
            {
                // Двигаем врага в направлении игрока
                direction = (playerTransform.position - transform.position).normalized;
                transform.right = direction;
                transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, 0);

            }
        }
    }
}
