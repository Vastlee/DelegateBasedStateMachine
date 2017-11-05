using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineExample : MonoBehaviour {
    [SerializeField] private StateMachine entityState;

    private State idleState = new State("Idle");
    private State runningState = new State("Running");
    private State jumpingState = new State("Jumping");

    public void Awake() {
        entityState = StateMachine.NewStateMachine();
        //new State("NONE", entityState); // Example of adding a state to a State Machine upon creation
        entityState.AddStates(idleState, runningState, jumpingState); // Example of adding already created States.
        entityState.AddState("Flying"); // Example of adding a new State with just a name.
    }

    public void Update() {
        entityState.UpdateActiveState();
    }

    public void FixedUpdate() {
        entityState.FixedUpdateActiveState();
    }
}
