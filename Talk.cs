using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.ActionsObject;
namespace AI.Actions
{

    [CreateAssetMenu(fileName = "Talk", menuName = "AI/Actions/Talk")]
    public class Talk : ActionsEnemy
    {
        public override void Execute(EnemyBattle enemy)
        {
            enemy.EnemyTurnTalk();
        }
    }
}