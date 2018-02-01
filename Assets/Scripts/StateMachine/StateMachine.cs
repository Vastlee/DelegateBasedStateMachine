/* Description: Setup to be a generic State Machine, what can use a pre-written state, but also has delegates 
 * attached to each transition that can be assigned to as well. 
 * Brogrammer: Vast
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vast.StateMachine {
    /// <summary>
    /// Maintains List of States, handles Adding, Removing, and Changing of Active States. Default State is NONE &amp; 
    /// </summary>
    [Serializable]
    public class StateMachine : IStateMachine {
        private State activeState;
        private State previousState;
        private State noneState;
        private List<State> states = new List<State>();
        private Action<State> onStateChange = null;
        private Action<State> onStateAdd = null;
        private Action<State> onStateRem = null;

        #region Properties
        public State ActiveState { get { return this.activeState; } }
        public State PreviousState { get { return this.previousState; } }
        public State NoneState { get { return this.noneState; } }
        public List<State> States { get { return this.states; } set { this.states = value; } }
        public Action<State> OnStateChange { get { return this.onStateChange; } set { this.onStateChange = value; } }
        public Action<State> OnStateAdd { get { return this.onStateAdd; } set { this.onStateAdd = value; } }
        public Action<State> OnStateRem { get { return this.onStateRem; } set { this.onStateRem = value; } }
        #endregion

        #region Constructors
        public StateMachine() {
            this.noneState = AddState(new State("NONE"));
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Adds a state to the State Machine & returns reference to the State Added.
        /// </summary>
        /// <param name="stateToAdd"></param>
        /// <returns></returns>
        public State AddState(State stateToAdd) {
            State addedState = null;
            if(ContainsState(stateToAdd)) {
                Debug.LogError("<color=yellow>State [" + stateToAdd.Name + "] NOT Added! Error: Duplicate State Name</color>");
            } else {
                this.states.Add(stateToAdd);
                addedState = stateToAdd;
                if(this.onStateAdd != null) {
                    this.onStateAdd(addedState);
                }
            }
            return addedState;
        }

        /// <summary>
        /// Creates a state & adds it to the State Machine & returns reference to the State Added.
        /// </summary>
        /// <param name="stateNameToAdd"></param>
        /// <returns></returns>
        public State AddState(string stateNameToAdd) {
            State addedState = null;
            if(ContainsState(stateNameToAdd)) {
                Debug.LogError("<color=yellow>State [" + stateNameToAdd + "] NOT Added! Error: Duplicate State Name</color>");
            } else {
                State newState = new State(stateNameToAdd);
                addedState = AddState(newState);
            }
            return addedState;
        }

        /// <summary>
        /// Adds State(s) to the State Machine & returns an array of the State(s) Added.
        /// </summary>
        /// <param name="statesToAdd"></param>
        /// <returns></returns>
        public State[] AddStates(params State[] statesToAdd) {
            List<State> addedStates = new List<State>();
            State addedState = null;
            for(int i = 0; i < statesToAdd.Length; i++) {
                addedState = AddState(statesToAdd[i]);
                if(addedState != null) {
                    addedStates.Add(addedState);
                }
            }
            return addedStates.ToArray();
        }

        /// <summary>
        /// Adds State(s) by name to the StateMachine & returns an array of the State(s) Added.
        /// </summary>
        /// <param name="stateNamesToAdd"></param>
        /// <returns></returns>
        public State[] AddStates(params string[] stateNamesToAdd) {
            List<State> addedStates = new List<State>();
            State addedState = null;
            for(int i = 0; i < stateNamesToAdd.Length; i++) {
                addedState = AddState(stateNamesToAdd[i]);
                if(addedState != null) {
                    addedStates.Add(addedState);
                }
            }
            return addedStates.ToArray();
        }

        /// <summary>
        /// Removes State from State Machine
        /// </summary>
        /// <param name="stateToRemove"></param>
        public void RemState(State stateToRemove) {
            if(ContainsState(stateToRemove)) {
                this.states.Remove(stateToRemove);
                if(this.onStateRem != null) {
                    this.onStateRem(stateToRemove);
                }
            } else {
                Debug.LogError("<color=yellow>State [" + stateToRemove.Name + "] NOT Removed! Error: State Not Found In StateMachine</color>");
            }
        }

        /// <summary>
        /// Removes State by String Name from State Machine
        /// </summary>
        /// <param name="stateNameToRemove"></param>
        public void RemState(string stateNameToRemove) {
            State stateToRemove = null;
            if(ContainsState(stateNameToRemove, out stateToRemove)) {
                RemState(stateToRemove);
            } else {
                Debug.LogError("<color=yellow>State [" + stateNameToRemove + "] NOT Removed! Error: State Not Found In StateMachine</color>");
            }
        }

        /// <summary>
        /// Removes Array of States from State Machine
        /// </summary>
        /// <param name="statesToRemove"></param>
        public void RemStates(params State[] statesToRemove) {
            foreach(State stateToRemove in statesToRemove) {
                RemState(stateToRemove);
            }
        }

        /// <summary>
        /// Removes Array of States by string Names from State Machine
        /// </summary>
        /// <param name="stateNamesToRemove"></param>
        public void RemStates(params string[] stateNamesToRemove) {
            foreach(string stateNameToRemove in stateNamesToRemove) {
                RemState(stateNameToRemove);
            }
        }

        /// <summary>
        /// Changes Active State & Processes Transitions
        /// </summary>
        /// <param name="toState"></param>
        public void ChangeState(State toState) {
            if(ContainsState(toState)) {
                if(this.activeState != null) {
                    this.activeState.ExitState();
                    this.previousState = this.activeState;
                }
                this.activeState = toState;
                if(this.onStateChange != null) {
                    this.onStateChange(toState);
                }
                this.activeState.EnterState();
            } else {
                Debug.LogError("<color=yellow>StateMachine does not contain an entry for: " + toState + "</color>");
            }
        }

        /// <summary>
        /// Changes Active State by Name & Processes Transitions
        /// </summary>
        /// <param name="toState"></param>
        public void ChangeState(string toStateName) {
            State toState = null;
            if(ContainsState(toStateName, out toState)) {
                ChangeState(toState);
            } else {
                Debug.LogError("<color=yellow>StateMachine does not contain an entry for: " + toStateName + "</color>");
            }
        }

        /// <summary>
        /// Calls UpdateState in the active State.
        /// </summary>
        public void UpdateActiveState() {
            if(this.activeState != null) {
                this.activeState.UpdateState();
            }
        }

        /// <summary>
        /// Does the State Machine contain this State?
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool ContainsState(State state) {
            for(int i = 0; i < this.states.Count; i++) {
                if(this.states[i] == state) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Does the State Machine contain a State with this name?
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public bool ContainsState(string stateName) {
            for(int i = 0; i < this.states.Count; i++) {
                if(this.states[i].Name == stateName) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Does the State Machine contain a State with this name? Assigns out if so.
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="foundState"></param>
        /// <returns></returns>
        public bool ContainsState(string stateName, out State foundState) {
            foundState = null;
            for(int i = 0; i < this.states.Count; i++) {
                if(this.states[i].Name == stateName) {
                    foundState = this.states[i];
                    break;
                }
            }
            return (foundState != null);
        }
        #endregion
    }

}
