using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Factors;

namespace AI.FactorFusions
{

    [CreateAssetMenu(fileName = "AnxietyFactorToCry", menuName = "AI/Factors/Anxiety Factor To Cry")]
    public class AnxietyFactorToCry : DecisionFusionFactorsEnemy
    {
        
        public override float WeightFactors(EnemyBattle enemy)
        {

            calculateWeight = Mathf.Clamp01(calculateEcuationExponential(enemy.CurrentANX, 1));
            Debug.Log("Weight Anxiety to cry: " + calculateWeight);//la funcion calculateEcuation se encargará de evaluar toda la movideta.
            return calculateWeight; // aqui va la función que devuelve el peso que indicara que el enemigo quiere atacar/curarse/blablabla.....
        }

        public float calculateEcuationExponential(int currentANX, float exponent) //x ^ e
        {
            return (Mathf.Pow((currentANX/100), exponent)); //Funcion exponencial x^e siendo X la cantidad de SIP..
        }

    }
}