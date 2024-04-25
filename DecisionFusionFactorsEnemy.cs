using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Factors
{


public abstract class DecisionFusionFactorsEnemy : ScriptableObject
{
    // Start is called before the first frame update
    public string Name;
    private float finalWeight;

    public float calculateWeight
    {
        get { return finalWeight; }
        set
        {
            finalWeight = Mathf.Clamp01(value); // de esta manera el peso sera siempre o 0 o 1
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        finalWeight = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract float WeightFactors(EnemyBattle enemy);
}
}
