using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    public GameObject targetObject; // ������, ������� ����� ��������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �������� "Player" �� ��� �������, ������� ������ �������� ��������
        {
            targetObject.SetActive(true); // �������� ������
        }
    }
}
