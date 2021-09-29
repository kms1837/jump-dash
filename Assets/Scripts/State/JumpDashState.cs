using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDashState :JumpingState
{
    public JumpDashState() {

    }

    public override void fixedUpdate() {
        characher.dash();
    }
}
