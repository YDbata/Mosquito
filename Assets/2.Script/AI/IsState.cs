using Mosquito.Character;

namespace Mosquito.AI
{
    public class IsState: Node, IDecoration
    {
        private State trueState;
        public IsState(Tree tree, State state) : base(tree)
        {
            trueState = state;
        }

        public override Result Invoke()
        {
            if (blackboard.controller.state == trueState)
            {
                return child.Invoke();
            }

            return Result.Failure;
        }

        public Node child { get; set; }
    }
}