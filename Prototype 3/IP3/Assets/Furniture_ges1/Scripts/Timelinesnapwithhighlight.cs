using UnityEngine;

public class TimelineSnapWithHighlight : MonoBehaviour
{
    [Header("Timeline Settings")]
    public Transform editingPanel; // Parent holding all timeline videos
    public float snapThreshold = 0.3f; // Distance to trigger snapping
    public Color snapHighlightColor = Color.green; // Glow when snapping
    public Color normalColor = Color.white;

    private bool isGrabbed = false;
    private Renderer videoRenderer;
    private Rigidbody rb;

    void Awake()
    {
        videoRenderer = GetComponentInChildren<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isGrabbed) return;

        Transform closestVideo = null;
        float closestDistance = Mathf.Infinity;
        bool snapLeft = false;

        // Find nearest video in X-axis within snapThreshold
        foreach (Transform video in editingPanel)
        {
            if (video == transform) continue;

            float distanceLeft = Mathf.Abs((video.position.x - video.localScale.x / 2) - (transform.position.x + transform.localScale.x / 2));
            float distanceRight = Mathf.Abs((video.position.x + video.localScale.x / 2) - (transform.position.x - transform.localScale.x / 2));

            if (distanceLeft < closestDistance && distanceLeft <= snapThreshold)
            {
                closestDistance = distanceLeft;
                closestVideo = video;
                snapLeft = true;
            }
            if (distanceRight < closestDistance && distanceRight <= snapThreshold)
            {
                closestDistance = distanceRight;
                closestVideo = video;
                snapLeft = false;
            }
        }

        // Highlight and snap if close
        if (closestVideo != null)
        {
            videoRenderer.material.color = snapHighlightColor;

            Vector3 newPos = transform.position;
            if (snapLeft)
            {
                newPos.x = closestVideo.position.x - closestVideo.localScale.x / 2 - transform.localScale.x / 2;
            }
            else
            {
                newPos.x = closestVideo.position.x + closestVideo.localScale.x / 2 + transform.localScale.x / 2;
            }

            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
        else
        {
            // Reset color if not snapping
            videoRenderer.material.color = normalColor;
        }
    }

    // Call from Distant Grab events
    public void OnGrab()
    {
        isGrabbed = true;
        rb.isKinematic = false;
    }

    public void OnRelease()
    {
        isGrabbed = false;
        rb.isKinematic = true;
        videoRenderer.material.color = normalColor; // Reset highlight
    }
}
