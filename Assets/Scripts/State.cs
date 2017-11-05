/* Description: State
 * Brogrammer: Vast
 */
using System;

[System.Serializable]
public class State {
    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnUpdate;
    public event Action OnFixedUpdate;

    private string name;
    private StateMachine machine;

    #region Properties
    public string Name { get { return this.name; } }
    public StateMachine Machine { set { this.machine = value; } }
    #endregion

    #region Constructors
    public State() : this("NONE", null) { }
    public State(string name) : this(name, null) { }
    public State(StateMachine machine) : this("NONE", machine) { }
    public State(string name, StateMachine machine) {
        this.name = name;
        this.machine = machine;
        if(this.machine != null && !this.machine.ContainsState(this)) {
            this.machine.AddState(this);
        }
    }
    #endregion

    #region Class Methods
    public virtual void EnterState() {
        if(this.OnEnter != null) { this.OnEnter(); }
    }

    public virtual void ExitState() {
        if(this.OnExit != null) { this.OnExit(); }
    }

    public virtual void UpdateState() {
        if(this.OnUpdate != null) { this.OnUpdate(); }
    }

    public virtual void FixedUpdateState() {
        if(this.OnFixedUpdate != null) { this.OnFixedUpdate(); }
    }

    #endregion
}
