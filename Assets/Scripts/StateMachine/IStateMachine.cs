namespace Vast.StateMachine {
    public interface IStateMachine {
        State AddState(State stateToAdd);
        State AddState(string stateNameToAdd);
        State[] AddStates(params State[] statesToAdd);
        State[] AddStates(params string[] stateNamesToAdd);
        void RemState(State stateToRemove);
        void RemState(string stateNameToRemove);
        void RemStates(params State[] statesToRemove);
        void RemStates(params string[] stateNamesToRemove);
        void ChangeState(State toState);
        void ChangeState(string toStateName);
        void UpdateActiveState();
        bool ContainsState(State state);
        bool ContainsState(string stateName);
        bool ContainsState(string stateName, out State foundState);
    }
}
