using Godot;

public class StateMachine
{
    public State active_state;

    // Necessary because each process is executed at different times
    public bool changing_state = false;
    
    public StateMachine(State defaultstate) {
        this.active_state = defaultstate;
        active_state._EnterState();
    }

    public void ChangeState(State new_state) {
        active_state._ExitState();
        active_state = new_state;
        active_state._EnterState();
    }
    public void Input_Process() {
        active_state._InputProcess();
    }
    public void Process(double delta) {
        State possible_new_state = active_state._process_state(delta);
        if (possible_new_state != null){
            ChangeState(possible_new_state);}
    }
    public void Physics_Process(double delta) {
        State possible_new_state = active_state._physics_process_state(delta);
        if (possible_new_state != null){
            ChangeState(possible_new_state);}
    }
}
