using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mosquito.AI
{
    
    public enum Result
    {
        Failure,
        Success,
        Running
    }
    /// <summary>
    /// 말단 노드에 대한 코드
    /// 자식이 없다
    /// 자식이 있을 경우 Composite로 구현
    /// </summary>
    public abstract class Node
    {
        protected Tree tree;
        protected Blackboard blackboard;
        
        public Node(Tree tree)
        {
            this.tree = tree;
            this.blackboard = tree.blackboard;
        }

        public abstract Result Invoke();
    }
}
