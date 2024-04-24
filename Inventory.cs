using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public static Inventory Instance;
    public List<GameObject> tottebag = new List<GameObject>();
    public List<ItemClass> itemsFromTotte = new List<ItemClass>();
    public GameObject inventory, selector;
    public Boolean empty;
    public int idSlot;

    private void Awake()
    {
        if(Instance ==null)
        {
            Instance = this;

        }
        
    }
    void Start()
    {
        createItem(itemsFromTotte[0].itemName, itemsFromTotte[0].anxietyPoints, itemsFromTotte[0].sipPoints, itemsFromTotte[0].description,
            itemsFromTotte[0].spriteItem, itemsFromTotte[0].price);
    }

    private void Update()
    {
        navigation();
        if (Input.GetMouseButtonDown(0)) // 0 representa el clic izquierdo del mouse
        {
            Vector2 clicPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selector.transform.position = clicPos;
        }
    }

    // Update is called once per frame




    public void navigation()
    {

        if(Input.GetKey(KeyCode.P))
        {
            if (idSlot< itemsFromTotte.Count)
            {
                ConsumeItem(itemsFromTotte[idSlot]);
                BattleManager.Instance.ElisaText.text = "Has consumido: " + itemsFromTotte[idSlot].itemName + " Y has bajado: "
                    + itemsFromTotte[idSlot].anxietyPoints + " de ansiedad y recuperado: " + -itemsFromTotte[idSlot].sipPoints + " de socialidad";
                removeItem(itemsFromTotte[idSlot]);
                tottebag[0].GetComponent<Image>().sprite = null;
                tottebag[0].GetComponent<Image>().enabled=false;
            }
        }
        if(Input.GetKeyDown(KeyCode.D) && idSlot < tottebag.Count - 1)
        {
            idSlot++;
        }

        if(Input.GetKeyDown(KeyCode.A) && idSlot > 0)
        {
            idSlot--;
        }

        if (Input.GetKeyDown(KeyCode.W) && idSlot > 3)
        {
            idSlot-=4;
        }

        if (Input.GetKeyDown(KeyCode.S) && idSlot < 8)
        {
            idSlot += 4;
        }

        selector.transform.position = tottebag[idSlot].transform.position;
    }

    public void addItem(ItemClass item)
    {
         itemsFromTotte.Add(item);
    }

    public void removeItem(ItemClass item)
    {
        itemsFromTotte.Remove(item);
    }

    public void ConsumeItem(ItemClass item)
    {
       // GameManager.instance.subAnxiety(item.getAnxietyPoints());
        // GameManager.instance.addSip(item.getSIPPoints());
        playerBattle.instance.TakeItem(item);




    }

    public void createItem(string itemName, int anxietyPoints, int sipPoints, string description, Sprite spriteItem, int price)
    {
        ItemClass newItem = new ItemClass();
        newItem.spriteItem = spriteItem;
        newItem.itemName = itemName;
        newItem.price = price;
        newItem.sipPoints = sipPoints;
        newItem.description = description;
        newItem.anxietyPoints = anxietyPoints;
        itemsFromTotte.Add(newItem);
        tottebag[0].GetComponent<Image>().sprite = itemsFromTotte[0].spriteItem;

    }

}
