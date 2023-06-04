using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public int attackDamage;
    public int maxHealth;
    public float[] position;

    public PlayerData(heromove player)
    {
        currentHealth = player.currentHealth;
        maxHealth = player.maxHealth;
        attackDamage = player.attackDamage;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
