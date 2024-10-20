using UnityEngine;
using UnityEngine.AI;

namespace Mosquito.AI
{
    public class Patrol : Node
    {
        private Transform[] wayPoints;
        private float walkingPointRadius;
        private int currentWayPoint;
        private float walkSpeed = 1f;
        private float currentSpeed;
        private float randomCoolTime;
        private float currentTime;
        
        public Patrol(Tree tree, Transform[] wayPoints, float walkingPointRadius) : base(tree)
        {
            this.wayPoints = wayPoints;
            this.walkingPointRadius = walkingPointRadius;
            currentWayPoint = 0;
            randomCoolTime = 0f;// Random.Range(0, 10);
            currentTime = 0;
            currentSpeed = walkSpeed;
        }

        public override Result Invoke()
        {
            Debug.Log(currentWayPoint);
            currentSpeed = walkSpeed;
            blackboard.agent.speed = currentSpeed;
            
            if (wayPoints.Length == 1)
            {
                if (Vector3.Distance(wayPoints[currentWayPoint].position, blackboard.transform.position) < walkingPointRadius)
                {
                    blackboard.transform.rotation = Quaternion.Lerp(blackboard.transform.rotation, 
                        wayPoints[currentWayPoint].rotation, Time.deltaTime*5);
            
                    if (currentSpeed > 0.01f)
                    {
                        currentSpeed = Mathf.Lerp(currentSpeed, 0.00f, 3 * Time.deltaTime);
                    }
                    else
                    {
                        currentSpeed = 0f;
                    }
                    blackboard.animator.SetFloat(AnimationStrings.Velocity, currentSpeed);
                    
                }
            }else if(Vector3.Distance(wayPoints[currentWayPoint].position, blackboard.transform.position)
                     < walkingPointRadius)
            
            {
            
                if (currentTime >= randomCoolTime)
                {
                    currentWayPoint++;
                    if (currentWayPoint >= wayPoints.Length)
                    {
                        currentWayPoint = 0;
                    }
                    randomCoolTime = Random.Range(0, 6);
                    //Debug.Log("randomcoolTime " + randomCoolTime);
                    currentTime = 0;
                }
                else
                {
                    currentTime += Time.deltaTime;
                    currentSpeed = 0;//Mathf.Lerp(currentSpeed, 0.00f, 0.2f * Time.deltaTime);
                }
                
            }
            blackboard.agent.ResetPath();
            blackboard.agent.SetDestination(wayPoints[currentWayPoint].position);
            //if (!blackboard.agent.pathPending)
            blackboard.animator.SetFloat(AnimationStrings.Velocity, currentSpeed);
            return Result.Success;
        }
        
    }
}