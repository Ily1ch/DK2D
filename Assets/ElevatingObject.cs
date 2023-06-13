using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] float height; // Высота подъема объекта
    [SerializeField] float speed; // Скорость подъема объекта
    [SerializeField] Collider2D other;
    Vector3 targetPosition;
    private bool isTriggered = false; // Флаг, указывающий, активирован ли триггер
    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }
    void Update()
    {
        if (isTriggered)
        {
           
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            
            // Ограничение значения Y позиции объекта
           // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, transform.position.y, targetHeight), transform.position.z);
        }
        
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true; // Установка флага активации триггера
        }
        else
        {
            isTriggered = false; // Установка флага активации триггера
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
    }
}
