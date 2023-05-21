using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;
    private float timer;

    private void Update()
    {
        // Перемещение снаряда вперед по направлению
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Увеличение таймера
        timer += Time.deltaTime;

        // Проверка, достиг ли снаряд предельного времени жизни
        if (timer >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Получение ссылки на объект игрока
        heromove player = collision.GetComponent<heromove>();

        // Если столкнулись с игроком, наносим урон и уничтожаем снаряд
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
