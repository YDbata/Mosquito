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
            Debug.Log("Attack Node Invoke "+ blackboard.isAttack);
            if (blackboard.isAttack)
            {
                blackboard.animator.SetTrigger("Attack");
                blackboard.isAttack = false;
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