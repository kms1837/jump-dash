using UnityEngine;

public interface IState
{
    void enter();
    void update();
    void fixedUpdate();
    void exit();
    void collisionEnter(Collision other);
}
