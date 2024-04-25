using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Factors;
namespace AI.ActionsObject { 
public abstract class ActionsEnemy : ScriptableObject
{

    public string Name;
    private float finalWeight;
    public DecisionFusionFactorsEnemy [] decisionFusionFactorsEnemyAvaible;
    public float calculateWeight
    {
        get { return finalWeight; }
        set
        {
            finalWeight = Mathf.Clamp01(value); // de esta manera el peso siempre estará entre 0 y 1
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        finalWeight= 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void Execute(EnemyBattle enemy);
    
    }
}