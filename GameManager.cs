using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //el gameManager sirve para controlar las stats del jugador durante fuera del combate
    public static GameManager instance;
    //Stats Character
     public int currentSip, maxSIP;
     public int currentAnxiety,maxAnxiety;
     public int lvlPA, maxPA;
     public int currentPA; //lvl PA es el nivel actual, maxPA es la maxima de experiencia para subir de nivel y current ps la actual
     public string avatarName;
    //Stats enemy
  
    //Player
     public GameObject player;
     

    //Inventory Manager
    //[SerializeField] public GameObject inventory;

    private void Awake()
    {
        if(instance == null)
        {

            instance = this;
            if (!SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("BattleTestScene"))){
                
                Instantiate(player);
                player.transform.position = Vector3.zero;
            }
            
            DontDestroyOnLoad(instance);
            maxAnxiety = 0;
            maxSIP = 100;
            currentAnxiety = 0;
            currentSip = 100;
            lvlPA = 1;
            maxPA = 100;
            currentPA = 0;
            
        }
        else
        {
        
            Debug.Log("ya hay gameManager bonita");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void addAnxiety(int moreAnxiety)
    {
        currentAnxiety += moreAnxiety;
    }

    public void subAnxiety(int lessAnxiety)
    {
        currentAnxiety -=lessAnxiety;
    }

    public void addSip(int moreSip)
    {
        currentSip += moreSip;
    }

    public void subSip(int lessSip)
    {
        currentSip -=lessSip;
    }
    public void addPA(int morePA)
    {
        currentPA += morePA;
    }
    public void subPA(int lessPA)
    {
        currentPA -=lessPA;
    }


    public void onClickActiveDesactiveInventory()
    {
       /* if(inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
       */
    }
}
