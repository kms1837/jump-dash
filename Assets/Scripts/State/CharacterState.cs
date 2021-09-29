using UnityEngine;

public class CharacterState :IState
{
    protected PlayerController characher;

    public CharacterState() {
    }

    public void init(PlayerController setCharacher) {
        characher = setCharacher;
    }

    public virtual void enter() {

    }

    public virtual void exit() {

    }

    public virtual void update() {
    }

    public virtual void fixedUpdate() {

    }

    public virtual void collisionEnter(Collision other) {
    }
}
