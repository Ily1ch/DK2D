using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTargetDestroyed : MonoBehaviour
{
    public GameObject targetObject; // ������� ������, �������� �������� �������� ����������� �������� �������

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned!");
            enabled = false; // ��������� ������, ���� �� ����� ������� ������
        }
    }

    private void Update()
    {
        if (targetObject == null)
        {
            Destroy(gameObject); // ���������� ������� ������, ���� ������� ������ ��� ���������
        }
    }
}
