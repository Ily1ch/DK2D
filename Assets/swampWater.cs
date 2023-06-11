using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swampWater : MonoBehaviour
{
    public int speedReduction = 5; // ��������, �� ������� ����� ��������� �������� ������
    public int damageAmount = 10; // ���������� �����, ���������� ������
    public float damagePeriod = 1f; // ������������� ��������� �����

    private heromove player;
    private float nextDamageTime;

    private void Start()
    {
        nextDamageTime = Time.time + damagePeriod; // ������������� ��������� ����� ��������� ����� ����� ����� ������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<heromove>(); // �������� ��������� heromove ������
            player.speed -= speedReduction; // ��������� �������� ������
            enabled = true; // �������� ������ ��������� �����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.speed += speedReduction; // ��������������� �������� ������
            player = null;
            enabled = false; // ��������� ������ ��������� �����
        }
    }

    private void Update()
    {
        if (Time.time >= nextDamageTime && player != null)
        {
            player.TakeDamage(damageAmount); // ������� ���� ������
            nextDamageTime = Time.time + damagePeriod; // ��������� ����� ���������� ��������� �����
        }
    }
}
