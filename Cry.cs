using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.ActionsObject;
namespace AI.Actions
{

    [CreateAssetMenu(fileName = "Cry", menuName = "AI/Actions/Cry")]
    public class Cry : ActionsEnemy
    {
        public override void Execute(EnemyBattle enemy)
        {
            enemy.EnemyTurnCry();
        }
    }
}