using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;
    private float timer;

    private void Update()
    {
        // ����������� ������� ������ �� �����������
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // ���������� �������
        timer += Time.deltaTime;

        // ��������, ������ �� ������ ����������� ������� �����
        if (timer >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��������� ������ �� ������ ������
        heromove player = collision.GetComponent<heromove>();

        // ���� ����������� � �������, ������� ���� � ���������� ������
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
