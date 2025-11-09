using UnityEngine;
using Oculus.Interaction;

public class XRVideoTrim : MonoBehaviour
{
    public Transform leftHandle;
    public Transform rightHandle;

    private bool trimmingLeft = false;
    private bool trimmingRight = false;
    private Transform activeHandle;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private float initialDistance;

    public void StartTrimLeft()
    {
        trimmingLeft = true;
        activeHandle = leftHandle;
        StoreInitialValues();
    }

    public void StartTrimRight()
    {
        trimmingRight = true;
        activeHandle = rightHandle;
        StoreInitialValues();
    }

    public void EndTrim()
    {
        trimmingLeft = false;
        trimmingRight = false;
        activeHandle = null;
    }

    void StoreInitialValues()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
        initialDistance = Vector3.Distance(leftHandle.position, rightHandle.position);
    }

    void Update()
    {
        if (activeHandle == null) return;

        float currentDistance = Vector3.Distance(leftHandle.position, rightHandle.position);
        float scaleRatio = currentDistance / initialDistance;

        transform.localScale = new Vector3(initialScale.x * scaleRatio, initialScale.y, initialScale.z);

        // Recenter so the opposite side stays fixed
        if (trimmingLeft)
            transform.position = new Vector3(rightHandle.position.x - transform.localScale.x / 2f, transform.position.y, transform.position.z);
        else if (trimmingRight)
            transform.position = new Vector3(leftHandle.position.x + transform.localScale.x / 2f, transform.position.y, transform.position.z);
    }
}
