using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTargetDestroyed : MonoBehaviour
{
    public GameObject targetObject; // Целевой объект, уничение которого вызывает уничтожение текущего объекта

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned!");
            enabled = false; // Отключаем скрипт, если не задан целевой объект
        }
    }

    private void Update()
    {
        if (targetObject == null)
        {
            Destroy(gameObject); // Уничтожаем текущий объект, если целевой объект был уничтожен
        }
    }
}
