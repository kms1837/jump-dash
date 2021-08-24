using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDashState :JumpingState
{
    public JumpDashState(PlayerController setCharacher) : base(setCharacher) {

    }

    public override void fixedUpdate() {
        characher.dash();
    }
}
