using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] private float targetX; // ������� ���������� X ��� ����������� ������
    [SerializeField] private float targetY; // ������� ���������� Y ��� ����������� ������
    [SerializeField] private float targetZ; // ������� ���������� Z ��� ����������� ������
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            anim.SetBool("StartOpen", true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("StartOpen", false);
        }
    }


    public void Teleportation(Collider2D other)
    {
        other.transform.position = new Vector3(targetX, targetY, targetZ);
    }
}


//if (other.CompareTag("Player"))
//{
//    other.transform.position = new Vector3(targetX, targetY, targetZ);
//}
