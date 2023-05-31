using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 10; // ����, ��������� ����
    public float damageInterval = 1f; // �������� ��������� �����
    public GameObject target; // ����, ������� ����� ���������� ����

    private bool isTargetInRange; // ���� ���������� ���� � ���� ��������

    private float damageTimer; // ������ ��� ������������ ��������� ��������� �����

    private void Update()
    {
        // ���� ���� ��������� � ���� �������� � ������ ���������� ������� � ���������� ��������� �����
        if (isTargetInRange && damageTimer >= damageInterval)
        {
            damageTimer = 0f; // ����� �������

            // ������� ���� ����
            if (target != null)
            {
                heromove health = target.GetComponent<heromove>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }

        damageTimer += Time.deltaTime; // ���������� ������� ��������� �����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            isTargetInRange = true; // ���� ��������� � ���� ��������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            isTargetInRange = false; // ���� �������� ���� ��������
        }
    }
}
