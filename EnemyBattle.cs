using AI.ActionsObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    //Parte stats
    public static EnemyBattle instance;
    public string Name;
    public string Rol;
    public string [,] matrizRolValues = new string[3, 4];   
    public int PA, CurrentSIP, CurrentANX;
    public int maxSIP, maxANX;
    public List<int> attacksShoutDMG;
    public List<string> attacksShoutNames;
    public List<int> talkDMG;
    public List<string> talkNames;
    //Parte STATES
    public enum STATEENEMY {STANDAR, MENTAL_DESTROYED };
    public STATEENEMY enemyState;
    //Parte AI
    public ActionsEnemy[] actionsAvaiable;
    public ActionsEnemy bestAction { get; set; }
    public bool decide { get; set; }



    private void Awake()
    {

        if (instance == null)
        {

            instance = this;
            CurrentSIP = 100;
            CurrentANX= 0;
            attacksShoutDMG = new List<int>();
            attacksShoutNames= new List<string>();
            enemyState = STATEENEMY.STANDAR;
            TypeOfShoutsEnemy();
            inicializeRolValues();
            Rol = "Empatico";
            
  

        }
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

    public void inicializeRolValues()
    {
        matrizRolValues[0, 0] = "Altruista";
        matrizRolValues[0, 1] = "0.25";
        matrizRolValues[0, 2] = "0";
        matrizRolValues[0, 3] = "0";
        matrizRolValues[1, 0] = "Empatico";
        matrizRolValues[1, 1] = "0";
        matrizRolValues[1, 2] = "0.1";
        matrizRolValues[1, 3] = "0.25";
        matrizRolValues[2, 0] = "Egocentrico";
        matrizRolValues[2, 1] = "0";
        matrizRolValues[2, 2] = "0.25";
        matrizRolValues[2, 3] = "0.1";

    }

    #region stats enemy part

    public bool TakeSip(int dmg)
    {
        CurrentSIP -= dmg;
        if (CurrentSIP <= 0)
        {
            CurrentSIP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TakeAnx(int dmg)
    {
        CurrentANX += dmg;
        if (CurrentANX >= 100)
        {
            CurrentANX = 100;
            return true;
        }
        else if (CurrentANX <= 0)
        {
            CurrentANX = 0;
            return false;
        }
        else
        {
            return false;
        }
    }

    public void TypeOfShoutsEnemy()
    {
        attacksShoutDMG.Add(20);
        attacksShoutDMG.Add(25);
        attacksShoutDMG.Add(40);
        attacksShoutDMG.Add(50);
        attacksShoutNames.Add("a");
        attacksShoutNames.Add("b");
        attacksShoutNames.Add("c");
        attacksShoutNames.Add("d");


    }
    #endregion

    void FixedUpdate()
    {
        //AI Funcionamiento
      /*  if (bestAction == null) //se selecciona mejor accion
        {
            DecideBestAction(actionsAvaiable);
        }
        if (decide && BattleManager.Instance.battleState == BattleManager.STATE.ENEMYTURN) //se ejecuta la mejor accion seleccionada
        {
            decide=false;
            Debug.Log("Execute action AI");
            bestAction.Execute(this);
            //System.Threading.Thread.Sleep((5 * 1000));//waits
        }*/


       
    }

    #region AI Enemy Brain

    public float WeightAction(ActionsEnemy action)
    {
        float weight = 0f; //default weight
        for (int i = 0; i < action.decisionFusionFactorsEnemyAvaible.Length; i++)
        {
            //se va seleccionando los pesos de todas los factores de decision de una accion.
            float decisionFusionFactorsWeight = action.decisionFusionFactorsEnemyAvaible[i].WeightFactors(this);
            weight += decisionFusionFactorsWeight; //acumulation weight
            if (weight == 0)
            {
                action.calculateWeight = 0; //si es 0 no tiene sentido seguir calculando
                return action.calculateWeight;
            }
        }

        //Peso total
        //Necesitamos un esquema general para recalcular el peso original de la decision
        //porque al acumularlas cada vez se va tergiversando mas el valor original
        //asi que se recalcula a traves de un factor de modificacion y un valor
        //de compensacion
        float originalWeight = weight;
        float modFactor = 1 - (1 / action.decisionFusionFactorsEnemyAvaible.Length);
        float makeUpValue = (1 - originalWeight) * modFactor;
        action.calculateWeight = originalWeight + (makeUpValue * weight);
        return action.calculateWeight;



    }

    public void DecideBestAction(ActionsEnemy[] actions)
    {
        //Recorrre la lista de todas las acciones para saber cual tiene el peso más alto y la asigna
        float inicialWeight = 0f;
        int BAindex = 0;
        for (int i = 0; i < actions.Length; i++)
        {
            if (WeightAction(actions[i]) > inicialWeight)
            {

                BAindex = i;
                inicialWeight = actions[i].calculateWeight;


            }
        }
        bestAction = actions[BAindex];

        decide = true;
    }
    #endregion

    #region ENEMYPLAYHIMTURN
    public void EnemyTurnShout()
    {

        StartCoroutine(BattleManager.Instance.EnemyShout());
        DecideBestAction(actionsAvaiable);
       
 
    
    }
    public void EnemyTurnTalk()
    {
    
        StartCoroutine(BattleManager.Instance.EnemyTalk());
        DecideBestAction(actionsAvaiable);
       
    }

    public void EnemyTurnCry()
    {
        
        StartCoroutine(BattleManager.Instance.EnemyCry());
        DecideBestAction(actionsAvaiable);
    
    }

    public void ExecuteNextActionAI()
    {
        //AI Funcionamiento
        if (bestAction == null) //se selecciona mejor accion
        {
            DecideBestAction(actionsAvaiable);
        }
        if (decide && BattleManager.Instance.battleState == BattleManager.STATE.ENEMYTURN) //se ejecuta la mejor accion seleccionada
        {
            decide = false;
            Debug.Log("Execute action AI" +bestAction);
            bestAction.Execute(this);
           /* if (bestAction.name == "Cry")
            {
                StartCoroutine(BattleManager.Instance.EnemyCry());
            }

            if(bestAction.name == "Shout")
            {
                StartCoroutine(BattleManager.Instance.EnemyShout());
            }

            if (bestAction.name == "Talk")
            {
                StartCoroutine(BattleManager.Instance.EnemyTalk());
            }*/
            //System.Threading.Thread.Sleep((5 * 1000));//waits
        }
    }

    #endregion



}

