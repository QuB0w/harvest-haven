using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lunka_System : MonoBehaviour, IPointerClickHandler
{
    public int ID = 0;

    public string status = "";
    public string typeOfSeed = "";

    public string slotName = "";

    public Animator seedOBJ;

    public int timeToGrow = 0; 
    public int moneyToGet = 0;

    public bool isLunkaChervyak = false;

    public bool isWater = false;
    public bool isProtivoyadie = false;

    public bool isSelect = false;

    public Image sprite;
    public Image Seed;
    public Text moneyToGetText;
    public GameObject moneyToGetOBJ;
    public Text timerText;
    public GameObject Water;

    [Header("Sprites")]
    public Sprite lunkaLocked;
    public Sprite lunkaUnlocked;
    public Sprite ananasSeed;
    public Sprite carrotSeed;
    public Sprite potatoSeed;
    public Sprite watermelonSeed;
    public Sprite weedSeed;
    public Sprite lemonSeed;
    // Start is called before the first frame update
    void Start()
    {
        LoadSave();
    }

    IEnumerator timer()
    {
        if(timeToGrow > 0)
        {
            yield return new WaitForSeconds(1f);
            timeToGrow -= 1;
            StartCoroutine(timer());
        }
    }

    IEnumerator chervyakTimer()
    {
        var seconds = Random.Range(20, 31);
        if(typeOfSeed == "Weed")
        {
            seconds = Random.Range(20, 31);
        }
        else if(typeOfSeed == "Potato")
        {
            seconds = Random.Range(40, 61);
        }
        else if (typeOfSeed == "Carrot")
        {
            seconds = Random.Range(60, 91);
        }
        else if (typeOfSeed == "Lemon")
        {
            seconds = Random.Range(60, 90);
        }
        else if (typeOfSeed == "Watermelon")
        {
            seconds = Random.Range(60, 90);
        }
        else if (typeOfSeed == "Ananas")
        {
            seconds = Random.Range(60, 90);
        }
        yield return new WaitForSeconds(seconds);
        seedOBJ.Play("SeedChervyak");
        moneyToGetOBJ.SetActive(false);
        Seed.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        typeOfSeed = "";
        timeToGrow = 0;
        moneyToGet = 0;
        slotName = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(isProtivoyadie == true)
        {
            isLunkaChervyak = false;
            StopCoroutine(chervyakTimer());
        }

        if (isSelect == false)
        {
            if (slotName == "Ananas")
            {
                if (isWater == false && typeOfSeed != "")
                {
                    Water.SetActive(true);
                }
                Seed.gameObject.SetActive(true);
                Seed.sprite = ananasSeed;
                typeOfSeed = "Ananas";
                moneyToGet = 350;
                Seed.SetNativeSize();
            }
            else if (slotName == "Potato")
            {
                if (isWater == false && typeOfSeed != "")
                {
                    Water.SetActive(true);
                }
                Seed.gameObject.SetActive(true);
                Seed.sprite = potatoSeed;
                typeOfSeed = "Potato";
                moneyToGet = 55;
                Seed.SetNativeSize();
            }
            else if (slotName == "Carrot")
            {
                if (isWater == false && typeOfSeed != "")
                {
                    Water.SetActive(true);
                }
                Seed.gameObject.SetActive(true);
                Seed.sprite = carrotSeed;
                typeOfSeed = "Carrot";
                moneyToGet = 105;
                Seed.SetNativeSize();
            }
            else if (slotName == "Watermelon")
            {
                if (isWater == false && typeOfSeed != "")
                {
                    Water.SetActive(true);
                }
                Seed.gameObject.SetActive(true);
                Seed.sprite = watermelonSeed;
                typeOfSeed = "Watermelon";
                moneyToGet = 240;
                Seed.SetNativeSize();
            }
            else if (slotName == "Lemon")
            {
                if (isWater == false && typeOfSeed != "")
                {
                    Water.SetActive(true);
                }
                Seed.gameObject.SetActive(true);
                Seed.sprite = lemonSeed;
                typeOfSeed = "Lemon";
                moneyToGet = 165;
                Seed.SetNativeSize();
            }
            else if (slotName == "Weed")
            {
                if (isWater == false && timeToGrow > 0)
                {
                    Water.SetActive(true);
                }
                Seed.gameObject.SetActive(true);
                Seed.sprite = weedSeed;
                typeOfSeed = "Weed";
                moneyToGet = 15;
                Seed.SetNativeSize();
            }
        }

        if (timeToGrow <= 0 && typeOfSeed != "")
        {
            moneyToGetOBJ.SetActive(true);
            timerText.gameObject.SetActive(false);
            isWater = false;
        }

        moneyToGetText.text = moneyToGet.ToString();
        timerText.text = timeToGrow.ToString();

        if(status == "Locked")
        {
            sprite.sprite = lunkaLocked;
        }
        else if(status == "Unlocked")
        {
            sprite.sprite = lunkaUnlocked;
        }
    }

    public void LoadSave()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().Coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().Coins = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_1"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].timeToGrow = PlayerPrefs.GetInt("LunkaTime_1");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].timeToGrow = 0;
        }


        if (PlayerPrefs.HasKey("LunkaTime_2"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].timeToGrow = PlayerPrefs.GetInt("LunkaTime_2");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_3"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].timeToGrow = PlayerPrefs.GetInt("LunkaTime_3");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_4"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].timeToGrow = PlayerPrefs.GetInt("LunkaTime_4");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_5"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].timeToGrow = PlayerPrefs.GetInt("LunkaTime_5");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_6"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].timeToGrow = PlayerPrefs.GetInt("LunkaTime_6");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_7"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].timeToGrow = PlayerPrefs.GetInt("LunkaTime_7");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_8"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].timeToGrow = PlayerPrefs.GetInt("LunkaTime_8");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaTime_9"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].timeToGrow = PlayerPrefs.GetInt("LunkaTime_9");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].timeToGrow = 0;
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_1"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].slotName = PlayerPrefs.GetString("LunkaSlotName_1");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].slotName = "";
        }


        if (PlayerPrefs.HasKey("LunkaSlotName_2"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].slotName = PlayerPrefs.GetString("LunkaSlotName_2");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_3"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].slotName = PlayerPrefs.GetString("LunkaSlotName_3");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_4"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].slotName = PlayerPrefs.GetString("LunkaSlotName_4");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_5"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].slotName = PlayerPrefs.GetString("LunkaSlotName_5");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_6"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].slotName = PlayerPrefs.GetString("LunkaSlotName_6");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_7"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].slotName = PlayerPrefs.GetString("LunkaSlotName_7");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_8"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].slotName = PlayerPrefs.GetString("LunkaSlotName_8");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaSlotName_9"))
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].slotName = PlayerPrefs.GetString("LunkaSlotName_9");
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].slotName = "";
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_1"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_1") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].isProtivoyadie = true;
            }
            else if(PlayerPrefs.GetInt("LunkaProtivoyadie_1") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].isProtivoyadie = false;
        }


        if (PlayerPrefs.HasKey("LunkaProtivoyadie_2"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_2") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_2") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_3"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_3") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_3") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_4"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_4") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_4") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_5"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_5") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_5") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_6"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_6") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_6") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_7"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_7") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_7") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_8"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_8") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_8") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaProtivoyadie_9"))
        {
            if (PlayerPrefs.GetInt("LunkaProtivoyadie_9") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].isProtivoyadie = true;
            }
            else if (PlayerPrefs.GetInt("LunkaProtivoyadie_9") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].isProtivoyadie = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].isProtivoyadie = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_1"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_1") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_1") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[0].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_2"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_2") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_2") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[1].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_3"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_3") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_3") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[2].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_4"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_4") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_4") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[3].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_5"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_5") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_5") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[4].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_6"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_6") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_6") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[5].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_7"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_7") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_7") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[6].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_8"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_8") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_8") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[7].isLunkaChervyak = false;
        }

        if (PlayerPrefs.HasKey("LunkaChervyak_9"))
        {
            if (PlayerPrefs.GetInt("LunkaChervyak_9") == 1)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].isLunkaChervyak = true;
            }
            else if (PlayerPrefs.GetInt("LunkaChervyak_9") == 0)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].isLunkaChervyak = false;
            }
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<General_System>().lunkas[8].isLunkaChervyak = false;
        }
    }

    public void OnApplicationQuit()
    {
        if(isProtivoyadie == true)
        {
            PlayerPrefs.SetInt("LunkaProtivoyadie_" + ID, 1);
        }
        else
        {
            PlayerPrefs.SetInt("LunkaProtivoyadie_" + ID, 0);
        }

        if (isLunkaChervyak == true)
        {
            PlayerPrefs.SetInt("LunkaChervyak_" + ID, 1);
        }
        else
        {
            PlayerPrefs.SetInt("LunkaChervyak_" + ID, 0);
        }
        PlayerPrefs.SetInt("LunkaTime_" + ID, timeToGrow);
        PlayerPrefs.SetString("LunkaSlotName_" + ID, typeOfSeed);
        PlayerPrefs.SetInt("Coins", GameObject.Find("Canvas").GetComponent<General_System>().Coins);
    }

    public void OnClickMoneyGet()
    {
        GameObject.Find("Canvas").GetComponent<General_System>().Coins += moneyToGet;
        moneyToGetOBJ.SetActive(false);
        Seed.gameObject.SetActive(false);
        typeOfSeed = "";
        timeToGrow = 0;
        moneyToGet = 0;
        slotName = "";
        isProtivoyadie = false;
    }

    public void Plant()
    {
        var rnd = Random.Range(1, 3);
        if (rnd == 1)
        {
            isLunkaChervyak = true;
            StartCoroutine(chervyakTimer());
        }
        else if(rnd == 2)
        {
            isLunkaChervyak = false;
        }

        if (slotName == "Ananas")
        {
            if (typeOfSeed == "")
            {
                Water.SetActive(true);
            } 
            GameObject.Find("Canvas").GetComponent<General_System>().ananasCount -= 1;
            Seed.gameObject.SetActive(true);
            Seed.sprite = ananasSeed;
            typeOfSeed = "Ananas";
            timeToGrow = 200;
            moneyToGet = 350;
            Seed.SetNativeSize();
        }
        else if (slotName == "Potato")
        {
            if (typeOfSeed == "")
            {
                Water.SetActive(true);
            }
            GameObject.Find("Canvas").GetComponent<General_System>().potatoCount -= 1;
            Seed.gameObject.SetActive(true);
            Seed.sprite = potatoSeed;
            typeOfSeed = "Potato";
            timeToGrow = 60;
            moneyToGet = 55;
            Seed.SetNativeSize();
        }
        else if (slotName == "Carrot")
        {
            if (typeOfSeed == "")
            {
                Water.SetActive(true);
            }
            GameObject.Find("Canvas").GetComponent<General_System>().carrotCount -= 1;
            Seed.gameObject.SetActive(true);
            Seed.sprite = carrotSeed;
            typeOfSeed = "Carrot";
            timeToGrow = 90;
            moneyToGet = 105;
            Seed.SetNativeSize();
        }
        else if (slotName == "Watermelon")
        {
            if (typeOfSeed == "")
            {
                Water.SetActive(true);
            }
            GameObject.Find("Canvas").GetComponent<General_System>().watermelonCount -= 1;
            Seed.gameObject.SetActive(true);
            Seed.sprite = watermelonSeed;
            typeOfSeed = "Watermelon";
            timeToGrow = 160;
            moneyToGet = 240;
            Seed.SetNativeSize();
        }
        else if (slotName == "Lemon")
        {
            if (typeOfSeed == "")
            {
                Water.SetActive(true);
            }
            GameObject.Find("Canvas").GetComponent<General_System>().lemonCount -= 1;
            Seed.gameObject.SetActive(true);
            Seed.sprite = lemonSeed;
            typeOfSeed = "Lemon";
            timeToGrow = 120;
            moneyToGet = 165;
            Seed.SetNativeSize();
        }
        else if (slotName == "Weed")
        {
            if (typeOfSeed == "")
            {
                Water.SetActive(true);
            }
            GameObject.Find("Canvas").GetComponent<General_System>().weedCount -= 1;
            Seed.gameObject.SetActive(true);
            Seed.sprite = weedSeed;
            typeOfSeed = "Weed";
            timeToGrow = 30;
            moneyToGet = 15;
            Seed.SetNativeSize();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelect && isWater == false && GameObject.Find("Canvas").GetComponent<General_System>().isTakeWater == false)
        {
            Plant();

            for (int i = 0; i < GameObject.Find("Canvas").GetComponent<General_System>().lunkas.Length; i++)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].GetComponent<Animator>().SetBool("Select", false);
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].isSelect = false;
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].slotName = "";
            }
        }
        if(isSelect == true && GameObject.Find("Canvas").GetComponent<General_System>().isTakeWater == true)
        {
            Water.SetActive(false);
            isWater = true;
            StartCoroutine(timer());
            timerText.gameObject.SetActive(true);
            GameObject.Find("Canvas").GetComponent<General_System>().isTakeWater = false;
            for (int i = 0; i < GameObject.Find("Canvas").GetComponent<General_System>().lunkas.Length; i++)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].GetComponent<Animator>().SetBool("Select", false);
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].isSelect = false;
            }
        }

        if (isSelect == true && GameObject.Find("Canvas").GetComponent<General_System>().isTakeProtivoyadie == true)
        {
            isProtivoyadie = true;
            GameObject.Find("Canvas").GetComponent<General_System>().isTakeProtivoyadie = false;
            for (int i = 0; i < GameObject.Find("Canvas").GetComponent<General_System>().lunkas.Length; i++)
            {
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].GetComponent<Animator>().SetBool("Select", false);
                GameObject.Find("Canvas").GetComponent<General_System>().lunkas[i].isSelect = false;
            }
        }
    }
}
