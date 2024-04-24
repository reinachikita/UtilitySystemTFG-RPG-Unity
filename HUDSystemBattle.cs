using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HUDSystemBattle : MonoBehaviour
{
    // Start is called before the first frame update
    public static HUDSystemBattle instance;
    public TextMeshProUGUI textBattleAnxietyUS, textBattleSIPUS, textLvlPAUS, textCurrentPAUS,textName; //UI Player
    public TextMeshProUGUI textBattleAnxietyEN, textBattleSIPEN, textLvlPAEN, textNameEnemy; //UI Enemy
    private Button Shout, Talk, Cry; //Types of actions
    public List<GameObject> ShoutsPlayer; //types of shouts.
   [SerializeField] private EventSystem eventSystem; //navegation buttons
    public Selectable currentButton; //navegation buttons part 2
    public GameObject typeShouts, typeTalks; //objecto donde estara la los botones
    public GameObject canvas;
    public void Awake()
    {
        if(instance== null)
        {
            instance = this;
            ShoutsPlayer= new List<GameObject>();
          
            
        }


        
    }
    public void Start()
    {
        eventSystem = EventSystem.current;
       // currentButton = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
    }

    public void Update()
    {
     /*   if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SelectNextButton(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SelectNextButton(-1);
        }*/
    }
    void SelectNextButton(int direction)
    {
        Selectable nextButton = currentButton.FindSelectable(new Vector3(direction, 0 ,0));
        if (nextButton != null)
        {
            nextButton.Select();
            currentButton = nextButton;
             //cuando se seleccione el boton se pone mas transparente
            
        }
    }

    public void inicialzeUITypes(GameObject type) //inicializa los botones con el nombre de los ataques.
    {
        
       //Instantiate(objectParent, objectParent.transform.position, objectParent.transform.rotation, canvas.transform);
       type.SetActive(true);
    }

  



    public void startUIBattle()
    {
    
        textBattleAnxietyUS.text = GameManager.instance.currentAnxiety.ToString();
        textBattleSIPUS.text = GameManager.instance.currentSip.ToString();
        textLvlPAUS.text = GameManager.instance.lvlPA.ToString();
        textCurrentPAUS.text = GameManager.instance.currentPA.ToString();
        textName.text = "Elisa";
        textBattleAnxietyEN.text=EnemyBattle.instance.CurrentANX.ToString();
        textBattleSIPEN.text=EnemyBattle.instance.CurrentSIP.ToString();
        textNameEnemy.text = EnemyBattle.instance.Name;
        textLvlPAEN.text = 1.ToString();
    }

  

    public void setSIPEnemy(int currentSip)
    {
        textBattleSIPEN.text = currentSip.ToString();
    }
    public void setANXEnemy(int currentAnx)
    {
        textBattleAnxietyEN.text = currentAnx.ToString();
    }
    public void setSIPElisa(int currentSip)
    {
        textBattleSIPUS.text = currentSip.ToString();
    }
    public void setANXElisa(int currentAnx)
    {
        textBattleAnxietyUS.text = currentAnx.ToString();
    }
}
