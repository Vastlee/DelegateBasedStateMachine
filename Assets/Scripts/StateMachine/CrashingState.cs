/*  Description: Example of a state.
 */

 using Vast.StateMachine;

public class CrashingState : State {
    #region Constructors
    public CrashingState() : base("Crashing") { }
    #endregion

    #region State Methods
    public override void EnterState() {
        base.EnterState();
        UnityEngine.Debug.Log("Entering Crashing State");
    }

    public override void ExitState() {
        base.ExitState();
        UnityEngine.Debug.Log("Exiting Crashing State");
    }

    public override void UpdateState() {
        base.UpdateState();
        UnityEngine.Debug.Log("Updating Crashing State");
    }
    #endregion
}