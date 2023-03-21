using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Button[] buttonPreChoose;
    public GameLogic playerLogic;
    public SkinManager manager;
    public GameObject moneyNeed;
    public GameObject[] cost;
    public GameObject[] choose;
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        for (int i = 0; i < buttonPreChoose.Length; i++)
        {
            buttonPreChoose[0].onClick.AddListener(Default);
            buttonPreChoose[1].onClick.AddListener(BigGuy);
            buttonPreChoose[2].onClick.AddListener(BombMan);
            buttonPreChoose[3].onClick.AddListener(Captain);
        }
        for (int i = 0; i < manager.whichOn.Length; i++)
        {
            if (manager.whichOn[i] == true)
            {
                animator.Play(manager.idleSkin[i]);
            }
        }
    }
    void Update()
    {
        for (int i = 0; i < manager.whichBuy.Length; i++)
        {
            if (manager.whichBuy[i] == true && cost[i] != null)
            {
                cost[i].SetActive(false);
            }
        }
        for (int i = 0; i < manager.whichOn.Length; i++)
        {
            if (manager.whichOn[i] == true)
            {
                choose[i].SetActive(true);
            }
            else
            {
                choose[i].SetActive(false);
            }
        }
    }
    public void Default()
    {
        for (int i = 0; i < manager.whichOn.Length; i++)
        {
            manager.whichOn[i] = false;
            choose[i].SetActive(false);
        }
        manager.whichOn[0] = true;
        choose[0].SetActive(true);
        animator.Play(manager.idleSkin[0]);
    }
    public void BigGuy()
    {
        if (playerLogic.money >= 100 && manager.whichBuy[1] == false)
        {
            playerLogic.money -= 100;
            manager.whichBuy[1] = true;
        }
        else if (manager.whichBuy[1] == true)
        {
            for (int i = 0; i < manager.whichOn.Length; i++)
            {
                manager.whichOn[i] = false;
                choose[i].SetActive(false);
            }
            manager.whichOn[1] = true;
            choose[1].SetActive(true);
            animator.Play(manager.idleSkin[1]);
        }
        else if (playerLogic.money < 100)
        {
            moneyNeed.SetActive(true);
        }
    }
    public void BombMan()
    {
        if (playerLogic.money >= 100 && manager.whichBuy[2] == false)
        {
            playerLogic.money -= 100;
            manager.whichBuy[2] = true;
        }
        else if (manager.whichBuy[2] == true)
        {
            for (int i = 0; i < manager.whichOn.Length; i++)
            {
                manager.whichOn[i] = false;
                choose[i].SetActive(false);
            }
            manager.whichOn[2] = true;
            choose[2].SetActive(true);
            animator.Play(manager.idleSkin[2]);
        }
        else if (playerLogic.money < 100)
        {
            moneyNeed.SetActive(true);
        }
    }
    public void Captain()
    {
        if (playerLogic.money >= 100 && manager.whichBuy[3] == false)
        {
            playerLogic.money -= 100;
            manager.whichBuy[3] = true;
        }
        else if (manager.whichBuy[3] == true)
        {
            for (int i = 0; i < manager.whichOn.Length; i++)
            {
                manager.whichOn[i] = false;
                choose[i].SetActive(false);
            }
            manager.whichOn[3] = true;
            choose[3].SetActive(true);
            animator.Play(manager.idleSkin[3]);
        }
        else if (playerLogic.money < 100)
        {
            moneyNeed.SetActive(true);
        }
    }
}
