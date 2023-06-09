using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject itemTextPanel;
    public Text itemTextUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowItemText(string itemText)
    {
        itemTextUI.text = itemText;
        itemTextPanel.SetActive(true);
    }

    public void HideItemText()
    {
        itemTextPanel.SetActive(false);
    }
}
