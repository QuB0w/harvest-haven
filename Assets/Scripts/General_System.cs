using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class General_System : MonoBehaviour
{
    [Header("Shop")]
    public string shopSlotName = "";
    public Text shopSlotNameText;
    public Image shopSlotImg;
    public Text shopSlotPriceText;
    public int shopSlotPrice = 0;

    [Header("Count of seeds and items")]
    public int ananasCount = 0;
    public int carrotCount = 0;
    public int lemonCount = 0;
    public int potatoCount = 0;
    public int watermelonCount = 0;
    public int weedCount = 0;
    public int protivoyadieCount = 0;
    public int lunkaCount = 2;

    [Header("GameObjects")]
    public Text[] countText;
    public Text coinsText;
    public RectTransform shopPanel;
    public RectTransform settingsPanel;
    public GameObject BG;
    public Lunka_System[] lunkas;

    [Header("ShopSlotsImages")]
    public Sprite shopSlotWeed;
    public Sprite shopSlotCarrot;
    public Sprite shopSlotPotato;
    public Sprite shopSlotWatermelon;
    public Sprite shopSlotAnanas;
    public Sprite shopSlotLemon;
    public Sprite shopSlotLunka;
    public Sprite shopSlotProtivoyadie;

    public bool isTakeWater = false;
    public bool isTakeProtivoyadie = false;

    public int Coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("LunkaCount"))
        {
            lunkaCount = PlayerPrefs.GetInt("LunkaCount");
        }
        else
        {
            lunkaCount = 2;
        }
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LunkaCount", lunkaCount);
    }

    public void OnWaterClick()
    {
        for (int i = 0; i < lunkas.Length; i++)
        {
            if (lunkas[i].status == "Unlocked" && lunkas[i].typeOfSeed != "" && lunkas[i].isWater == false && lunkas[i].timeToGrow > 0)
            {
                lunkas[i].GetComponent<Animator>().SetBool("Select", true);
                lunkas[i].isSelect = true;
                isTakeWater = true;
            }
        }
    }

    public void OnProtivoyadieClick()
    {
        for (int i = 0; i < lunkas.Length; i++)
        {
            if (lunkas[i].status == "Unlocked" && lunkas[i].typeOfSeed != "" && lunkas[i].isWater == true && lunkas[i].timeToGrow > 0)
            {
                lunkas[i].GetComponent<Animator>().SetBool("Select", true);
                lunkas[i].isSelect = true;
                isTakeProtivoyadie = true;
            }
        }
    }

    public void OnClickBuy()
    {
        if(Coins >= shopSlotPrice)
        {
            if (shopSlotName == "Potato")
            {
                potatoCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Carrot")
            {
                carrotCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Lemon")
            {
                lemonCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Watermelon")
            {
                watermelonCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Ananas")
            {
                ananasCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Weed")
            {
                weedCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Protivoyadie")
            {
                protivoyadieCount += 1;
                Coins -= shopSlotPrice;
            }
            else if (shopSlotName == "Lunka")
            {
                lunkaCount += 1;
                Coins -= shopSlotPrice;
            }
        }
    }

    public void onClickArrowLeft()
    {
        if (shopSlotName == "Potato")
        {
            shopSlotName = "Weed";
        }
        else if (shopSlotName == "Carrot")
        {
            shopSlotName = "Potato";
        }
        else if (shopSlotName == "Lemon")
        {
            shopSlotName = "Carrot";
        }
        else if (shopSlotName == "Watermelon")
        {
            shopSlotName = "Lemon";
        }
        else if (shopSlotName == "Ananas")
        {
            shopSlotName = "Watermelon";
        }
        else if(shopSlotName == "Weed")
        {
            shopSlotName = "Lunka";
        }
        else if (shopSlotName == "Protivoyadie")
        {
            shopSlotName = "Ananas";
        }
        else if (shopSlotName == "Lunka")
        {
            shopSlotName = "Protivoyadie";
        }
    }

    public void onClickArrowRight()
    {
        if (shopSlotName == "Weed")
        {
            shopSlotName = "Potato";
        }
        else if (shopSlotName == "Potato")
        {
            shopSlotName = "Carrot";
        }
        else if (shopSlotName == "Carrot")
        {
            shopSlotName = "Lemon";
        }
        else if (shopSlotName == "Lemon")
        {
            shopSlotName = "Watermelon";
        }
        else if (shopSlotName == "Watermelon")
        {
            shopSlotName = "Ananas";
        }
        else if (shopSlotName == "Ananas")
        {
            shopSlotName = "Protivoyadie";
        }
        else if (shopSlotName == "Protivoyadie")
        {
            shopSlotName = "Lunka";
        }
        else if (shopSlotName == "Lunka")
        {
            shopSlotName = "Weed";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lunkaCount == 2)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Locked";
            lunkas[3].status = "Locked";
            lunkas[4].status = "Locked";
            lunkas[5].status = "Locked";
            lunkas[6].status = "Locked";
            lunkas[7].status = "Locked";
            lunkas[8].status = "Locked";
        }
        else if(lunkaCount == 3)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Locked";
            lunkas[4].status = "Locked";
            lunkas[5].status = "Locked";
            lunkas[6].status = "Locked";
            lunkas[7].status = "Locked";
            lunkas[8].status = "Locked";
        }
        else if (lunkaCount == 4)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Unlocked";
            lunkas[4].status = "Locked";
            lunkas[5].status = "Locked";
            lunkas[6].status = "Locked";
            lunkas[7].status = "Locked";
            lunkas[8].status = "Locked";
        }
        else if (lunkaCount == 5)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Unlocked";
            lunkas[4].status = "Unlocked";
            lunkas[5].status = "Locked";
            lunkas[6].status = "Locked";
            lunkas[7].status = "Locked";
            lunkas[8].status = "Locked";
        }
        else if (lunkaCount == 6)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Unlocked";
            lunkas[4].status = "Unlocked";
            lunkas[5].status = "Unlocked";
            lunkas[6].status = "Locked";
            lunkas[7].status = "Locked";
            lunkas[8].status = "Locked";
        }
        else if (lunkaCount == 7)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Unlocked";
            lunkas[4].status = "Unlocked";
            lunkas[5].status = "Unlocked";
            lunkas[6].status = "Unlocked";
            lunkas[7].status = "Locked";
            lunkas[8].status = "Locked";
        }
        else if (lunkaCount == 8)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Unlocked";
            lunkas[4].status = "Unlocked";
            lunkas[5].status = "Unlocked";
            lunkas[6].status = "Unlocked";
            lunkas[7].status = "Unlocked";
            lunkas[8].status = "Locked";
        }
        else if (lunkaCount == 9)
        {
            lunkas[0].status = "Unlocked";
            lunkas[1].status = "Unlocked";
            lunkas[2].status = "Unlocked";
            lunkas[3].status = "Unlocked";
            lunkas[4].status = "Unlocked";
            lunkas[5].status = "Unlocked";
            lunkas[6].status = "Unlocked";
            lunkas[7].status = "Unlocked";
            lunkas[8].status = "Unlocked";
        }


        if (shopSlotName == "Weed")
        {
            shopSlotNameText.text = "Weed";
            shopSlotPriceText.text = "10";
            shopSlotPrice = 10;
            shopSlotImg.sprite = shopSlotWeed;
        }
        else if(shopSlotName == "Potato")
        {
            shopSlotNameText.text = "Potato";
            shopSlotPriceText.text = "40";
            shopSlotPrice = 40;
            shopSlotImg.sprite = shopSlotPotato;
        }
        else if (shopSlotName == "Carrot")
        {
            shopSlotNameText.text = "Carrot";
            shopSlotPriceText.text = "75";
            shopSlotPrice = 75;
            shopSlotImg.sprite = shopSlotCarrot;
        }
        else if (shopSlotName == "Lemon")
        {
            shopSlotNameText.text = "Lemon";
            shopSlotPriceText.text = "125";
            shopSlotPrice = 125;
            shopSlotImg.sprite = shopSlotLemon;
        }
        else if (shopSlotName == "Watermelon")
        {
            shopSlotNameText.text = "Watermelon";
            shopSlotPriceText.text = "185";
            shopSlotPrice = 185;
            shopSlotImg.sprite = shopSlotWatermelon;
        }
        else if (shopSlotName == "Ananas")
        {
            shopSlotNameText.text = "Ananas";
            shopSlotPriceText.text = "280";
            shopSlotPrice = 280;
            shopSlotImg.sprite = shopSlotAnanas;
        }
        else if (shopSlotName == "Protivoyadie")
        {
            shopSlotNameText.text = "Poison";
            shopSlotPriceText.text = "150";
            shopSlotPrice = 150;
            shopSlotImg.sprite = shopSlotProtivoyadie;
        }
        else if (shopSlotName == "Lunka")
        {
            shopSlotNameText.text = "Garden";
            if(lunkaCount == 2)
            {
                shopSlotPriceText.text = "30";
                shopSlotPrice = 30;
            }
            else if(lunkaCount == 3)
            {
                shopSlotPriceText.text = "70";
                shopSlotPrice = 70;
            }
            else if (lunkaCount == 3)
            {
                shopSlotPriceText.text = "140";
                shopSlotPrice = 140;
            }
            else if (lunkaCount == 4)
            {
                shopSlotPriceText.text = "250";
                shopSlotPrice = 250;
            }
            else if (lunkaCount == 5)
            {
                shopSlotPriceText.text = "360";
                shopSlotPrice = 360;
            }
            else if (lunkaCount == 6)
            {
                shopSlotPriceText.text = "450";
                shopSlotPrice = 450;
            }
            else if (lunkaCount == 7)
            {
                shopSlotPriceText.text = "560";
                shopSlotPrice = 560;
            }
            shopSlotImg.sprite = shopSlotLunka;
        }

        coinsText.text = Coins.ToString();
        countText[0].text = ananasCount.ToString();
        countText[1].text = potatoCount.ToString();
        countText[2].text = lemonCount.ToString();
        countText[3].text = weedCount.ToString();
        countText[4].text = carrotCount.ToString();
        countText[5].text = watermelonCount.ToString();
        countText[6].text = protivoyadieCount.ToString();
    }

    public void OnClickShop()
    {
        BG.SetActive(true);
        shopPanel.DOMoveY(1, 1f);
    }

    public void OnClickCloseShop()
    {
        BG.SetActive(false);
        shopPanel.DOMoveY(-6, 1f);
    }

    public void OnClickSettings()
    {
        BG.SetActive(true);
        settingsPanel.DOMoveY(1.5f, 1f);
    }

    public void OnClickCloseSettings()
    {
        BG.SetActive(false);
        settingsPanel.DOMoveY(8, 1f);
    }
}
