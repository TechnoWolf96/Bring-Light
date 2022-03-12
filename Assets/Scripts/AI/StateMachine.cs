using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public abstract bool CheckCondition();
}

public class Transition
{
    public Condition condition { get; private set; }
    public State newState { get; private set; }

    public Transition(Condition condition, State newState)
    {
        this.condition = condition;
        this.newState = newState;
    }

}

public abstract class State : MonoBehaviour
{
    public List<Transition> transitions;

    protected void Awake()
    {
        transitions = new List<Transition>();
        SetTransitions();
    }

    public abstract void SetTransitions(); // Здесь помещается настройка переходов

    public abstract void OnEnable();  // Инициализация состояния

    public abstract void OnDisable(); // Финализация состояния

    public abstract void Update();   // Определяется модель поведения

    protected void LateUpdate()
    {
        foreach (var transition in transitions)
        {
            if (transition.condition.CheckCondition())
            {
                transition.newState.enabled = true;
                enabled = false;
                return;
            }
        }
    }


}
