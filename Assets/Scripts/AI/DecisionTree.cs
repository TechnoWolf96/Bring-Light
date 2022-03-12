

public abstract class TreeNode
{
    public object agent;
    public TreeNode(object agent)
    {
        this.agent = agent;
    }
    public abstract void MakeDecision();
}

public abstract class Branch : TreeNode
{
    protected Branch(object agent) : base(agent){}

    public abstract TreeNode GetBranch();

    public override void MakeDecision()
    {
        TreeNode newNode = GetBranch();
        newNode.MakeDecision();
    }
}


public abstract class Action : TreeNode
{
    protected Action(object agent) : base(agent){}

    public abstract void DoAction();

    public override void MakeDecision() => DoAction();
}

public class DecisionTree
{
    public TreeNode root;

    public void MakeDecision()
    {
        root.MakeDecision();
    }
}
