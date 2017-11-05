/* Description: State Machine
 * Brogrammer: Vast
 */

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine {
    private State activeState;
    private Dictionary<string, State> statesByName = new Dictionary<string, State>();
    private List<State> states = new List<State>();
    private Action<State> onStateChange = null;
    private Action<State> onStateAdd = null;
    private Action<State> onStateRem = null;
    private Stack<State> history = new Stack<State>();

    #region Properties
    public State ActiveState { get { return this.activeState; } }
    public Dictionary<string, State> StatesByName { get { return this.statesByName; } }
    public List<State> States { get { return this.states; } set { this.states = value; } }
    public Action<State> OnStateChange { get { return this.onStateChange; } set { this.onStateChange = value; } }
    public Action<State> OnStateAdd { get { return this.onStateAdd; } set { this.onStateAdd = value; } }
    public Action<State> OnStateRem { get { return this.onStateRem; } set { this.onStateRem = value; } }
    #endregion

    #region Constructors & Factory Methods
    private StateMachine() { }

    public static StateMachine NewStateMachine() {
        StateMachine machine = new StateMachine();
        State noneState = new State("NONE");
        noneState.Machine = machine;
        machine.statesByName.Add(noneState.Name, noneState);
        machine.states.Add(noneState);
        return machine;
    }
    #endregion



    #region Class Methods
    public State AddState(State stateToAdd) {
        State addedState = null;
        if(stateToAdd.Name == "NONE") {
            Debug.LogError("<color=yellow>State [" + stateToAdd.Name + "] NOT Added! Error: NONE is a reserved state name</color>");
        } else if(this.statesByName.ContainsKey(stateToAdd.Name)) {
            Debug.LogError("<color=yellow>State [" + stateToAdd.Name + "] NOT Added! Error: Duplicate State Name</color>");
        } else {
            stateToAdd.Machine = this;
            this.statesByName.Add(stateToAdd.Name, stateToAdd);
            this.states.Add(stateToAdd);
            addedState = stateToAdd;
        }
        return addedState;
    }

    public State AddState(string stateNameToAdd) {
        State addedState = null;
        if(this.statesByName.ContainsKey(stateNameToAdd)) {
            Debug.LogError("<color=yellow>State [" + stateNameToAdd + "] NOT Added! Error: Duplicate State Name</color>");
        } else {
            State newState = new State(stateNameToAdd);
            addedState = AddState(newState);
        }
        return addedState;
    }

    // Add a State to the State Machine
    public State[] AddStates(params State[] statesToAdd) {
        List<State> addedStates = new List<State>();
        State addedState;
        foreach(State stateToAdd in statesToAdd) {
            addedState = AddState(stateToAdd);
            if(addedState != null) { addedStates.Add(addedState); }
        }
        return addedStates.ToArray();
    }

    public State[] AddStates(params string[] statesToAdd) {
        List<State> addedStates = new List<State>();
        State addedState;
        foreach(string stateToAdd in statesToAdd) {
            addedState = AddState(stateToAdd);
            if(addedState != null) { addedStates.Add(addedState); }
        }
        return addedStates.ToArray();
    }

    public void RemState(State stateToRemove) {
        if(this.statesByName.ContainsKey(stateToRemove.Name)) {
            this.states.Remove(stateToRemove);
            this.statesByName.Remove(stateToRemove.Name);
        }
    }

    public void RemState(string stateNameToRemove) {
        if(this.statesByName.ContainsKey(stateNameToRemove)) {
            RemState(this.StatesByName[stateNameToRemove]);
        }
    }

    public void RemStates(params State[] statesToRemove) {
        foreach(State stateToRemove in statesToRemove) {
            RemState(stateToRemove);
        }
    }

    public void RemStates(params string[] stateNamesToRemove) {
        foreach(string stateNameToRemove in stateNamesToRemove) {
            RemState(stateNameToRemove);
        }
    }

    public void ChangeState(State toState) {
        if(this.statesByName.ContainsKey(toState.Name)) {
            if(this.activeState != null) {
                this.activeState.ExitState();
            }
            this.history.Push(toState);
            this.activeState = toState;
            this.activeState.EnterState();
        } else {
            Debug.LogError("<color=yellow>StateMachine does not contain an entry for: " + toState + "</color>");
        }
    }

    public void ChangeState(string toState) {
        if(this.statesByName.ContainsKey(toState)) {
            ChangeState(this.statesByName[toState]);
        } else {
            Debug.LogError("<color=yellow>StateMachine does not contain an entry for: " + toState + "</color>");
        }
    }

    public void UpdateActiveState() {
        if(this.activeState != null) {
            this.activeState.UpdateState();
        }
    }

    public void FixedUpdateActiveState() {
        if(this.activeState != null) {
            this.activeState.FixedUpdateState();
        }
    }

    /// <summary>
    /// Does the State Machine contain this State?
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool ContainsState(State state) {
        return this.statesByName.ContainsValue(state);
    }

    /// <summary>
    /// Does the State Machine contain a State with this name?
    /// </summary>
    /// <param name="stateName"></param>
    /// <returns></returns>
    public bool ContainsState(string stateName) {
        return this.statesByName.ContainsKey(stateName);
    }

    /// <summary>
    /// Does the State Machine contain this State?
    /// </summary>
    /// <param name="state"></param>
    /// <param name="foundState"></param>
    /// <returns></returns>
    public bool ContainsState(State state, out State foundState) {
        foundState = null;
        bool result = this.statesByName.ContainsValue(state);
        if(result) { foundState = this.statesByName[state.Name]; }
        return result;
    }

    /// <summary>
    /// Does the State Machine contain a State with this name?
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="foundState"></param>
    /// <returns></returns>
    public bool ContainsState(string stateName, out State foundState) {
        foundState = null;
        bool result = this.statesByName.ContainsKey(stateName);
        if(result) { foundState = this.statesByName[stateName]; }
        return result;
    }
    #endregion
}