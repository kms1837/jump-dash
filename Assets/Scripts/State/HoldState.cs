using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldState :IdleState
{
    public HoldState() {

    }

    public override void update() {
        characher.cameraRotation();
        jumpkeyInput();
    }

    public override void exit() {
        characher.unHold();
    }
}
