using System.Collections;
using UnityEngine;
using TMPro;


public class BattleManager : MonoBehaviour
{
    //quiero enfocar:
    //Start battle es para inicializar todas las estancias necesarias para realizar un combate
    //Elisa turn basicamente es el menu de opciones desplegadas para que elisa haga lo que sea (llorar,gritar o hablar...)
    //OnShoutButton refleja que pasa cuando presionas el boton de gritar (se despliegan los gritos y se elige)
    //Elisa Shout refleja que es lo que ocurre cuando Elisa grita al enemigo, TELA TELITA TELAAA
   public enum STATE {START, PLAYERTURN, ENEMYTURN, WON, LOST};
    public static BattleManager Instance;   
    public GameObject enemy, player, gameObjthis;
    public TextMeshProUGUI ElisaText,EnemyText;
    public EnemyBattle enemyI;
    public playerBattle playerI;
    public HUDSystemBattle hudSystem;
    public STATE battleState;
    public int selectBtton;

    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
           
        }
        hudSystem= GetComponent<HUDSystemBattle>();  
    }

    private void Start()
    {

        int semilla = 123;
        selectBtton = -1;
        Random.InitState(semilla);
        


        battleState = STATE.START;
        StartCoroutine(StartBattle());
    }
    //START TURNS
   


    //IENUMERATORS FROM BATTLE
   public IEnumerator StartBattle()
    {
        battleState = STATE.START;
        GameObject i= Instantiate(enemy);
        GameObject i2= Instantiate(player);
        enemyI= i.GetComponent<EnemyBattle>();
        playerI= i2.GetComponent<playerBattle>();
        hudSystem.startUIBattle();
        ElisaText.text = "INICIO DEL COMBATE VS " + enemyI.Name;
        yield return new WaitForSeconds(1.5f);
        ElisaTurn();
        

    }
    public IEnumerator ElisaShoutInic()
    {
        if (battleState == STATE.PLAYERTURN)
        {
            yield return new WaitForSeconds(0.1f);
            HUDSystemBattle.instance.inicialzeUITypes(HUDSystemBattle.instance.typeShouts);

        }



    }
    public IEnumerator ElisaTalkInic()
    {

        if (battleState == STATE.PLAYERTURN)
        {
            yield return new WaitForSeconds(0.1f);
            HUDSystemBattle.instance.inicialzeUITypes(HUDSystemBattle.instance.typeTalks);

        }

    }

    public IEnumerator EnemyShout()
    {

        if(battleState == STATE.ENEMYTURN)
        {
            ElisaText.text = "Enemigo atacando al jugador";
            int i = Random.Range(0, 4); // Genera un número entre 1 (inclusive) y 5 (exclusivo)

            Debug.Log("Número aleatorio: " + i);

            yield return new WaitForSeconds(1f);
            bool isDeadElisa = playerBattle.instance.TakeSip(EnemyBattle.instance.attacksShoutDMG[i]);
            yield return new WaitForSeconds(1f);
            ElisaText.text = "player recibe daño!: "+ EnemyBattle.instance.attacksShoutDMG[i];

            if (isDeadElisa)
            {
                ElisaText.text = "Vaya... menudo mierdon has perdido";
                battleState = STATE.LOST;
            }
            else
            {
                battleState = STATE.PLAYERTURN;
            }
            
            
            
        }
        
    }

    public IEnumerator EnemyTalk()
    {
       
        if (battleState == STATE.ENEMYTURN)
        {
            ElisaText.text = "Enemigo hablando al jugador";
            int i = Random.Range(0, 4); // Genera un número entre 1 (inclusive) y 5 (exclusivo)

            Debug.Log("Número aleatorio: " + i);

            yield return new WaitForSeconds(1f);
            bool isAnxElisa = playerBattle.instance.TakeAnx(EnemyBattle.instance.attacksShoutDMG[i]);
            yield return new WaitForSeconds(1f);
            ElisaText.text = "player recibe Ansiedad!: " + EnemyBattle.instance.attacksShoutDMG[i];
          

            if (isAnxElisa)
            {
                ElisaText.text = "ATAQUE DE ANSIEDAD, ELISA CUIDAO";
                playerBattle.instance.ElisaState = playerBattle.STATE_ELISA.MENTAL_DESTROYED;
                ElisaTurn();
            }
            else 
            {

                ElisaTurn();
            }
           



        }

    }

    public IEnumerator SelectElisaShout(int i)
    {
        //Destroy(HUDSystemBattle.instance.objectParent);
        i = selectBtton;
        Debug.Log("ATACÓ con el boton: "+i);
        bool isDead = EnemyBattle.instance.TakeSip(playerBattle.instance.attacksShoutDMG[i]);
        Debug.Log(EnemyBattle.instance.CurrentSIP);
        HUDSystemBattle.instance.setSIPEnemy(EnemyBattle.instance.CurrentSIP);

        ElisaText.text = "(El player ha atacado...   y ha realizado" + playerBattle.instance.attacksShoutDMG[i]+ "de daño";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            ElisaText.text = "Enhorabuena has ganado!";
            battleState = STATE.WON;
        }
        else
        {
            battleState = STATE.ENEMYTURN;
            EnemyBattle.instance.ExecuteNextActionAI();
        }

    }
   
   public IEnumerator SelectElisaTalk(int i)
    {
        i = selectBtton;
        Debug.Log("elgio el boton: " + i);
        bool isAnxious = EnemyBattle.instance.TakeAnx(playerBattle.instance.talkDMG[i]);
        HUDSystemBattle.instance.setANXEnemy(EnemyBattle.instance.CurrentANX);

     
        ElisaText.text = "(El player ha HABLADO... haciendo )"+ playerBattle.instance.talkDMG[i] +"de ansiedad";
        yield return new WaitForSeconds(2f);
        if (isAnxious)
        {
            battleState = STATE.ENEMYTURN;
            EnemyBattle.instance.enemyState = EnemyBattle.STATEENEMY.MENTAL_DESTROYED;
            EnemyBattle.instance.ExecuteNextActionAI();

        }
        else
        {
            battleState = STATE.ENEMYTURN;
            EnemyBattle.instance.ExecuteNextActionAI();
        }

       
       
    }
   


   public IEnumerator ElisaTalk()
    {
        ElisaText.text = "(Gritando no voy a conseguir nada... venga joder)";
        yield return new WaitForSeconds(2f);
        battleState = STATE.ENEMYTURN;
        EnemyBattle.instance.ExecuteNextActionAI();


    }
   public IEnumerator ElisaCry()
    {
        ElisaText.text = "T_________________T";
        yield return new WaitForSeconds(2f);
        playerBattle.instance.TakeSip(-10);
        HUDSystemBattle.instance.setSIPElisa(playerBattle.instance.currentSIP);
        playerBattle.instance.TakeAnx(-10);
        HUDSystemBattle.instance.setANXElisa(playerBattle.instance.currentANX);

        
        ElisaText.text = "Elisa ha llorado y por lo tanto tiene menos ansiedad";
        battleState= STATE.ENEMYTURN;
        EnemyBattle.instance.ExecuteNextActionAI();




    }

   public IEnumerator EnemyCry()
    {
        if(battleState==STATE.ENEMYTURN)
        {
            ElisaText.text = "Enemigo llorando T_________________T";
        
            yield return new WaitForSeconds(2f);
            EnemyBattle.instance.TakeSip(-10);
            HUDSystemBattle.instance.setSIPEnemy(EnemyBattle.instance.CurrentSIP);
            EnemyBattle.instance.TakeAnx(-10);
            HUDSystemBattle.instance.setANXEnemy(EnemyBattle.instance.CurrentANX);


            ElisaText.text = "Enemigo ha llorado y por lo tanto tiene menos ansiedad";
            ElisaTurn();
          
        }
      



       
    }
    #region ELISAPLAYHERTURN
    public void ElisaTurn()
    {

        battleState = STATE.PLAYERTURN;
        ElisaText.text = "Turno del jugador...";

    }


    //CLICKS ON BUTTONS
    public void OnClickElisaShout(GameObject UIM)
    {



        Debug.Log("boton: " + selectBtton);

        StartCoroutine(SelectElisaShout(selectBtton));
        UIM.SetActive(false);
    }

    public void OnClickselectButtonClick(int i)
    {
        selectBtton = i;
    }

    public void OnClickElisaTalk(GameObject UIM)
    {

        Debug.Log("boton talk: " + selectBtton);
        StartCoroutine(SelectElisaTalk(selectBtton));
        UIM.SetActive(false);
    }





    public void OnClickElisaCry()
    {
        if (battleState == STATE.PLAYERTURN)
        {
            HUDSystemBattle.instance.typeTalks.SetActive(false);
            HUDSystemBattle.instance.typeShouts.SetActive(false);
            StartCoroutine(ElisaCry());
        }

    }

    public void OnClickTypeShout()
    {
        if (battleState == STATE.PLAYERTURN)
        {
            Debug.Log(isActiveAndEnabled);
            HUDSystemBattle.instance.typeTalks.SetActive(false);
            StartCoroutine(ElisaShoutInic());

        }

    }
    public void OnClickTypeTalk()
    {
        if (battleState == STATE.PLAYERTURN)
        {
            HUDSystemBattle.instance.typeShouts.SetActive(false);
            StartCoroutine(ElisaTalkInic());

        }

    }
    public void onClickOnPressInventory()
    {
        if (battleState == STATE.PLAYERTURN)
        {
            if (Inventory.Instance.inventory.active)
            {
                Inventory.Instance.inventory.SetActive(false);
            }
            else
            {
                Inventory.Instance.inventory.SetActive(true);
            }
        }


    }

    public void onClickQuitMenu(GameObject UIMenu)
    {

        Debug.Log("quitamos menu");
        UIMenu.SetActive(false);

    }
    #endregion





}
