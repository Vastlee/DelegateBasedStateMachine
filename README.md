# StateMachine
Unity StateMachine that is Class Based (State) with the typical Enter/Exit/Update/FixedUpdate Methods, 
but each state also has a transition event that can be subscribed to. 
This allows not only the creation of new states, but other code to attach it's own methods to be 
executed on the various state changes.

## Custom Editor
GameObjects using the StateMachine type will have a drop down with the all of the current States.
Selecting one will switch to that state.