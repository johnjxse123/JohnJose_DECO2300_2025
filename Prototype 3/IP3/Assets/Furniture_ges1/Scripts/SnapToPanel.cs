using UnityEngine;

public class SnapToPanel : MonoBehaviour
{
    public Transform targetPanel; // assign your editing panel in inspector
    public float snapDistance = 5f; // distance at which video will snap
    private bool isGrabbed = false;

    public void Grabbed()
    {
        isGrabbed = true;
    }

    public void Released()
    {
        isGrabbed = false;
        TrySnap();
    }

    void TrySnap()
    {
        if (Vector3.Distance(transform.position, targetPanel.position) <= snapDistance)
        {
            transform.position = targetPanel.position;
            transform.rotation = targetPanel.rotation;
        }
    }
}
