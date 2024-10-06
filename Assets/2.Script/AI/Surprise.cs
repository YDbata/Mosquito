using Mosquito.Character;
using UnityEngine;

namespace Mosquito.AI
{
    public class Surprise : Node
    {
        public Surprise(Tree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            //Debug.Log("Surprise Node Invoke "+ blackboard.isAttack);

            Debug.Log("Surprise Seccese!");
            blackboard.animator.SetInteger(AnimationStrings.State, (int)State.Suprise);
            blackboard.controller.ChangeState(State.Idle);
            return Result.Success;
            
            //throw new System.NotImplementedException();
        }
    }
}