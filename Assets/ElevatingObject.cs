using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] private float targetX; // ������� ���������� X ��� ����������� ������
    [SerializeField] private float targetY; // ������� ���������� Y ��� ����������� ������
    [SerializeField] private float targetZ; // ������� ���������� Z ��� ����������� ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(targetX, targetY, targetZ);
        }
    }
}
