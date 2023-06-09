using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public string itemText; // Текст, который будет отображаться при активации триггера

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowItemText(itemText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideItemText();
        }
    }
}
