using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallTrigger : MonoBehaviour
{
    public GameObject targetObject; // ������� ������ ��� ���������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(true); // ���������� ������� ������
        }
    }
}
