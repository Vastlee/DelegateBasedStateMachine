using UnityEngine;

namespace Vast.StateMachine {
    [CreateAssetMenu]
    public class ExampleStateSave : ScriptableObject {
        #pragma warning disable 0414
        [SerializeField] private string stateName = string.Empty;
        #pragma warning restore 0414
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