using System;
using Mosquito.Character;
using UnityEngine;

namespace Mosquito.AI
{
    public class Attack : Node
    {
        public Attack(Tree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            Debug.Log("Attack Node Invoke ");
            if (blackboard.controller.attackZoneCol)
            {
                blackboard.controller.state = State.Attack;
                //blackboard.animator.SetTrigger("Attack");
                //blackboard.isAttack = false;
                //blackboard.controller.ChangeState(State.Idle);
                blackboard.controller.attackZoneCol = false;
                return Result.Success;
            }
            else
            {
                //blackboard.animator.SetInteger("State", 0);
                return Result.Failure;
            }
        }
    }
}