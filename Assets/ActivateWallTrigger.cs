using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallTrigger : MonoBehaviour
{
    public GameObject targetObject; // Целевой объект для активации

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(true); // Активируем целевой объект
        }
    }
}
