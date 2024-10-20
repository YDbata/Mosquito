using Mosquito.Character;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Mosquito.AI
{
    public class EyeDetectionObject : Node
    {
        private float radius;
        private float angle;
        private LayerMask targetMask;
        private Rig headRig;
        
        
        public EyeDetectionObject(Tree tree, float radius, float angle, LayerMask targetMask, Rig headRig) : base(tree)
        {
            this.radius = radius;
            this.angle = angle;
            this.targetMask = targetMask;
            this.headRig = headRig;
        }

        public override Result Invoke()
        {
            if (blackboard.animator.GetInteger(AnimationStrings.State) == (int)State.Suprise)
            {
                return Result.Failure;
            }
            // player가 반경안으로 들어왔는지 확인
            Collider[] cols =
                Physics.OverlapCapsule(blackboard.transform.position,
                    blackboard.transform.position + new Vector3(0,1,0),
                    radius,
                    targetMask);
            if (cols.Length > 0)
            {
                // 반경안에서도 지정한 angle안에 있는지 확인
                if (IsInSight(cols[0].transform.position))
                {
                    blackboard.target = cols[0].transform;
                    //Debug.Log("Detection");
                    //blackboard.animator.SetLayerWeight(1, 0f);
                    headRig.weight = Mathf.Lerp(headRig.weight, 1.00f, 1 * Time.deltaTime);
                    return Result.Success;
                }
            }
            //Debug.Log("layer weight ch");
            //blackboard.animator.SetLayerWeight(1, 1f);
            headRig.weight = Mathf.Lerp(headRig.weight, 0.00f, 1 * Time.deltaTime);
            return Result.Failure;
        }
        
        /// <summary>
        /// target이 blackboard의 주변 범위 내로 들어왔는지 확인하는 함수
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
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