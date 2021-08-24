using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldState :IdleState
{
    public HoldState(PlayerController setCharacher) : base(setCharacher) {

    }

    public override void update() {
        float h = Input.GetAxis("Horizontal");

        characher.cameraRotation(h);
        jumpkeyInput();
    }

    public override void exit() {
        characher.unHold();
    }
}
