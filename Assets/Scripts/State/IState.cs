using UnityEngine;

public interface IState
{
    void init();
    void update();
    void fixedUpdate();
    void exit();
    void collisionEnter(Collision other);
}
