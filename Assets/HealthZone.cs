using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZone : MonoBehaviour
{
    public int healthAmount = 20; // Количество здоровья для восстановления
    public float healingPeriod = 1f; // Период восстановления здоровья

    private heromove playerHealth;
    private float nextHealTime;

    private void Start()
    {
        playerHealth = FindObjectOfType<heromove>(); // Находим компонент heromove в сцене
        nextHealTime = Time.time; // Устанавливаем следующее время восстановления сразу после старта
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Запускаем восстановление здоровья только когда игрок входит в зону триггера
            enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Останавливаем восстановление здоровья когда игрок покидает зону триггера
            enabled = false;
        }
    }

    private void Update()
    {
        if (enabled && Time.time >= nextHealTime && playerHealth != null)
        {
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.currentHealth += healthAmount; // Восстанавливаем здоровье игрока
                if (playerHealth.currentHealth > playerHealth.maxHealth)
                    playerHealth.currentHealth = playerHealth.maxHealth; // Ограничиваем здоровье максимальным значением
                nextHealTime = Time.time + healingPeriod; // Обновляем время следующего восстановления
            }
        }
    }
}
