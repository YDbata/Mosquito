using UnityEngine;

namespace Mosquito.AI
{
    public class IsAttackRange : Node
    {
        private float radius;
        public IsAttackRange(Tree tree, float radius) : base(tree)
        {
            this.radius = radius;
        }

        public override Result Invoke()
        {
            if (Vector3.Distance(blackboard.target.position, blackboard.transform.position) <= radius)
            {
                return Result.Success;
            }
            else
            {
                
                return Result.Running;
            }
            return Result.Failure;
        }
    }
}