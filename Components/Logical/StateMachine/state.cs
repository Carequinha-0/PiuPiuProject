using Godot;

public abstract class State
{
    public Node parent_node;

    public State(Node parent_node) {
        this.parent_node = parent_node;
    }
    public abstract void _EnterState();
    public abstract void _ExitState();
    public abstract void _InputProcess();
    public abstract State _process_state(double delta);
    public abstract State _physics_process_state(double delta);
}