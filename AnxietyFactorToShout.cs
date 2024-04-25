using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Factors;

namespace AI.FactorFusions
{

    [CreateAssetMenu(fileName = "AnxietyFactorToShout", menuName = "AI/Factors/Anxiety Factor To Shout")]
    public class AnxietyFactorToShout : DecisionFusionFactorsEnemy
    {

        public override float WeightFactors(EnemyBattle enemy)
        {
            calculateWeight = Mathf.Clamp01(calculateEcuationExponentialDecrecient(0.25f, enemy.CurrentANX));
            Debug.Log("Weight Anxiety to Shout: " + calculateWeight); //la funcion calculateEcuation se encargará de evaluar toda la movideta.
            return calculateWeight; // aqui va la función que devuelve el peso que indicara que el enemigo quiere atacar/curarse/blablabla.....
        }
        public float calculateEcuationExponentialDecrecient(float currentANX, float exponent) // 0.1^ANX (DECRECIENTE) + ansiedad = menos prob de gritar
        {
            if(exponent == 0) {
                return 0f;
            }
            return (Mathf.Pow(currentANX, (float) (exponent/100))); //Funcion exponencial x^e siendo X la cantidad de SIP..
        }

    }
}












