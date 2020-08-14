using Vast.StateMachine;

public class Jumping : State {
    private const string DEFAULT_NAME = "Jumping";

    public Jumping(string name = DEFAULT_NAME) {
        Name = name;
    }

    public override void OnEnter() {    }

    public override void OnExit() {    }

    public override void Update() {    }

    public override void FixedUpdate() { }

}
