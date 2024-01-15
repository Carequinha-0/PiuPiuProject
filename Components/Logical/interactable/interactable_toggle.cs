using System;

public abstract partial class InteractableToggleable : Interactable {
    public bool active;

    public void Toggle() {
        active = !active;
    }
}