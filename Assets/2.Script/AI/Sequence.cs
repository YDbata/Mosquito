using System.Collections.Generic;
using UnityEngine;

namespace Mosquito.AI
{
    public class Sequence : Composite
    {
        List<Node> children { get; set; }

        public Sequence(Tree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            Result result = Result.Success;

            for (int i = currentSiblingIndex; i < children.Count; i++)
            {
                result = children[i].Invoke();

                switch (result)
                {
                    case Result.Success:
                        currentSiblingIndex++;
                        break;
                    case Result.Failure:
                        currentSiblingIndex = 0;
                        return result;
                    case Result.Running :
                        return result;
                    default:
                        break;
                }
            }

            currentSiblingIndex = 0;
            return result;
        }
    }
}