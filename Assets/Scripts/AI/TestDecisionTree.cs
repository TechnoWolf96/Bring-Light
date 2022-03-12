using UnityEngine;

public class TestDecisionTree : MonoBehaviour
{
    DecisionTree tree;

    bool see = false;
    bool lowHealth = true;

    private void Start()
    {
        tree = new DecisionTree();
        tree.root = new Branch_EnemyIsSee(this);
        tree.MakeDecision();
    }



    protected class Branch_EnemyIsSee : Branch
    {
        public Branch_EnemyIsSee(object agent) : base(agent) { }

        public override TreeNode GetBranch()
        {
            TestDecisionTree test = (TestDecisionTree)agent;

            if (test.see) return new Branch_HaveLowHealth(agent);
            else return new Action_DoNothing(agent);
        }
    }
    protected class Branch_HaveLowHealth : Branch
    {
        public Branch_HaveLowHealth(object agent) : base(agent) { }

        public override TreeNode GetBranch()
        {
            TestDecisionTree test = (TestDecisionTree)agent;

            if (test.lowHealth) return new Action_Run(agent);
            else return new Action_Attack(agent);
        }
    }
    protected class Action_DoNothing : Action
    {
        public Action_DoNothing(object agent) : base(agent) { }

        public override void DoAction()
        {
            print("DoNothing");
        }
    }
    protected class Action_Attack : Action
    {
        public Action_Attack(object agent) : base(agent) { }

        public override void DoAction()
        {
            print("Attack");
        }
    }
    protected class Action_Run : Action
    {
        public Action_Run(object agent) : base(agent) { }

        public override void DoAction()
        {
            print("Run");
        }
    }


}
