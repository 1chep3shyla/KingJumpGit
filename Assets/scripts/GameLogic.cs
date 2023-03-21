using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public float loseTime;
    public float maxTime;
    public Image bombImage;
    public Text curTimeText;
    private Animator animator;
    public string nameBut;
    private bool corWork;
    public string[] where = new string[3];
    private BoxCollider2D bc;
    public int countLevel;
    public int countLevelMax;
    public Text[] countLevelText;
    public Text moneyText;
    public int money;
    [SerializeField] private float height = 2.8f; // The height to raise the object to
    [SerializeField] private float speed = 1f; // The speed at which to move the object
    private EnemyManager EM;
    public bool Lose;
    public bool gameIs;
    public GameObject particleJump;

    public GameObject[] LoseGameObject;
    public GameObject recordText;
    private Vector3 startingPosition; // The starting position of the object
    private Vector3 curPos;
    private Vector3 StartPosInGame;
    public SkinManager skins;
    public CreatePlatform creator;
    public AudioSource[] source;
    public AudioClip[] clips;
    public Slider mmrSlider;
    public int mmr;
    public Text mmrText;
    public Button[] butReward;
    public int[] needMMR;
    public bool[] butRewardGet;
    public float saveTime;
    public ShowerAd addCount;   
    private Rigidbody2D rb;


    void Start()
    {
        loseTime = maxTime;
        animator = gameObject.GetComponent<Animator>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        EM = gameObject.GetComponent<EnemyManager>();
        startingPosition = transform.position;
        StartPosInGame = transform.position;
        skins = gameObject.GetComponent<SkinManager>();
        creator = gameObject.GetComponent<CreatePlatform>();
        gameIs = false;
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            LoadStat();
        }

    }
    void Update()
    {
        saveTime += Time.deltaTime;
        if (saveTime >= 0.5f)
        {
            SaveSystem.SavePlayer(this, skins);
            saveTime = 0f;
        }
        for (int m = 0; m < butReward.Length; m++)
        {
            if (mmr >= needMMR[m] && butRewardGet[m] == false)
            {
                butReward[m].interactable = true;
            }
        }
        mmrSlider.maxValue = 1200;
        mmrSlider.value = mmr;
        mmrText.text = "" + mmr;
        moneyText.text = "" + money;
        if (countLevel >=0 && countLevel <10)
        {
            maxTime = 5f;
        }
        else if (countLevel >= 10 && countLevel < 30)
        {
            maxTime = 4f;
        }
        else if (countLevel >= 30 && countLevel < 50)
        {
            maxTime = 3f;
        }
        else if (countLevel >= 50 && countLevel < 70)
        {
            maxTime = 2f;
        }
        else if (countLevel >= 70 && countLevel < 90)
        {
            maxTime = 1.5f;
        }
        else if (countLevel >= 90 && countLevel < 110)
        {
            maxTime = 1.25f;
        }
        else if (countLevel >= 110 && countLevel < 140)
        {
            maxTime = 1f;
        }
        else if (countLevel >= 140 && countLevel < 170)
        {
            maxTime = 0.85f;
        }
        else if (countLevel >= 170 && countLevel < 200)
        {
            maxTime = 0.7f;
        }
        else if (countLevel >= 200)
        {
            maxTime = 0.5f;
        }
        countLevelText[1].text = "" + countLevelMax;
        countLevelText[3].text = "Record: " + countLevelMax;
        countLevelText[0].text = "" + countLevel;
        countLevelText[2].text = "Points: " + countLevel;
        curPos = transform.position;
        if (gameIs == true)
        {
            if (loseTime <= 0f)
            {
                StartCoroutine(Losee());
            }
            else
            {
                loseTime -= Time.deltaTime;
            }
        }
        bombImage.fillAmount = loseTime / maxTime;
        curTimeText.text = "" + loseTime.ToString("0.0");
    }
    public void Left()
    {
        if (corWork == false && Lose == false)
        {
            nameBut = "Left";
            StartCoroutine(MoveObject());
        }
    }
    public void Right()
    {
        if (corWork == false && Lose == false)
        {
            nameBut = "Right";
            StartCoroutine(MoveObject());
        }
    }
    IEnumerator Losee()
    {
        mmr += (countLevel / 10);
        LoseGameObject[5].SetActive(false);
        LoseGameObject[6].SetActive(false);
        gameIs = false;
        animator.Play(skins.allAnimSkin[3]);
        if (skins.whichOn[1] == true)
        {
            source[2].PlayOneShot(clips[2]);
        }
        yield return new WaitForSeconds(1.2f);
        if (skins.whichOn[0] == true)
        {
        }
        else if (skins.whichOn[2] == true)
        {
            source[2].PlayOneShot(clips[2]);
        }
        LoseGame();
        addCount.counting++;
        yield return null;
    }

    public void Restart()
    {
        animator.Play(skins.allAnimSkin[0]);
        transform.position = StartPosInGame;
        Lose = false;
        countLevel = 0;
        corWork = false;
        loseTime = maxTime;
    }

    public void LoseGame()
    {
        LoseGameObject[0].SetActive(true);
        LoseGameObject[1].SetActive(false);
        LoseGameObject[2].SetActive(true);
        LoseGameObject[3].SetActive(false);
        LoseGameObject[4].SetActive(false);
    }

    private IEnumerator MoveObject()
    {
        rb.gravityScale = 0;
        creator.CreateNewObject();
        startingPosition = curPos;
        corWork = true;
        bc.enabled = false;
        float elapsedTime = 0f;
        animator.Play(skins.allAnimSkin[1]);
        GameObject copy = Instantiate(particleJump, transform.position, Quaternion.identity);
        if (nameBut == "Left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (EM.enemyNames[countLevel] == "left" || EM.enemyNames[countLevel] == "left_gold")
            {
                FXPlayer();
                if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "left")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_death");
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "left")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_anim_death");
                }
                else if (skins.whichOn[2] == true && EM.enemyNames[countLevel] == "left")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("bomb_anim_exp");
                }
                else if (skins.whichOn[3] == true && EM.enemyNames[countLevel] == "left")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("enemy_bottle_anim");
                }
                else if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "left_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pigKing_anim_death");
                    money++;
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "left_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_gold_anim_death");
                    money++;
                }
                else if (skins.whichOn[2] == true && EM.enemyNames[countLevel] == "left_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("bomb_gold_anim_exp");
                    money++;
                }
                else if (skins.whichOn[3] == true && EM.enemyNames[countLevel] == "left_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("enemy_gold_bottle");
                    money++;
                }
                countLevel++;

                if (countLevel > countLevelMax)
                {
                    countLevelMax = countLevel;
                    recordText.SetActive(true);
                }
                else if (countLevel < countLevelMax)
                {
                    recordText.SetActive(false);
                }
                loseTime = maxTime;
            }
            else
            {
                if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "right")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_attack");
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "right")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_anim_attack");
                }
                else if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "right_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pigKing_anim_Attack");
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "right_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_gold_anim_attack");
                }
                Lose = true;
                animator.Play(skins.allAnimSkin[3]);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                StartCoroutine(Losee());
                if (countLevel > countLevelMax)
                {
                    countLevelMax = countLevel;
                    recordText.SetActive(true);
                }
                else if (countLevel < countLevelMax)
                {
                    recordText.SetActive(false);
                }
            }
        }
        else if (nameBut == "Right")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (EM.enemyNames[countLevel] == "right" || EM.enemyNames[countLevel] == "right_gold")
            {
                FXPlayer();
                if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "right")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_death");
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "right")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_anim_death");
                }
                else if (skins.whichOn[2] == true && EM.enemyNames[countLevel] == "right")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("bomb_anim_exp");
                }
                else if (skins.whichOn[3] == true && EM.enemyNames[countLevel] == "right")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("enemy_bottle_anim");
                }
                else if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "right_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pigKing_anim_death");
                    money++;
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "right_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_gold_anim_death");
                    money++;
                }
                else if (skins.whichOn[2] == true && EM.enemyNames[countLevel] == "right_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("bomb_gold_anim_exp");
                    money++;
                }
                else if (skins.whichOn[3] == true && EM.enemyNames[countLevel] == "right_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("enemy_gold_bottle");
                    money++;
                }
                countLevel++;

                if (countLevel > countLevelMax)
                {
                    countLevelMax = countLevel;
                    recordText.SetActive(true);
                }
                else if (countLevel < countLevelMax)
                {
                    recordText.SetActive(false);
                }
                loseTime = maxTime;
            }
            else
            {
                if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "left")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_attack");
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "left")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_anim_attack");
                }
                else if (skins.whichOn[0] == true && EM.enemyNames[countLevel] == "left_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pigKing_anim_Attack");
                }
                else if (skins.whichOn[1] == true && EM.enemyNames[countLevel] == "left_gold")
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("cannon_gold_anim_attack");

                }
                Lose = true;
                animator.Play(skins.allAnimSkin[3]);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                StartCoroutine(Losee());
                if (countLevel > countLevelMax)
                {
                    countLevelMax = countLevel;
                    recordText.SetActive(true);
                }
                else if (countLevel < countLevelMax)
                {
                    recordText.SetActive(false);
                }
            }
        }
        while (gameObject.transform.position.y < startingPosition.y + 7.535259 - 4.910259)
        {
            float newY = Mathf.Lerp(startingPosition.y, startingPosition.y + height, elapsedTime);
            transform.position = new Vector3(0, newY, 0);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        corWork = false;
        bc.enabled = true;

        rb.gravityScale = 0.5f;
        if (Lose == false)
        {
            animator.Play(skins.allAnimSkin[0]);
        }
    }
    public void FXPlayer()
    {
        for (int i = 0; i < skins.whichOn.Length; i++)
        {
            if (skins.whichOn[i] == true)
            {
                float random = Random.Range(0.9f , 1.1f);
                source[i].pitch = random;
                source[i].PlayOneShot(clips[i]);
            }
        }
    }
    public void firstReward()
    {
        butRewardGet[0] = true;
        butReward[0].interactable = false;
        money += 10;
    }
    public void secondReward()
    {
        butRewardGet[1] = true;
        butReward[1].interactable = false;
        money += 15;
    }
    public void thirdReward()
    {
        butRewardGet[2] = true;
        butReward[2].interactable = false;
        money += 20;
    }
    public void fourthReward()
    {
        butRewardGet[3] = true;
        butReward[3].interactable = false;
        money += 30;
    }
    public void fivethReward()
    {
        butRewardGet[4] = true;
        butReward[4].interactable = false;
        money += 50;
    }
    public void LoadStat()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        money = data.moneyData;
        countLevelMax = data.maxCountData;
        mmr = data.mmrData;
        addCount.working = data.workingAd;
        for (int WOD = 0; WOD < skins.whichBuy.Length; WOD++)
        {
            skins.whichBuy[WOD] = data.whichBuyData[WOD];
        }
        for (int WOC = 0; WOC < skins.whichOn.Length; WOC++)
        {
            skins.whichOn[WOC] = data.whichOnData[WOC];
        }
        for (int anim = 0; anim < skins.allAnimSkin.Length; anim++)
        {
            skins.allAnimSkin[anim] = data.allAnimSkinData[anim];
        }
        for (int reward = 0; reward < data.getRewardData.Length; reward++)
        {
            butRewardGet[reward] = data.getRewardData[reward];
        }
    }
    
    public void Donate()
    {
        money += 190;
    }
    public void OffAdd()
    {
        addCount.working = false;
    }
}
