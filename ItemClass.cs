using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : MonoBehaviour
{

    public ItemClass Instance;
    public int anxietyPoints;
    public int sipPoints;
    public string itemName;
    public string description;
    public Sprite spriteItem;
    public int price;

    public int getAnxietyPoints()
    {
        return anxietyPoints;
    }

    public int getSIPPoints()
    {
        return sipPoints;
    }

    public string getName() { return itemName; }
    public string getDescription() { return description; }
    public Sprite getSprite() { return spriteItem;}
    public int getPrice() { return price; } 




}


