using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] float height; // Высота подъема объекта
    [SerializeField] float speed; // Скорость подъема объекта

    private bool isTriggered = false; // Флаг, указывающий, активирован ли триггер
    private bool isPlayerOnLift = false; // Флаг, указывающий, находится ли игрок на лифте
    private Vector3 targetPosition; // Целевая позиция подъема объекта

    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

    private void Update()
    {
        if (isTriggered && !isPlayerOnLift)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnLift = true;
            isTriggered = true; // Установка флага активации триггера
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnLift = false;
        }
    }
}
