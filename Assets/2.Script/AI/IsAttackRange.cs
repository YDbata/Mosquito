using UnityEngine;

namespace Mosquito.AI
{
    public class IsAttackRange : Node
    {
        private float radius;
        private float angle;
        private LayerMask targetMask;
        
        public IsAttackRange(Tree tree, float radius, float angle, LayerMask targetMask) : base(tree)
        {
            this.radius = radius;
            this.angle = angle;
            this.targetMask = targetMask;
        }

        public override Result Invoke()
        {
            if (blackboard.target == null)
            {
                return Result.Failure;
            }
            float distance_target = Vector2.Distance(
                new Vector2(blackboard.target.position.x, blackboard.target.position.z),
                new Vector2(blackboard.transform.position.x, blackboard.transform.position.z));
            if (distance_target <= radius)
            {
                if (IsInSight(blackboard.target.position))
                {
                    blackboard.isAttack = true;
                    return Result.Success;
                }
            }
            else
            {
                blackboard.isAttack = false;
                return Result.Failure;
            }
            blackboard.isAttack = false;
            return Result.Failure;
        }
        
        private bool IsInSight(Vector3 target)
        {
            Vector3 origin = blackboard.transform.position;
            Vector3 forward = blackboard.transform.forward;
            Vector3 lookDir = (target - (origin + new Vector3(0,target.y,0))).normalized;
            float theta = Mathf.Acos(Vector3.Dot(forward, lookDir)) * Mathf.Rad2Deg;
            if (theta < angle / 2.0f )
            {
            
                if (Physics.Raycast(origin+new Vector3(0, target.y, 0),
                        lookDir,
                        out RaycastHit hit,
                        Vector2.Distance(new Vector2(target.x, target.z), new Vector2(origin.x, origin.z)),
                        //sssVector3.Distance(target, origin),
                        targetMask))
                {
                    return true;
                }
            }

            return false;
        }
        
        
        
    }
    
    
}