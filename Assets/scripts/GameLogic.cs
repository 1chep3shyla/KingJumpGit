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


    void Start()
    {
        loseTime = maxTime;
        animator = gameObject.GetComponent<Animator>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        EM = gameObject.GetComponent<EnemyManager>();
        startingPosition = transform.position;
        StartPosInGame = transform.position;
        skins = gameObject.GetComponent<SkinManager>();
        creator = gameObject.GetComponent<CreatePlatform>();
        gameIs = false;

    }
    void Update()
    {
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
        LoseGameObject[5].SetActive(false);
        LoseGameObject[6].SetActive(false);
        gameIs = false;
        animator.Play(skins.allAnimSkin[3]);
        yield return new WaitForSeconds(1.2f);
        if (skins.whichOn[0] == true)
        {
        }
        else if (skins.whichOn[1] == true)
        {

        }
        else if (skins.whichOn[2] == true)
        {
            source[2].PlayOneShot(clips[2]);
        }
        LoseGame();
        yield return null;
    }

    public void Restart()
    {
        animator.Play(skins.allAnimSkin[0]);
        transform.position = StartPosInGame;
        Lose = false;
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
        creator.CreateNewObject();
        startingPosition = curPos;
        corWork = true;
        bc.enabled = false;
        float elapsedTime = 0f;
        animator.Play(skins.allAnimSkin[1]);
        GameObject copy = Instantiate(particleJump, transform.position , Quaternion.identity);
        if (nameBut == "Left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (EM.enemyNames[countLevel] == "left")
            {
                FXPlayer();
                if (skins.whichOn[0] == true)
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_death");
                }
                else if (skins.whichOn[1] == true)
                {

                }
                else if (skins.whichOn[2] == true)
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("bomb_anim_exp");
                }
                    countLevel++;

                if (countLevel > countLevelMax)
                {
                    countLevelMax = countLevel;
                    recordText.SetActive(true);
                }
                else if(countLevel < countLevelMax)
                {
                    recordText.SetActive(false);
                }
                loseTime = maxTime;
            }
            else
            {
                Lose = true;
                EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_attack");
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
            if (EM.enemyNames[countLevel] == "right")
            {
                FXPlayer();
                if (skins.whichOn[0] == true)
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_death");
                }
                else if (skins.whichOn[1] == true)
                {

                }
                else if (skins.whichOn[2] == true)
                {
                    EM.enemies[countLevel].GetComponent<Animator>().Play("bomb_anim_exp");
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
                EM.enemies[countLevel].GetComponent<Animator>().Play("pig_anim_attack");
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
        if (elapsedTime >= 1f)
        {
            bc.enabled = true;
        }
        while (elapsedTime < 1f )
        {
            float newY = Mathf.Lerp(startingPosition.y, startingPosition.y + height, elapsedTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z); 
            elapsedTime += Time.deltaTime * speed; 
            yield return null; 
        }
        
        corWork = false;
        bc.enabled = true;
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
}
