using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBattle : MonoBehaviour
{
    //Las diferencias entre el player y el enemigo son principalmente el rol, y la IA.
    public static playerBattle instance;
    public string playerName;
    public int PA, currentSIP, currentANX;
    public int maxSIP, maxANX;
    public List<int> attacksShoutDMG;
    public List<string> attacksShoutNames;
    public List<int> talkDMG;
    public List<string> talkNames;

    public enum STATE_ELISA { STANDAR, MENTAL_DESTROYED };
    public STATE_ELISA ElisaState;

    private void Awake()
    {

        if (instance == null)
        {

            instance = this;
            currentSIP = 100;
            currentANX = 0;
            attacksShoutDMG = new List<int>();
            attacksShoutNames = new List<string>();
            talkDMG= new List<int>();
            talkNames = new List<string>();
            ElisaState=STATE_ELISA.STANDAR;
            TypeOfShoutsElisa();
            TypeOfTalksElisa();

        }
    }
    public bool TakeSip(int dmg)
    {
        if (ElisaState == STATE_ELISA.MENTAL_DESTROYED)
        {
            // mas dmg...
        }
        currentSIP -= dmg;
        HUDSystemBattle.instance.setSIPElisa(currentSIP);
        if (currentSIP <= 0)
        {
            currentSIP= 0;
            HUDSystemBattle.instance.setSIPElisa(currentSIP);
            return true;
        }
        else if (currentSIP >= 100)
        {
            currentSIP= 100;
            HUDSystemBattle.instance.setSIPElisa(currentSIP);
            return false;
        }else
        {
            return false;
        }
    }
  
    public bool TakeAnx(int dmg)
    {
        if(ElisaState==STATE_ELISA.MENTAL_DESTROYED)
        {
            // mas anx...
        }
        currentANX += dmg;
        HUDSystemBattle.instance.setANXElisa(currentANX);
        if (currentANX >= 100)
        {
            currentANX = 100;
            HUDSystemBattle.instance.setANXElisa(currentANX);
            return true;
        } else if (currentANX <= 0)
        {
            currentANX = 0;
            HUDSystemBattle.instance.setANXElisa(currentANX);
            return false;
        }
        else
        {
            return false;
        }
    }

    public void TakeItem(ItemClass item)
    {
        TakeAnx(item.getAnxietyPoints());
        TakeSip(item.getSIPPoints());

    }
    public void TypeOfShoutsElisa()
    {
        attacksShoutDMG.Add(15);
        attacksShoutDMG.Add(25);
        attacksShoutDMG.Add(40);
        attacksShoutDMG.Add(50);
        attacksShoutNames.Add("Subida de tono");
        attacksShoutNames.Add("Grito a caraperro");
        attacksShoutNames.Add("¿BASTA YA, NO?");
        attacksShoutNames.Add("ME CAGO EN TODO LO *****************!!!!!!!!!!!@#@#");


    }

    public void TypeOfTalksElisa()
    {
        talkDMG.Add(15);
        talkDMG.Add(25);
        talkDMG.Add(40);
        talkDMG.Add(50);
        talkNames.Add("Dialogo");
        talkNames.Add("Reflexión");
        talkNames.Add("Menuda chapa");
        talkNames.Add("Planteamiento de crisis existencial");
    }
   

}
