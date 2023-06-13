using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] float height; // ������ ������� �������
    [SerializeField] float speed; // �������� ������� �������

    private bool isTriggered = false; // ����, �����������, ����������� �� �������
    private bool isPlayerOnLift = false; // ����, �����������, ��������� �� ����� �� �����
    private Vector3 targetPosition; // ������� ������� ������� �������

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
            isTriggered = true; // ��������� ����� ��������� ��������
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
