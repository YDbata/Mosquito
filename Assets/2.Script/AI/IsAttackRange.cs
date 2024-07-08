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
            float distance_target = Vector2.Distance(
                new Vector2(blackboard.target.position.x, blackboard.target.position.z),
                new Vector2(blackboard.transform.position.x, blackboard.transform.position.z));
            if (distance_target <= radius)
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