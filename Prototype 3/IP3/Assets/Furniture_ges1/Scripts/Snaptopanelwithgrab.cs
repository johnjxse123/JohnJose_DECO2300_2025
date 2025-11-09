using UnityEngine;

public class SnapToPanelWithoutSpecificGrab : MonoBehaviour
{
    public Transform targetPanel;         // Drag your editing panel here
    public Vector3 snapOffset = Vector3.zero;
    public float snapDistance = 0.5f;

    private bool isGrabbed = false;       // track if user is holding
    private bool isSnapped = false;

    void Update()
    {
        // Only check snapping when grabbed
        if (isGrabbed && !isSnapped)
        {
            float distance = Vector3.Distance(transform.position, targetPanel.position);
            if (distance <= snapDistance)
            {
                Snap();
            }
        }
        else if (!isGrabbed && isSnapped)
        {
            ResetSnap();
        }
    }

    public void GrabStarted()
    {
        isGrabbed = true;
    }

    public void GrabEnded()
    {
        isGrabbed = false;
    }

    void Snap()
    {
        transform.position = targetPanel.position + snapOffset;
        transform.rotation = targetPanel.rotation;
        isSnapped = true;
    }

    void ResetSnap()
    {
        isSnapped = false;
    }
}
