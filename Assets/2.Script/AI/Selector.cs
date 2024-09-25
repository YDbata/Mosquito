using System.Collections.Generic;
using UnityEngine;

namespace Mosquito.AI
{
    public class Selector : Composite
    {
        public Selector(Tree tree, string name) : base(tree, name)
        {
        }

        public override Result Invoke()
        {
            Result result = Result.Failure;
            for (int i = currentSiblingIndex; i < children.Count; i++)
            {
                result = children[i].Invoke();
                switch (result)
                {
                    case Result.Failure:
                        currentSiblingIndex++;
                        break;
                    case Result.Success:
                        currentSiblingIndex = 0;
                        return result;
                    case Result.Running:
                        return result;
                    default:
                        break;
                }
            }

            currentSiblingIndex = 0;
            return result;
        }

        public Node FindChildren(string name)
        {
            for (int i = 0; i < children.Count; i++)
            {
                
                if (name.Equals(children[i].NodeName))
                {
                    return children[i];
                }
            }

            return null;
        }
    }
}