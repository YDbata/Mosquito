using Mosquito.Character;
using UnityEngine;

namespace Mosquito.AI
{
    public class IsAttackRange : Node
    {
        private bool flag;
        private float angle;
        private LayerMask targetMask;
        
        public IsAttackRange(Tree tree, float angle, LayerMask targetMask) : base(tree)
        {
            this.angle = angle;
            this.targetMask = targetMask;
        }

        public override Result Invoke()
        {
            if (blackboard.target == null)
            {
                return Result.Failure;
            }
            // AttackZone(overlapBox)에 Mosquito가 있은지 00초가 지난 시점 공격 모션 실행
            Collider[] cols =
                Physics.OverlapBox(blackboard.transform.position + new Vector3(0,0.8f,-0.3f), new Vector3(0.8f, 0.7f, 0.7f), blackboard.transform.rotation, targetMask);
            if (cols.Length > 0)
            {
                // foreach (var c in cols)
                // {
                //     if (c.gameObject.layer == 6)
                //     {
                blackboard.controller.attackZoneCol = true;
                        //break;
                //     }
                // }
                
            }
            

            if (blackboard.controller.attackZoneCol)
            {
                
                // TODO : coolTime 기능 구현
                Debug.Log("Attackrange Sussece");
                //blackboard.controller.state = State.Attack;
                return Result.Success;
            }
            return Result.Failure;
        }
    }
    
    
}