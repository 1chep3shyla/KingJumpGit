using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectEnabled : MonoBehaviour
{
    public float offTime;

    void Update()
    {
        offTime += Time.deltaTime;
        if (offTime >= 0.5f)
        {
            gameObject.SetActive(false);
            offTime = 0f;
        }
    }
}
