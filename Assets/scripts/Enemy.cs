using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target; // ����, �� ������� ����� ������������ ����
    public float speed = 5f; // �������� ������������ �����
    public float minDistance = 2f; // ����������� ����������, ��� ������� ���� �������� ������������ ������

    private bool isFollowing = false; // ����, �����������, ������� �� ���� �� �������

    private void Update()
    {
        if (target == null)
        {
            return; // ���� ���� �����������, �� ���� ������ �� ������
        }

        float distance = Vector3.Distance(transform.position, target.position); // ��������� ���������� �� ����

        if (distance <= minDistance)
        {
            isFollowing = true; // ���� ����� ��������� � ���� �������������, �� ���� �������� ��������� �� ���
        }

        if (isFollowing)
        {
            Vector3 direction = target.position - transform.position; // ��������� ����������� � ����
            direction.Normalize(); // ����������� �����������, ����� �������� ������������ �� �������� �� ���������� �� ����
            transform.position += direction * speed * Time.deltaTime; // ���������� ����� � ����������� ����
        }

        if (distance > minDistance * 2f)
        {
            isFollowing = false; // ���� ����� ������� �� ������� ���� �������������, �� ���� ���������������
        }
    }
}
