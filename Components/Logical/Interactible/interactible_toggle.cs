using System;

public abstract partial class InteractibleToggleable : Interactible {
    public bool active;

    public void Toggle() {
        active = !active;
    }
}