using UnityEngine;

public class TeleportationObject : MonoBehaviour
{
    [SerializeField] private float targetX; // Целевая координата X для перемещения игрока
    [SerializeField] private float targetY; // Целевая координата Y для перемещения игрока
    [SerializeField] private float targetZ; // Целевая координата Z для перемещения игрока
    public Animator anim;
    private bool isTriggered = false; // Флаг, указывающий, активирован ли триггер

    private void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.T)) // Измените KeyCode на нужную вам кнопку
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            anim.SetBool("StartOpen", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
            anim.SetBool("StartOpen", false);
        }
    }

    private void TeleportPlayer()
    {
        Vector3 newPosition = new Vector3(targetX, targetY, targetZ);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = newPosition;
        }
    }
}
