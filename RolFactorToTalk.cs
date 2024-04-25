using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Factors;

namespace AI.FactorDecision
{


    [CreateAssetMenu(fileName = "RolFactorToTalk", menuName = "AI/Factors/Rol Factor To Talk")]
    public class RolFactorToTalk : DecisionFusionFactorsEnemy
    {

        public override float WeightFactors(EnemyBattle enemy)
        {


            calculateWeight = Mathf.Clamp01(calculateEcuationLineal(enemy));
            //la funcion calculateEcuation se encargar� de evaluar toda la movideta.
            return calculateWeight; // aqui va la funci�n que devuelve el peso que indicara que el enemigo quiere atacar/curarse/blablabla.....
        }

        public float calculateEcuationLineal(EnemyBattle enemy)
        {
            for (int i = 0; i < enemy.matrizRolValues.GetLength(0); i++)
            {
                for (int j = 0; j < enemy.matrizRolValues.GetLength(1); j++)
                {
                    if (enemy.Rol == enemy.matrizRolValues[i, j])
                    {
                        Debug.Log("weight from rol TO TALK: " + 0.10);
                        return 0.10f;
                    }
                }
            }
            Debug.Log("Throw Error: rol dont found");
            return 0f;
        }
    }
}