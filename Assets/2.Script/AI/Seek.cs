using UnityEngine;
using UnityEngine.AI;

namespace Mosquito.AI
{
    public class Seek : Node
    {
        private float _distanceLimit;
        private Animator _animator;
        
        public Seek(Tree tree, float distanceLimit, Animator animator) : base(tree)
        {
            _animator = animator;
            _distanceLimit = distanceLimit;
        }

        public override Result Invoke()
        {
            float distance_target = Vector2.Distance(
                new Vector2(blackboard.target.position.x, blackboard.target.position.z),
                new Vector2(blackboard.transform.position.x, blackboard.transform.position.z));
            if (blackboard.agent.stoppingDistance >= distance_target)
            {
                //blackboard.transform.LookAt(blackboard.target);
                _animator.SetInteger("State", 1);
                Debug.Log("Success");
                return Result.Success;
            }
            
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
                    _animator.SetInteger("State", 4);
                    return Result.Running;
                }
            }

            blackboard.target = null;
            blackboard.agent.isStopped = true;
            _animator.SetInteger("State", 1);
            return Result.Failure;
        }
    }
}