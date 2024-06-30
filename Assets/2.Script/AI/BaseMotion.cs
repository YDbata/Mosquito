using UnityEngine;

namespace Mosquito.AI
{
    /// <summary>
    /// BaseMotion
    /// Idle 1초 -> LookAround 1~3초 --> 모기 탐지시 Sequence넘어감
    /// --> 미탐지시 random wayPoint로 이동 --> LookAround 1회 --> 방 중앙 바라보기 --> 반복
    /// </summary>
    public class BaseMotion : Node
    {
        public BaseMotion(Tree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            
            return Result.Failure;
        }
    }
}