using UnityEngine;

namespace Mosquito.Character
{
    public class HumanController : CharacterController
    {
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