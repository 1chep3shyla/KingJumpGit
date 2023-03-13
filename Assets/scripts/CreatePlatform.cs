using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private GameObject[] Goldprefab;
    [SerializeField] private Vector3 startPosition; // The starting position of the first object
    [SerializeField] private float yOffset = 2.8f; // The amount to offset the Y position by for each new object
    public GameObject GridContinue;
    public int intCountGrid;
    public int CountGrid;
    public GameObject[] allGrids;
    public GameLogic playerLogic;
    private GameObject lastObject; // The last object that was created
    public SkinManager skin;



    public void CreateNewObject()
    {
        int i = 0;
        int goldIs = Random.Range(0, 101);
        if (skin.whichOn[0] == true)
        {
            i = Random.Range(0, 2);
        }
        else if (skin.whichOn[1] == true)
        {

        }
        else if (skin.whichOn[2] == true)
        {
            i = Random.Range(2, 4);
        }
        else if (skin.whichOn[3] == true)
        {
            i = Random.Range(4, 6);
        }
        CountGrid++;
        Vector3 newPosition = lastObject.transform.position + new Vector3(0, yOffset, 0); // Calculate the new position
        if (goldIs != 100)
        {
            lastObject = Instantiate(prefab[i], newPosition, Quaternion.identity); // Instantiate the first object
        }
        else
        {
            lastObject = Instantiate(Goldprefab[i], newPosition, Quaternion.identity); // Instantiate the first object
        }
        if (CountGrid > 1)
        {
            CountGrid = 0;
            ControlGrid();
            GameObject copyGrid = Instantiate(GridContinue, new Vector3(0, newPosition.y + 17, 0), Quaternion.identity);
        }
    }

    public void ControlGrid()
    {
        allGrids = GameObject.FindGameObjectsWithTag("Grid");
        if (allGrids.Length >= 6)
        {
            for (int i = 0; i < 2; i++)
            {
                Destroy(allGrids[i]);
            }
            for (int o = 2; o < allGrids.Length; o++)
            {
                allGrids[o - 2] = allGrids[o];
            }
        }
    }

    public void DestroyerGrid()
    {
        for (int i = 0; i < allGrids.Length; i++)
        {
            if (allGrids != null)
            {
                Destroy(allGrids[i]);
            }
        }
    }
    public void gameStart()
    {
        playerLogic.gameIs = true;
        playerLogic.countLevel = 0;
        int i = 0;
        int goldIs = Random.Range(0, 101);
        if (skin.whichOn[0] == true)
        {
            i = Random.Range(0, 2);
        }
        else if (skin.whichOn[1] == true)
        {

        }
        else if (skin.whichOn[2] == true)
        {
            i = Random.Range(2, 4);
        }
        else if (skin.whichOn[3] == true)
        {
            i = Random.Range(4, 6);
        }
        if (goldIs != 100)
        {
            lastObject = Instantiate(prefab[i], startPosition, Quaternion.identity); // Instantiate the first object
        }
        else
        {
            lastObject = Instantiate(Goldprefab[i], startPosition, Quaternion.identity); // Instantiate the first object
        }
        
        CountGrid = 2;
        CreateNewObject();
    }
}

