using System.Collections.Generic;

public class Selector : CompositeNode {
    public Selector(List<Node> children) : base(children) {}

    public override NodeState Evaluate() {
        foreach (var node in children) {
            NodeState result = node.Evaluate();
            if (result == NodeState.Success) return NodeState.Success;
            if (result == NodeState.Running) return NodeState.Running;
        }
        return NodeState.Failure;
    }
}