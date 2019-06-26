using System.Collections;
using UnityEngine;
using Vast.StateMachine;

public class StateMachineExample : MonoBehaviour {
    private const float PAUSE_TIME = 1.5f;

    [SerializeField] private StateMachine entityState;
    [SerializeField] private ExampleStateSave save;

    readonly private State runningState = new State("Running");
    readonly private State jumpingState = new State("Jumping");
    readonly private State crashingState = new CrashingState();

    public void Awake() {
        // Creates the new StateMachine.
        this.entityState = new StateMachine();

        this.entityState.OnStateChange += UpdateStateSave;

        // Example of adding a state by Name to a State Machine upon creation
        this.entityState.AddState(new State("Idle"));

        // Example of adding already created States.
        this.entityState.AddStates(this.runningState, this.jumpingState);

        // Example of adding a new State to a State Machine with just a name.
        this.entityState.AddState("Flying");

        // Example of adding a Class State
        this.entityState.AddState(this.crashingState);

        // For Testing Only: Loads previously saved state if it exists.
        this.entityState.ChangeState(this.save.SaveState.Name);

        // For Testing Only: Cycles through the states to demonstrate visual changes from code
        StartCoroutine(CycleStates(PAUSE_TIME));
    }

    public void Update() {
        this.entityState.UpdateActiveState();
    }

    private IEnumerator CycleStates(float pauseTime) {
        var pause = new WaitForSeconds(pauseTime);
        var states = this.entityState.States;

        int index = 0;
        while (true) {
            this.entityState.ChangeState(states[index++]);
            if (index >= states.Count) { index = 0; }
            yield return pause;
        }
    }

    private void UpdateStateSave(State newState) {
        this.save.SaveState = newState;
    }
}