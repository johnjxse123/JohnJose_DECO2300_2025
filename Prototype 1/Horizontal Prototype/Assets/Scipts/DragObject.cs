using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DragObject : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;
    private float distanceToCamera;

    void Start()
    {
        cam = Camera.main; // grab main camera
    }

    void OnMouseDown()
    {
        // Calculate distance from camera to object
        distanceToCamera = Vector3.Distance(transform.position, cam.transform.position);

        // Convert mouse position to world space
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera));

        // Save offset so object doesn't snap to mouse
        offset = transform.position - mouseWorld;

        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // Convert mouse to world position and apply offset
            Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera));
            transform.position = mouseWorld + offset;
        }
    }
}

// I used chatgpt to create this code
