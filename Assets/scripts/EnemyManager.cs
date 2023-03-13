using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;        // The array of game objects with the "Enemy" tag
    public string[] enemyNames;         // The array of string variables to store the enemy names

    // This method is called when the script is enabled
    public void Control()
    {
        // Find all game objects with the "Enemy" tag and add them to the enemies array
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Create a new string array with the same size as the enemies array
        enemyNames = new string[enemies.Length];

        // Loop through each enemy in the enemies array
        for (int i = 0; i < enemies.Length; i++)
        {
            // Get the name of the current enemy and add it to the enemyNames array at the same index
            enemyNames[i] = enemies[i].name;
        }
    }
    public void Destroyer()
    {   
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].transform.parent.gameObject);
        }
    }
}
