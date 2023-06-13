using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] float height; // ������ ������� �������
    [SerializeField] float speed; // �������� ������� �������
    [SerializeField] Collider2D other;
    Vector3 targetPosition;
    private bool isTriggered = false; // ����, �����������, ����������� �� �������
    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }
    void Update()
    {
        if (isTriggered)
        {
           
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            
            // ����������� �������� Y ������� �������
           // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, transform.position.y, targetHeight), transform.position.z);
        }
        
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true; // ��������� ����� ��������� ��������
        }
        else
        {
            isTriggered = false; // ��������� ����� ��������� ��������
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
    }
}
