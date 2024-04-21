using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slots_System : MonoBehaviour, IPointerClickHandler
{
    public string slotName;
    public int slotCount;

    public Lunka_System[] lunkas;

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < lunkas.Length; i++)
        {
            if (lunkas[i].status == "Unlocked" && lunkas[i].typeOfSeed == "" && slotCount > 0)
            {
                lunkas[i].GetComponent<Animator>().SetBool("Select", true);
                lunkas[i].isSelect = true;
                lunkas[i].slotName = slotName;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slotName == "Ananas")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().ananasCount;
        }
        else if (slotName == "Potato")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().potatoCount;
        }
        else if (slotName == "Lemon")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().lemonCount;
        }
        else if (slotName == "Weed")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().weedCount;
        }
        else if (slotName == "Carrot")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().carrotCount;
        }
        else if (slotName == "Watermelon")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().watermelonCount;
        }
        else if (slotName == "Protivoyadie")
        {
            slotCount = GameObject.Find("Canvas").GetComponent<General_System>().protivoyadieCount;
        }
    }
}
