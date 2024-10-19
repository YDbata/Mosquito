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
            // AttackZone collider에 Mosquito가 있은지 00초가 지난 시점 공격 모션 실행
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