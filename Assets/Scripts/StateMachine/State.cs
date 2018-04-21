/* Description: Base State Class. Has 3 Transision methods & 3 transistion events.
 */

using System;

namespace Vast.StateMachine {
    [System.Serializable]
    public class State : IState {
        private string name = string.Empty;
        private Action onEnter;
        private Action onExit;
        private Action onUpdate;

        #region Properties
        public string Name {
            get { return this.name; }
            protected set { this.name = value; }
        }
        public Action OnEnter {
            get { return this.onEnter; }
            protected set { this.onEnter = value; }
        }
        public Action OnExit {
            get { return this.onExit; }
            protected set { this.onExit = value; }
        }
        public Action OnUpdate {
            get { return this.onUpdate; }
            protected set { this.onUpdate = value; }
        }
        #endregion

        #region Constructors
        public State() { }
        public State(string stateName) {
            this.name = stateName;
        }
        #endregion

        #region Class Methods
        public void EnterState() {
            ExecuteEnter();
            if(this.OnEnter != null) {
                OnEnter();
            }
        }

        protected virtual void ExecuteEnter() { }

        public void ExitState() {
            ExecuteExit();
            if(this.OnExit != null) {
                OnExit();
            }
        }

        protected virtual void ExecuteExit() { }

        public void UpdateState() {
            ExecuteUpdate();
            if(this.OnUpdate != null) {
                OnUpdate();
            }
        }

        protected virtual void ExecuteUpdate() { }

        #endregion
    }
}