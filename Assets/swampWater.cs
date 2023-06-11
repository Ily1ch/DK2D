using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swampWater : MonoBehaviour
{
    public int speedReduction = 5; // Значение, на которое будет снижаться скорость игрока
    public int damageAmount = 10; // Количество урона, наносимого игроку
    public float damagePeriod = 1f; // Периодичность нанесения урона

    private heromove player;
    private float nextDamageTime;

    private void Start()
    {
        nextDamageTime = Time.time + damagePeriod; // Устанавливаем следующее время нанесения урона сразу после старта
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<heromove>(); // Получаем компонент heromove игрока
            player.speed -= speedReduction; // Уменьшаем скорость игрока
            enabled = true; // Включаем скрипт нанесения урона
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.speed += speedReduction; // Восстанавливаем скорость игрока
            player = null;
            enabled = false; // Выключаем скрипт нанесения урона
        }
    }

    private void Update()
    {
        if (Time.time >= nextDamageTime && player != null)
        {
            player.TakeDamage(damageAmount); // Наносим урон игроку
            nextDamageTime = Time.time + damagePeriod; // Обновляем время следующего нанесения урона
        }
    }
}
