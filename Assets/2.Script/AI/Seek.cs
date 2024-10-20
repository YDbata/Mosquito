using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace Mosquito.AI
{
    public class Seek : Node
    {
        private float _distanceLimit;
        private Rig headRig;
        
        public Seek(Tree tree, float distanceLimit, Rig headRig) : base(tree)
        {
            
            _distanceLimit = distanceLimit;
            this.headRig = headRig;
        }

        public override Result Invoke()
        {
            float distance_target = Vector2.Distance(
                new Vector2(blackboard.target.position.x, blackboard.target.position.z),
                new Vector2(blackboard.transform.position.x, blackboard.transform.position.z));
            // 쳐다 보는 단계X
            if (blackboard.agent.stoppingDistance >= distance_target)
            {
                //blackboard.transform.LookAt(blackboard.target);
                blackboard.animator.SetFloat(AnimationStrings.Velocity, 0f);
                blackboard.agent.ResetPath();
                Debug.Log("Success Look");
                return Result.Success;
            }
            // 
            if (blackboard.target &&
                 distance_target <= _distanceLimit) //Vector3.Distance(blackboard.transform.position, blackboard.target.position)
            {
                if (NavMesh.SamplePosition(blackboard.target.position, 
                        out NavMeshHit hit,
                        2.0f,
                        NavMesh.AllAreas))
                {
                    
                    blackboard.agent.isStopped = false;
                    blackboard.agent.SetDestination(hit.position);
                    blackboard.animator.SetFloat(AnimationStrings.Velocity, 1.5f);
                    // 시선 고정
                    //headRig.weight = Mathf.Lerp(headRig.weight, 1.00f, 1 * Time.deltaTime);
                    Debug.Log("쫓기 성공");
                    return Result.Running;
                }
            }

            blackboard.target = null;
            blackboard.agent.isStopped = true;
            blackboard.animator.SetFloat(AnimationStrings.Velocity, 0f);
            blackboard.animator.SetInteger("State", 1);
            // 시선 고정 해제
            //headRig.weight = Mathf.Lerp(headRig.weight, 0.00f, 1 * Time.deltaTime);
            return Result.Failure;
        }
    }
}