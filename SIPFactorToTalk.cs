using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Factors;
namespace AI.FactorDecision
{

    [CreateAssetMenu(fileName = "SIPFactorToTalk", menuName = "AI/Factors/SIP Factor To Talk")]
    public class SIPFactorToTalk : DecisionFusionFactorsEnemy
    {


        public override float WeightFactors(EnemyBattle enemy)
        {
            calculateWeight = Mathf.Clamp01(calculateEcuationExponentialDecrecient((float)(enemy.CurrentSIP), 2));
            Debug.Log("Weight SipFactor to talk: " + calculateWeight);
            return calculateWeight; // aqui va la función que devuelve el peso que indicara que el enemigo quiere atacar/curarse/blablabla.....
        }

        public float calculateEcuationExponentialDecrecient(float currentSIP, float exponent) //y=x^e
        {
           
            return (Mathf.Pow(currentSIP/100, exponent));// e ^ (x / 100), creciente
        }
    }
}