using Mosquito.Character;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = UnityEngine.CharacterController;

namespace Mosquito.AI
{
    /// <summary>
    /// 다른 노드에서 다루는 타겟을 공유하기 위해 작성
    /// </summary>
    public class Blackboard
    {
        // owner : 트리를 실행한 주체
        public Tree tree;
        public Transform transform;
        public NavMeshAgent agent;
        //public CharacterController controller;
        public HumanController controller;
        public Animator animator;
        
        // target : 트리 안에서 판단을 위해 접근하는 주체
        public Transform target;
        
        
        public Blackboard(Tree tree)
        {
            this.tree = tree;
            this.transform = tree.GetComponent<Transform>();
            this.agent = tree.GetComponent<NavMeshAgent>();
            //this.controller = tree.GetComponent<CharacterController>();
            this.controller = tree.GetComponent<HumanController>();
            this.animator = tree.GetComponent<Animator>();
            this.target = null;
        }
    }
}