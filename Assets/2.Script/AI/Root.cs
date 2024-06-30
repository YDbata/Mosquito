using UnityEngine;

namespace Mosquito.AI
{
    public class Root : Node
    {
        public Root(Tree tree) : base(tree){}
        
        public Node child { get; set; }

        public override Result Invoke()
        {
            tree.stack.Push(child);
            return Result.Success;
        }
    }
}