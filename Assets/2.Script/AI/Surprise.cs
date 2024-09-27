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
            if (blackboard.controller.isHit)
            {
                Debug.Log("Surprise Seccese!");
                blackboard.controller.isHit = false;
                return Result.Success;
            }

            return Result.Failure;
            //throw new System.NotImplementedException();
        }
    }
}