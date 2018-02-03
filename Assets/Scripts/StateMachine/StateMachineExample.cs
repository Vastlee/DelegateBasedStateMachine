using System.Collections;
using UnityEngine;
using Vast.StateMachine;

public class StateMachineExample : MonoBehaviour {
    [SerializeField] private StateMachine entityState;

    private State runningState = new State("Running");
    private State jumpingState = new State("Jumping");
    private CrashingState crashingState = new CrashingState();

    public void Awake() {
        // Creates the new StateMachine.
        this.entityState = new StateMachine();

        // Example of adding a state by Name to a State Machine upon creation
        this.entityState.AddState(new State("Idle"));

        // Example of adding already created States.
        this.entityState.AddStates(this.runningState, this.jumpingState);

        // Example of adding a new State to a State Machine with just a name.
        this.entityState.AddState("Flying");

        // Example of adding a Class State
        this.entityState.AddState(this.crashingState);

        // Example of switching through states from Code every 2 seconds.
        StartCoroutine(ChangeStatesWitCodeExample(2.0f));
    }

    public void Update() {
        this.entityState.UpdateActiveState();
    }

    private IEnumerator ChangeStatesWitCodeExample(float pauseTime) {
        WaitForSeconds pause = new WaitForSeconds(pauseTime);
        State[] states = this.entityState.States.ToArray();
        for(int i = 0; i < states.Length; i++) {
            this.entityState.ChangeState(states[i]);
            yield return pause;
        }
    }
}
