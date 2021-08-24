using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 distance;

    private void Start() {
    }

    void LateUpdate() {
        this.transform.position = new Vector3(target.position.x, 0, target.position.z) + distance;
    }
}
