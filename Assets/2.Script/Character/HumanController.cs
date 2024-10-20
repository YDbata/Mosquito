using System;
using Mosquito.AI;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Tree = Mosquito.AI.Tree;

namespace Mosquito.Character
{
    public class HumanController : CharacterController
    {
        private Tree tree;
        [SerializeField] Transform sittingPoint;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform hips;
        private bool IsSit;
        public float sitheight;
        public bool _isSit
        {
            get { return animator.GetBool(AnimationStrings.IsSit); }
            set
            {
                IsSit = value;
                animator.SetBool(AnimationStrings.IsSit, value);
                //hips.position = sittingPoint.position;
            }
        }

        [Header("AI")] [SerializeField] private float radius = 5f;
        [SerializeField] private float angle = 120f;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] public bool attackZoneCol = false;
        [Header("Seek관련(agent stop Distance보다 작게)")]
        [SerializeField] private float seekDistanceLimit = 1.5f;
        [SerializeField] private Rig headRig; 

        [Header("Patrol wayPoint")] [SerializeField]
        private Transform[] wayPoints;
        [SerializeField]float walkingPointRadius = 2f;
        
        private void Start()
        {
            tree = gameObject.AddComponent<Tree>();
            tree.StartBuild().Selector("Selector").Sequence("Suprise").IsState(State.Suprise).child = new Surprise(tree);
                
            //.IsAttackRange(attackRadius, angle, targetMask)
            //.Attack();

            
            #region AttackSquence
            
            ((Selector)tree.root.child).children.Add(new Sequence(tree, "Attack"));
            ((Sequence)((Selector)tree.root.child).children[1])
                .children.Add(new IsAttackRange(tree, angle, targetMask));
            ((Sequence)((Selector)tree.root.child).children[1])
                .children.Add(new Attack(tree));
            #endregion
            
            #region SeekSequence
            
            ((Selector)tree.root.child).children.Add(new Sequence(tree, "Seek"));
            ((Sequence)((Selector)tree.root.child).children[2])
                .children.Add(new EyeDetectionObject(tree, radius, angle, targetMask, headRig));
            ((Sequence)((Selector)tree.root.child).children[2])
                .children.Add(new Seek(tree, seekDistanceLimit, headRig));
            
            #endregion 
            
            #region Idle(Patrol)
            
            ((Selector)tree.root.child).children.Add(new Sequence(tree, "Idle"));
            ((Sequence)((Selector)tree.root.child).children[3])
                .children.Add(new IsState(tree, State.Idle));
            ((IDecoration)((Sequence)((Selector)tree.root.child).children[3]).children[0]).child = new Patrol(tree, wayPoints, walkingPointRadius);
            #endregion
            
            
            
        }
        
        
        private void OnDrawGizmos()
        {
            
            DrawArcGizmos(radius, angle, Color.yellow);

            if (tree)
            {
                if (tree.blackboard.target)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, tree.blackboard.target.position);
                }
            }
            Gizmos.DrawWireCube(transform.position + new Vector3(0,0.8f,0) + Vector3.forward*-0.3f, new Vector3(0.8f, 0.7f, 0.7f));
        }
        
        private void DrawArcGizmos(float radius, float angle, Color color)
        {
            Gizmos.color = color;
            Vector3 left = Quaternion.Euler(0f, -angle / 2.0f, 0.0f) * transform.forward;
            Vector3 right = Quaternion.Euler(0f, angle / 2.0f, 0.0f) * transform.forward;

            int segements = 10;
            Vector3 prev = transform.position + left * radius;
            for (int i = 0; i < segements; i++)
            {
                float ratio = (float)(i + 1) / segements;
                float theta = Mathf.Lerp(-angle / 2.0f, angle / 2.0f, ratio);
                Vector3 dir = Quaternion.Euler(0f, theta, 0f) * transform.forward;
                Vector3 next = transform.position + dir * radius;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(transform.position, transform.position + left * radius);
            Gizmos.DrawLine(transform.position, transform.position + right * radius);

            
        }

        private void OnTriggerEnter(Collider other)
        {
            //모기의 공격과 부딪혀 들어온 충격에 대해서는 Hit처리를 진행한다.
            // 1. 부딪힌 collider가 Attack의 레이어번호와 맞는지 확인하기
            // 2. Surprise Tree에 결과 전달 및 물린 위치 받아서 기억하기
            //Debug.Log(other.gameObject.layer + "numm");
            if (other.gameObject.layer == 9)
            {
                Debug.Log("HitHit");
                state = State.Suprise;
            }
        }
    }
    
    
}