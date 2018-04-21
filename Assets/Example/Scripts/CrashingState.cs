/*  Description: Example of a state.
 */

 using Vast.StateMachine;

public class CrashingState : State {
    #region Constructors
    public CrashingState() : base("Crashing") { }
    #endregion

    #region State Methods
    protected override void ExecuteEnter() {
        UnityEngine.Debug.Log("Entering Crashing State");
    }

    protected override void ExecuteExit() {
        UnityEngine.Debug.Log("Exiting Crashing State");
    }

    protected override void ExecuteUpdate() {
        UnityEngine.Debug.Log("Updating Crashing State");
    }
    #endregion
}