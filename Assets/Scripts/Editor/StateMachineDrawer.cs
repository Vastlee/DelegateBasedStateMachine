/* Description: Quick Drawer to interface with the state machine.
 * Displays Current State, State Status, and provides drop down for
 * switching states.
 * Brogrammer: Vast
 */

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StateMachine))]
[CanEditMultipleObjects]
public class StateMachineDrawer : PropertyDrawer {
    private StateMachine stateMachine;
    private GUIContent[] states;
    private int currentSelection = 0;
    private int previousSelection = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        if(this.stateMachine == null) {
            this.stateMachine = this.fieldInfo.GetValue(property.serializedObject.targetObject) as StateMachine;
            if(this.stateMachine != null) {
                this.stateMachine.OnStateChange += this.OnStateChange;
                this.stateMachine.OnStateAdd += this.OnStateAdd;
                this.stateMachine.OnStateRem += this.OnStateRem;
                UpdateStatesList();
            } else {
                return;
            }
        }

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, label);
        position.x += EditorGUIUtility.labelWidth;
        position.width -= EditorGUIUtility.labelWidth;
        this.currentSelection = EditorGUI.Popup(position, this.currentSelection, this.states);
        EditorGUI.EndProperty();

        if(this.currentSelection != this.previousSelection) {
            this.stateMachine.ChangeState(this.stateMachine.States[this.currentSelection]);
            this.previousSelection = this.currentSelection;
        }
    }

    /// <summary>
    /// Returns the States Names as a string array
    /// </summary>
    /// <returns></returns>
    private void UpdateStatesList() {
        int stateCount = this.stateMachine.States.Count;
        this.states = new GUIContent[stateCount];
        for(int i = 0; i < stateCount; i++) {
            this.states[i] = new GUIContent(this.stateMachine.States[i].Name);
        }
    }

    private void OnStateChange(State changeToState) {
        this.currentSelection = this.stateMachine.States.IndexOf(this.stateMachine.ActiveState);
    }

    private void OnStateAdd(State stateAdded) {
        UpdateStatesList();
    }


    private void OnStateRem(State stateRemoved) {
        UpdateStatesList();
    }
}