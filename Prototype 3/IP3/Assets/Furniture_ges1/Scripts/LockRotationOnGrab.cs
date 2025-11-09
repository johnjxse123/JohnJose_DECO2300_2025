using UnityEngine;

public class LockRotationOnGrab : MonoBehaviour
{
    private Quaternion lockedRotation;
    public bool isGrabbed = false; // You can set this from your grab logic

    void Start()
    {
        lockedRotation = transform.localRotation;
    }

    void Update()
    {
        if (isGrabbed)
        {
            transform.localRotation = lockedRotation;
        }
    }

    public void OnGrab()
    {
        isGrabbed = true;
        lockedRotation = transform.localRotation;
    }

    public void OnRelease()
    {
        isGrabbed = false;
    }
}
