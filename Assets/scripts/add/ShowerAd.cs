using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerAd : MonoBehaviour
{
    public bool working;
    public int counting;
    public InterstitialAds shower;

    void Update()
    {
        if (working == true)
        {
            if (counting >= 3)
            {
                shower.ShowAd();
                counting = 0;
            }
        }
    }
}
