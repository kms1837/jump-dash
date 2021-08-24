using UnityEngine;

public class CharacterState :IState
{
    protected PlayerController characher;

    public CharacterState(PlayerController setCharacher) {
        characher = setCharacher;
    }

    public virtual void init() {

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
