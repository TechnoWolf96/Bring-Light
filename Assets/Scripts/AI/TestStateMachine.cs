using UnityEngine;

public class TestStateMachine : MonoBehaviour
{
    public bool seeEnemy;
    public bool lowHealth;
    public bool enemyClose;

    public string message;
    public bool showMessage;

    private void Start()
    {
        State state = new Idle_State(this);
        state.enabled = true;
    }

    protected class RunToEnemy_State : State
    {
        TestStateMachine agent;
        public RunToEnemy_State(TestStateMachine agent)=> this.agent = agent;

        public override void OnDisable()
        {
            agent.showMessage = false;
        }

        public override void OnEnable()
        {
            agent.message = "Working RunToEnemy_State";
        }

        public override void SetTransitions()
        {
            transitions.Add(new Transition(new NotSee_Condition(agent), new Idle_State(agent)));
            transitions.Add(new Transition(new SeeAndLowHealth_Condition(agent), new RunFromEnemy_State(agent)));
        }

        public override void Update()
        {
            if (agent.showMessage)
            {
                print(agent.message);
                agent.showMessage = false;
            }
        }
    }
    protected class RunFromEnemy_State : State
    {
        TestStateMachine agent;
        public RunFromEnemy_State(TestStateMachine agent) => this.agent = agent;

        public override void OnDisable()
        {
            agent.showMessage = false;
        }

        public override void OnEnable()
        {
            agent.message = "Working RunFromEnemy_State";
        }

        public override void SetTransitions()
        {
            transitions.Add(new Transition(new NotSee_Condition(agent), new Idle_State(agent)));
            transitions.Add(new Transition(new SeeAndNotLowHealth_Condition(agent), new RunToEnemy_State(agent)));
        }

        public override void Update()
        {
            if (agent.showMessage)
            {
                print(agent.message);
                agent.showMessage = false;
            }
        }
    }
    protected class Idle_State : State
    {
        TestStateMachine agent;
        public Idle_State(TestStateMachine agent) => this.agent = agent;

        public override void OnDisable()
        {
            agent.showMessage = false;
        }

        public override void OnEnable()
        {
            agent.message = "Working Idle_State";
        }

        public override void SetTransitions()
        {
            transitions.Add(new Transition(new SeeAndLowHealth_Condition(agent), new RunFromEnemy_State(agent)));
            transitions.Add(new Transition(new SeeAndNotLowHealth_Condition(agent), new RunToEnemy_State(agent)));
        }

        public override void Update()
        {
            if (agent.showMessage)
            {
                print(agent.message);
                agent.showMessage = false;
            }
        }
    }
    protected class SeeAndNotLowHealth_Condition : Condition
    {
        TestStateMachine agent;
        public SeeAndNotLowHealth_Condition(TestStateMachine agent) => this.agent = agent;
        public override bool CheckCondition()
        {
            if (agent.seeEnemy && !agent.lowHealth) return true;
            return false;
        }
    }
    protected class SeeAndLowHealth_Condition : Condition
    {
        TestStateMachine agent;
        public SeeAndLowHealth_Condition(TestStateMachine agent) => this.agent = agent;
        public override bool CheckCondition()
        {
            if (agent.seeEnemy && agent.lowHealth) return true;
            return false;
        }
    }
    protected class NotSee_Condition : Condition
    {
        TestStateMachine agent;
        public NotSee_Condition(TestStateMachine agent) => this.agent = agent;
        public override bool CheckCondition()
        {
            if (!agent.seeEnemy) return true;
            return false;
        }
    }

}
