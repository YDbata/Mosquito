using Mosquito.Character;
using UnityEngine;

namespace Mosquito.AI
{
    public class EyeDetectionObject : Node
    {
        private float radius;
        private float angle;
        private LayerMask targetMask;
        
        
        public EyeDetectionObject(Tree tree, float radius, float angle, LayerMask targetMask) : base(tree)
        {
            this.radius = radius;
            this.angle = angle;
            this.targetMask = targetMask;
        }

        public override Result Invoke()
        {
            if (blackboard.animator.GetInteger(AnimationStrings.State) == (int)State.Suprise)
            {
                return Result.Failure;
            }
            Collider[] cols =
                Physics.OverlapCapsule(blackboard.transform.position,
                    blackboard.transform.position + new Vector3(0,1,0),
                    radius,
                    targetMask);
            if (cols.Length > 0)
            {
                if (IsInSight(cols[0].transform.position))
                {
                    blackboard.target = cols[0].transform;
                    //Debug.Log("Detection");
                    blackboard.animator.SetLayerWeight(1, 0f);
                    return Result.Success;
                }
            }
            //Debug.Log("layer weight ch");
            blackboard.animator.SetLayerWeight(1, 1f);
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