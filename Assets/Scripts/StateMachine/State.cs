/* Description: Base State Class. Has 3 Transision methods & 3 transistion events.
 */

namespace Vast.StateMachine {
    public class State : IState {
        private string name = string.Empty;
        private System.Action onEnter = () => { };
        private System.Action onExit = () => { };
        private System.Action onUpdate = () => { };

        #region Properties
        public string Name { get { return this.name; } protected set { this.name = value; } }
        public System.Action OnEnter { get { return this.onEnter; } set { this.onEnter = value; } }
        public System.Action OnExit { get { return this.onExit; } set { this.onExit = value; } }
        public System.Action OnUpdate { get { return this.onUpdate; } set { this.onUpdate = value; } }
        #endregion

        #region Constructors
        public State() { }
        public State(string stateName) {
            this.name = stateName;
        }
        #endregion

        #region Class Methods
        public virtual void EnterState() {
            OnEnter();
        }

        public virtual void ExitState() {
            OnExit();
        }

        public virtual void UpdateState() {
            OnUpdate();
        }
        #endregion
    }
}