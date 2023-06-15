using UnityEngine;

public class TeleportationObject : MonoBehaviour
{
    [SerializeField] private float targetX; // ������� ���������� X ��� ����������� ������
    [SerializeField] private float targetY; // ������� ���������� Y ��� ����������� ������
    [SerializeField] private float targetZ; // ������� ���������� Z ��� ����������� ������
    public Animator anim;
    private bool isTriggered = false; // ����, �����������, ����������� �� �������

    private void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.T)) // �������� KeyCode �� ������ ��� ������
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
