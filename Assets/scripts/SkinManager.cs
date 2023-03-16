using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public string[] idleSkin;
    public string[] hitSkin;
    public string[] jumpSkin;
    public string[] deathSkin;
    public string[] allAnimSkin; // Which contain names of all anim of one char
    private Animator animator;
    public bool[] whichOn;
    public bool[] whichBuy;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        for (int i = 0; i < whichOn.Length; i++)
        {
            if (whichOn[i] == true)
            {
                allAnimSkin[0] = idleSkin[i];
                allAnimSkin[1] = hitSkin[i];
                allAnimSkin[3] = deathSkin[i];
            }
        }

    }
    public void FirstPlayer()
    {
        for (int i = 0; i < whichOn.Length; i++)
        {
            whichOn[i] = false;
        }
        whichOn[0] = true;
        animator.Play(idleSkin[0]);
    }

    public void SecondPlayer()
    {
        for (int i = 0; i < whichOn.Length; i++)
        {
            whichOn[i] = false;
        }
        whichOn[1] = true;
        animator.Play(idleSkin[1]);
    }

    public void ThirdPlayer()
    {
        for (int i = 0; i < whichOn.Length; i++)
        {
            whichOn[i] = false;
        }
        whichOn[2] = true;
        animator.Play(idleSkin[2]);
    }

    public void FourthPlayer()
    {
        for (int i = 0; i < whichOn.Length; i++)
        {
            whichOn[i] = false;
        }
        whichOn[3] = true;
        animator.Play(idleSkin[3]);
    }
}
