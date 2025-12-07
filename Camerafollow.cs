using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.2f, -3.5f);
    public float smooth = 8f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);

        // Optional: look at player head
        transform.LookAt(target.position + Vector3.up * 1.4f);
    }
}
