namespace Vast.StateMachine {
    public interface IState {
        void EnterState();
        void ExitState();
        void UpdateState();
    }
}
