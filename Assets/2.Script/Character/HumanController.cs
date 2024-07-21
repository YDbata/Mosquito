using System;
using Mosquito.AI;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
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
        [SerializeField] private float attackRadius = 0.7f;
        [SerializeField] private float seekDistanceLimit = 1.5f;

        private void Start()
        {
            tree = gameObject.AddComponent<Tree>();
            tree.StartBuild().Selector().Sequence().IsAttackRange(attackRadius, angle, targetMask)
                .Attack();
            ((Selector)tree.root.child).children.Add(new Sequence(tree));
            ((Sequence)((Selector)tree.root.child).children[1])
                .children.Add(new EyeDetectionObject(tree, radius, angle, targetMask));
            ((Sequence)((Selector)tree.root.child).children[1])
                .children.Add(new Seek(tree, seekDistanceLimit, animator));
        }

        // override void Update()
        // {
        //     if (_isSit)
        //     {
        //         hips.position = sittingPoint.position;
        //         hips.position -= new Vector3(0, sitheight,0);
        //         transform.position = hips.position;
        //     }
        // }
        
        
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
    }
    
    
}