using System.Collections.Generic;

public class Sequence : CompositeNode {
    public Sequence(List<Node> children) : base(children) {}

    public override NodeState Evaluate() {
        foreach (var node in children) {
            NodeState result = node.Evaluate();
            if (result == NodeState.Failure) return NodeState.Failure;
            if (result == NodeState.Running) return NodeState.Running;
        }
        return NodeState.Success;
    }
}