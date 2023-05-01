using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    private bool invetoryOn;

    private void Start()
    {
        invetoryOn = false;
    }
    public void inventori()
    {
        if (invetoryOn == false)
        {
            invetoryOn = true;
            inventory.SetActive(true);
        }
        else if (invetoryOn == true)
        {
            invetoryOn = false;
            inventory.SetActive(false);
        }
    }
}
