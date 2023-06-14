using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] private float targetX; // Целевая координата X для перемещения игрока
    [SerializeField] private float targetY; // Целевая координата Y для перемещения игрока
    [SerializeField] private float targetZ; // Целевая координата Z для перемещения игрока

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(targetX, targetY, targetZ);
        }
    }
}
