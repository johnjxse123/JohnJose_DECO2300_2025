using UnityEngine;

public class SnapToPanelFixedJoint : MonoBehaviour
{
    public Rigidbody panelRigidbody; // Assign your editing panel Rigidbody
    public float snapDistance = 0.5f; // Distance at which snapping triggers

    private Rigidbody videoRb;
    private bool isGrabbed = false;

    void Awake()
    {
        videoRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, panelRigidbody.position);
            if (distance <= snapDistance)
            {
                SnapToPanel();
            }
        }
    }

    public void OnGrab()
    {
        isGrabbed = true;
        if (GetComponent<FixedJoint>())
            Destroy(GetComponent<FixedJoint>());
    }

    public void OnRelease()
    {
        isGrabbed = false;
    }

    private void SnapToPanel()
    {
        // Align position exactly to panel
        transform.position = panelRigidbody.position + Vector3.up * 0.1f; // Slight offset above panel

        // Add fixed joint
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = panelRigidbody;

        // Make sure the video doesn’t move while attached
        videoRb.velocity = Vector3.zero;
        videoRb.angularVelocity = Vector3.zero;

        isGrabbed = false;
    }
}
