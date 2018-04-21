using UnityEngine;

namespace Vast.StateMachine {
    [CreateAssetMenu]
    public class ExampleStateSave : ScriptableObject {
        [SerializeField] private string stateName = string.Empty;
        private State saveState;

        #region Properties
        public State SaveState {
            get { return this.saveState; }
            set {
                this.saveState = value;
                this.stateName = value.Name;
            }
        }
        #endregion
    }
}