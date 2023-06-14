using UnityEngine;

public class ElevatingObject : MonoBehaviour
{
    [SerializeField] private float targetX; // Целевая координата X для перемещения игрока
    [SerializeField] private float targetY; // Целевая координата Y для перемещения игрока
    [SerializeField] private float targetZ; // Целевая координата Z для перемещения игрока
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
