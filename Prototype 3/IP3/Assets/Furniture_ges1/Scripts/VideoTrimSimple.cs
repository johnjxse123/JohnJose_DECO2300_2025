using UnityEngine;

public class VideoTrimSimple : MonoBehaviour
{
    public Transform leftHandle;
    public Transform rightHandle;

    private bool isTrimmingLeft = false;
    private bool isTrimmingRight = false;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Left handle grab simulation
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == leftHandle)
                    isTrimmingLeft = true;
                else if (hit.transform == rightHandle)
                    isTrimmingRight = true;
            }
        }

        // Release mouse
        if (Input.GetMouseButtonUp(0))
        {
            isTrimmingLeft = false;
            isTrimmingRight = false;
        }

        // Trim logic
        if (isTrimmingLeft)
            TrimLeft();
        else if (isTrimmingRight)
            TrimRight();
    }

    void TrimLeft()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            float rightX = rightHandle.position.x;
            float newWidth = Mathf.Max(0.1f, rightX - hitPoint.x);
            transform.localScale = new Vector3(newWidth, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(rightX - newWidth / 2f, transform.position.y, transform.position.z);
            leftHandle.position = new Vector3(transform.position.x - newWidth / 2f, transform.position.y, transform.position.z);
        }
    }

    void TrimRight()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            float leftX = leftHandle.position.x;
            float newWidth = Mathf.Max(0.1f, hitPoint.x - leftX);
            transform.localScale = new Vector3(newWidth, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(leftX + newWidth / 2f, transform.position.y, transform.position.z);
            rightHandle.position = new Vector3(transform.position.x + newWidth / 2f, transform.position.y, transform.position.z);
        }
    }
}
