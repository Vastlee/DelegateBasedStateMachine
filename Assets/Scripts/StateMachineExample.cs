using UnityEngine;

public class StateMachineExample : MonoBehaviour {
    [SerializeField] private StateMachine entityState;

    private State runningState = new State("Running");
    private State jumpingState = new State("Jumping");
    private CrashingState crashingState = new CrashingState();

    public void Awake() {
        // Creates the new StateMachine.
        this.entityState = StateMachine.NewStateMachine();

        // Example of adding a state by Name to a State Machine upon creation
        new State("Idle", this.entityState);

        // Example of adding already created States.
        this.entityState.AddStates(this.runningState, this.jumpingState);

        // Example of adding a new State to a State Machine with just a name.
        this.entityState.AddState("Flying");

        // Example of adding a Class State
        this.entityState.AddState(this.crashingState);
    }

    public void Update() {
        this.entityState.UpdateActiveState();
    }

    public void FixedUpdate() {
        this.entityState.FixedUpdateActiveState();
    }
}
