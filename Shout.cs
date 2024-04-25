using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.ActionsObject;
namespace AI.Actions {

    [CreateAssetMenu(fileName = "Shout", menuName = "AI/Actions/Shout")]
    public class Shout : ActionsEnemy
{
        public override void Execute(EnemyBattle enemy)
        {
            enemy.EnemyTurnShout();
        }
    }
}
