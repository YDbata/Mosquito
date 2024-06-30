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

        [Header("AI")] [SerializeField] private LayerMask targetMask;

        private void Start()
        {
            tree = gameObject.AddComponent<Tree>();
            tree.StartBuild().Selector().Sequence().Detection(5, 90, targetMask);
        }

        private void Update()
        {
            if (_isSit)
            {
                hips.position = sittingPoint.position;
                hips.position -= new Vector3(0, sitheight,0);
                transform.position = hips.position;
            }
        }
    }
}