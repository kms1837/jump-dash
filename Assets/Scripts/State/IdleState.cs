using UnityEngine;

public class IdleState :CharacterState
{

    public IdleState() {
    }

    public override void enter() {
    }

    public override void exit() {

    }

    public override void update() {
        moveInputkey();
        jumpkeyInput();
    }

    public override void fixedUpdate() {

    }

    protected void moveInputkey() {
        characher.move();
        characher.cameraRotation();
    }

    protected void jumpkeyInput() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            characher.jump();
            characher.setState(new JumpingState());
        }
    }
}
