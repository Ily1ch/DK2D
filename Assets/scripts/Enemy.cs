using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;  // ������ ����������� ������
    public float speed = 2f;  // �������� �����
    public Transform player;  // ������ �� ������
    private SpriteRenderer spriteRenderer;  // ������ �� ��������� ����������� �������
    private bool isFacingRight = true;  // ���� ����������� �����

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (IsPlayerDetected(distanceToPlayer))
        {
            FollowPlayer(distanceToPlayer);
            FlipSpriteTowardsPlayer();
        }
    }

    private bool IsPlayerDetected(float distanceToPlayer)
    {
        return distanceToPlayer <= detectionRadius;
    }

    private void FollowPlayer(float distanceToPlayer)
    {
        Vector2 direction = player.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void FlipSpriteTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        bool shouldFaceRight = (direction.x > 0);
        if (shouldFaceRight != isFacingRight)
        {
            spriteRenderer.flipX = shouldFaceRight;
            isFacingRight = shouldFaceRight;
        }
    }
}
