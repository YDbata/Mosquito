using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mosquito.AI
{
    
    public class Tree : MonoBehaviour
    {
        public Blackboard blackboard;
        public Stack<Node> stack; //running인 상태를 모아서 계속체크하기 위함
        public Root root;
        public bool isrunning; // CTick이 코루틴으로 실행 중일 때 중복 실행을 멈취줌 


        private Node _current;
        private Stack<Composite> _composites;
        private void Update()
        {
            if (!isrunning)
            {
                isrunning = true;
                StartCoroutine(C_Tick());
            }
        }

        private IEnumerator C_Tick()
        {
            stack.Clear();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                Result result = current.Invoke();

                if (result == Result.Running)
                {
                    stack.Push(current);
                    yield return null;
                }
            }

            isrunning = false;
        }

        
        #region Build
        public Tree StartBuild()
        {
            blackboard = new Blackboard(this);
            stack = new Stack<Node>();
            root = new Root(this);
            _current = root;
            _composites = new Stack<Composite>();
            return this;
        }

        public void Attach(Node parent, Node child)
        {
            if (parent is Root)
            {
                ((Root)parent).child = child;
            }else if (parent is Composite)
            {
                ((Composite)parent).children.Add(child);
            }
            else
            {
                throw new System.Exception($"[Tree] : {parent.GetType()} has no child");
            }
        }
        
        #endregion

        #region Composite&Deco

        public Tree Selector()
        {
            Composite selector = new Selector(this);
            Attach(_current, selector);
            _composites.Push(selector);
            _current = selector;
            return this;
        }

        public Tree Sequence()
        {
            Composite sequence = new Sequence(this);
            Attach(_current, sequence);
            _composites.Push(sequence);
            _current = sequence;
            return this;
        }

        public Tree Inverter()
        {
            Node inverter = new Inverter(this);
            Attach(_current, inverter);
            _current = inverter;
            return this;
        }
        
        #endregion

        #region ActionNode

        /// <summary>
        /// 기본 모션으로 1~3초동안 랜덤으로 좌우를 둘러보다가 랜덤 지점 wayPoint로 걸어가서
        /// 1번 둘러보고 방의 중앙을 향해 돌아 둘러보기 반복 
        /// </summary>
        /// <returns></returns>
        public Tree BasicMotion()
        {
            
            return this;
        }

        public Tree Detection(float radius, float angle, LayerMask targetMask)
        {
            Node detection = new DetectionObject(this, radius, angle, targetMask);
            Attach(_current, detection);
            _current = _composites.Count > 0 ? _composites.Peek() : null;
            return this;
        }

        public Tree IsAttackRange(float radius)
        {
            Node isAttack = new IsAttackRange(this, radius);
            Attach(_current, isAttack);
            _current = _composites.Count > 0 ? _composites.Peek() : null;
            return this;
        }

        public Tree Seek()
        {
            return this;
        }

        public Tree Attack()
        {
            return this;
        }

        #endregion
    }
}