using UnityEngine;



public class TriggerActivation : MonoBehaviour
{
    public GameObject targetObject;
    public BossScript1 bossScript;

        private bool hasBeenActivated; // ���� ��������� �������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") && !hasBeenActivated)
        {
            hasBeenActivated = true; // ������������� ���� ��������� �������

            targetObject.SetActive(true); // �������� ������

            // ��������� �������� �����
            if (BossScript1.curr <= 0)
            {
                targetObject.SetActive(false); // ��������� ������
            }
        }
    }
}


