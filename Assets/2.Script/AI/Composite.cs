using System.Collections.Generic;
using UnityEngine;

namespace Mosquito.AI
{
    /// <summary>
    /// 말단이 아닌 노드
    /// 자식을 가지는 Selector나 Sequence가 여기 해당됨
    /// </summary>
    public abstract class Composite : Node
    {
        // ???질문 하기 : 마지막으로 실행했던 자식의 인덱스
        // 자식이 Running 상태에서 빠져나온후, 
        // 스택을통해 다시 이 Composite 를 실행해야 할 때 첫 자식부터 실행하지않고
        // 마지막으로 실행했던 자식 다음거부터 실행할 수 있다.
        protected int currentSiblingIndex;
        public List<Node> children { get; set; }
        
        protected Composite(Tree tree) : base(tree)
        {
            children = new List<Node>();
        }
    }
}