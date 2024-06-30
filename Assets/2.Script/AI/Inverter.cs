using UnityEngine;

namespace Mosquito.AI
{
    public class Inverter : Node, IDecoration
    {
        public Inverter(Tree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            switch (child.Invoke())
            {
                case Result.Failure:
                    return Result.Success;
                case Result.Success:
                    return Result.Failure;
                case Result.Running:
                    return Result.Running;
                
            }
            return Result.Success;
        }


        public Node child { get; set; }
    }
}