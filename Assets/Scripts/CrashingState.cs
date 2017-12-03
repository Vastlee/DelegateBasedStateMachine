using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashingState : State {
    static string stateName = "Crashing";

    #region Constructors
    public CrashingState() : base(stateName, null) { }
    public CrashingState(string name) : base(name, null) { }
    public CrashingState(StateMachine machine) : base(stateName, machine) { }
    public CrashingState(string name, StateMachine machine) : base(name, machine) { }
    #endregion

    #region State Methods
    public override void EnterState() {
        base.EnterState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void UpdateState() {
        base.UpdateState();
    }

    public override void FixedUpdateState() {
        base.FixedUpdateState();
    }
    #endregion
}
