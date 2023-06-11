using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZone : MonoBehaviour
{
    public int healthAmount = 20; // ���������� �������� ��� ��������������
    public float healingPeriod = 1f; // ������ �������������� ��������

    private heromove playerHealth;
    private float nextHealTime;

    private void Start()
    {
        playerHealth = FindObjectOfType<heromove>(); // ������� ��������� heromove � �����
        nextHealTime = Time.time; // ������������� ��������� ����� �������������� ����� ����� ������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������� �������������� �������� ������ ����� ����� ������ � ���� ��������
            enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ������������� �������������� �������� ����� ����� �������� ���� ��������
            enabled = false;
        }
    }

    private void Update()
    {
        if (enabled && Time.time >= nextHealTime && playerHealth != null)
        {
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.currentHealth += healthAmount; // ��������������� �������� ������
                if (playerHealth.currentHealth > playerHealth.maxHealth)
                    playerHealth.currentHealth = playerHealth.maxHealth; // ������������ �������� ������������ ���������
                nextHealTime = Time.time + healingPeriod; // ��������� ����� ���������� ��������������
            }
        }
    }
}
