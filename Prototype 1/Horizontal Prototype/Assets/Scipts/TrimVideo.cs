using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TrimVideoCube : MonoBehaviour
{
    private Camera cam;
    private bool isTrimming = false;
    private Vector3 offset;
    private float initialXScale;

    public enum Edge { Right, Left } // choose which edge to trim
    public Edge trimEdge = Edge.Right;

    void Start()
    {
        cam = Camera.main;
        initialXScale = transform.localScale.x;
    }

    void OnMouseDown()
    {
        // Start trimming
        isTrimming = true;

        // Store offset between mouse and cube edge
        Vector3 mouseWorld = GetMouseWorldPos();
        if (trimEdge == Edge.Right)
            offset = transform.position + new Vector3(transform.localScale.x / 2f, 0, 0) - mouseWorld;
        else
            offset = transform.position - new Vector3(transform.localScale.x / 2f, 0, 0) - mouseWorld;
    }

    void OnMouseUp()
    {
        isTrimming = false;
    }

    void Update()
    {
        if (isTrimming)
        {
            Vector3 mouseWorld = GetMouseWorldPos() + offset;

            float newXScale = transform.localScale.x;

            if (trimEdge == Edge.Right)
            {
                // Compute new scale from left side fixed
                float leftX = transform.position.x - transform.localScale.x / 2f;
                newXScale = Mathf.Max(0.1f, mouseWorld.x - leftX);
                transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
                // Adjust cube center
                transform.position = new Vector3(leftX + newXScale / 2f, transform.position.y, transform.position.z);
            }
            else
            {
                // Trim left edge
                float rightX = transform.position.x + transform.localScale.x / 2f;
                newXScale = Mathf.Max(0.1f, rightX - mouseWorld.x);
                transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
                // Adjust cube center
                transform.position = new Vector3(rightX - newXScale / 2f, transform.position.y, transform.position.z);
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePos);
    }
}

// I used chatgpt to create this code
