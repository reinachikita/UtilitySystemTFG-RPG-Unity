using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Factors;
namespace AI.FactorDecision
{

    [CreateAssetMenu(fileName = "SIPFactorToShout", menuName = "AI/Factors/SIP Factor To Shout")]
    public class SIPFactorToShout : DecisionFusionFactorsEnemy
    {
       
        
        public override float WeightFactors(EnemyBattle enemy)
        {
            calculateWeight = Mathf.Clamp01(calculateEcuationExponential(enemy.CurrentSIP, Mathf.Epsilon));
            Debug.Log("Weight Sip to shout: " + calculateWeight); //la funcion calculateEcuation se encargará de evaluar toda la movideta.
            return calculateWeight; // aqui va la función que devuelve el peso que indicara que el enemigo quiere atacar/curarse/blablabla.....
        }

        public float calculateEcuationExponential(float currentSIP, float exponent) //x ^ e + 0.2
        {
            return currentSIP/100;  //Funcion exponencial x^e siendo X la cantidad de SIP..
        }
  
    }
}