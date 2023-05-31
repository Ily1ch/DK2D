using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 10; // Урон, наносимый цели
    public float damageInterval = 1f; // Интервал нанесения урона
    public GameObject target; // Цель, которой будет наноситься урон

    private bool isTargetInRange; // Флаг нахождения цели в зоне действия

    private float damageTimer; // Таймер для отслеживания интервала нанесения урона

    private void Update()
    {
        // Если цель находится в зоне действия и прошло достаточно времени с последнего нанесения урона
        if (isTargetInRange && damageTimer >= damageInterval)
        {
            damageTimer = 0f; // Сброс таймера

            // Нанести урон цели
            if (target != null)
            {
                heromove health = target.GetComponent<heromove>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }

        damageTimer += Time.deltaTime; // Увеличение таймера нанесения урона
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            isTargetInRange = true; // Цель находится в зоне действия
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            isTargetInRange = false; // Цель покинула зону действия
        }
    }
}
