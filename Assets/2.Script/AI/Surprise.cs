using UnityEngine;

namespace Mosquito.AI
{
    public class Surprise : Node
    {
        public Surprise(Tree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            Debug.Log("Attack Node Invoke "+ blackboard.isAttack);
            throw new System.NotImplementedException();
        }
    }
}