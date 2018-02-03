﻿using UnityEditor;
using UnityEngine;

namespace Vast.StateMachine {
    /// <summary>
    /// Draws a drop down with all states in the State Machine.
    /// Displays the active state &amp; allows for changing state by selecting new one.
    /// </summary>
    /// <remarks>
    /// Brogrammer: Vast
    /// Created: 15/08/03
    /// Last Modified: 18/02/01
    /// </remarks>
    [CustomPropertyDrawer(typeof(StateMachine))]
    [CanEditMultipleObjects]
    public class StateMachineDrawer : PropertyDrawer {
        private StateMachine stateMachine = null;
        private int currentSelection = 0;
        private bool originalGUIState;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if(this.stateMachine == null) {
                this.stateMachine = this.fieldInfo.GetValue(property.serializedObject.targetObject) as StateMachine;
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PrefixLabel(position, label);
            position.x += EditorGUIUtility.labelWidth; // Moves position over the width of the label
            position.width -= EditorGUIUtility.labelWidth; // Subtracts the width of the label from the position width.
            this.originalGUIState = GUI.enabled;
            GUI.enabled = (this.stateMachine.States.Count > 1); // If it has no states other than the default NONE, it turns the UI off.
            this.currentSelection = EditorGUI.Popup(position, CurrentActiveStateIndex(), GetCurrentStates());
            GUI.enabled = this.originalGUIState;

            if(GUI.changed) { // If another state is selected, it calls for the StateMachine to change.
                this.stateMachine.ChangeState(this.stateMachine.States[this.currentSelection]);
            }
            EditorGUI.EndProperty();

            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.UpdateIfRequiredOrScript();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        /// <summary>
        /// Returns the States Names as a GUIContent array for the popup
        /// </summary>
        /// <returns></returns>
        private GUIContent[] GetCurrentStates() {
            GUIContent[] result = new GUIContent[this.stateMachine.States.Count];
            for(int i = 0; i < result.Length; i++) {
                result[i] = new GUIContent(this.stateMachine.States[i].Name);
            }
            return result;
        }

        /// <summary>
        /// Returns the index # of the current Active State. Or 0 if there is none.
        /// </summary>
        /// <returns></returns>
        private int CurrentActiveStateIndex() {
            int result = 0;
            if(this.stateMachine.ActiveState != null) {
                result = this.stateMachine.States.IndexOf(this.stateMachine.ActiveState);
            }
            return result;
        }
    }
}
