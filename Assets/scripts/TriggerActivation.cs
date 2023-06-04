using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    public GameObject targetObject; // Объект, который нужно включить

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Замените "Player" на тег объекта, который должен касаться триггера
        {
            targetObject.SetActive(true); // Включить объект
        }
    }
}
