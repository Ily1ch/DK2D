using UnityEngine;



public class TriggerActivation : MonoBehaviour
{
    public GameObject targetObject;
    public BossScript1 bossScript;

        private bool hasBeenActivated; // Флаг активации объекта

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") && !hasBeenActivated)
        {
            hasBeenActivated = true; // Устанавливаем флаг активации объекта

            targetObject.SetActive(true); // Включаем объект

            // Проверяем здоровье босса
            if (BossScript1.curr <= 0)
            {
                targetObject.SetActive(false); // Выключаем объект
            }
        }
    }
}


