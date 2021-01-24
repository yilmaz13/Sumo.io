using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    // The target we are following
    public Transform target;

    void LateUpdate()
    {
        if (!target) return;

        transform.position = target.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + 14, transform.position.z - 11);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}