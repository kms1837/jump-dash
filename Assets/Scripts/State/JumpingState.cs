using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState :IdleState
{
    public JumpingState(PlayerController setCharacher) : base(setCharacher) {

    }

    public override void update() {
        moveInputkey();

        if (Input.GetKeyUp(KeyCode.Space)) {
            characher.dashStart();
            characher.setState(new JumpDashState(characher));
        }
    }

    public override void collisionEnter(Collision other) {
        holdCheck(other);
    }

    private void holdCheck(Collision other) {
        Vector3 hitPoint = other.GetContact(0).point;
        Bounds hitObjectSize = other.collider.bounds;
        float characterSize = characher.transform.localScale.y;
        float handSize = characterSize / 2;
        float colliderTop = hitObjectSize.max.y;

        float characterBottom = (characher.transform.position.y - characher.transform.localScale.y / 2);

        if (characterBottom < colliderTop) { // 바닥체크
            bool holding = other.transform.localScale.y > characterSize ? hitPoint.y > (colliderTop - handSize) : true;

            if (holding) {
                characher.hold(colliderTop);
                characher.setState(new HoldState(characher));
            }
            Debug.Log($"{hitPoint.y} {(colliderTop - handSize)}");
            //Debug.Log($"{other.transform.localScale.y} {isWallHold}");
        }
        //Debug.Log($"{hitPoint.y} {colliderTop} {characterBottom}");
        // 점프중 벽에 손이 닿을정도면 매달릴수 있음
        // issue: 기울린 벽에서 잡으면 문제가 생김
    }
}
