using UnityEngine;

public class IdleState :CharacterState
{

    public IdleState(PlayerController setCharacher): base(setCharacher) {
    }

    public override void init() {
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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        characher.move(v);
        characher.cameraRotation(h);
    }

    protected void jumpkeyInput() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            characher.jump();
            characher.setState(new JumpingState(characher));
        }
    }
}
