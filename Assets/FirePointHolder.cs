using UnityEngine;

public class FirePointHolder : MonoBehaviour
{
    private heromove hero;

    private void Start()
    {
        
    }

    private void Update()
    {
        hero = GetComponent<heromove>();
        // �������� ������� ������� �������
        Vector3 scale = transform.localScale;
        Debug.Log("Scale: " + scale);

        if (hero.moveVector.x < 0)
        {
            // ������ ������� �� ������������� �� ��� X
            transform.localScale = new Vector3(Mathf.Abs(scale.x) * -1f, scale.y, scale.z);
        }
        else if (hero.moveVector.x > 0)
        {
            // ������ ������� �� ������������� �� ��� X
            transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }
}
