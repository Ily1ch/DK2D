using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextTriggerIvent : MonoBehaviour
{
    public GameObject textMeshPro;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textMeshPro.gameObject.SetActive(true); // Включаем компонент TextMeshProUGUI
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textMeshPro.gameObject.SetActive(false); // Выключаем компонент TextMeshProUGUI
        }
    }
}
