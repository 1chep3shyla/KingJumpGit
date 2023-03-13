using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public bool delete;
    void Update()
    {
        if (delete == true)
        {
            Destroy(gameObject);
        }
    }
}
