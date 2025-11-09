using UnityEngine;
using Oculus.Interaction;

public class VideoTrim : MonoBehaviour
{
    public Transform leftHandle;
    public Transform rightHandle;
    public Transform videoMesh;

    private Vector3 originalLeftPos;
    private Vector3 originalRightPos;
    private Vector3 originalScale;
    private Transform grabbedHandle = null;

    void Start()
    {
        originalScale = videoMesh.localScale;
        originalLeftPos = leftHandle.localPosition;
        originalRightPos = rightHandle.localPosition;
    }

    // Call this when grab starts
    public void HandleGrabStarted(Transform handle)
    {
        grabbedHandle = handle;
    }

    // Call this when grab ends
    public void HandleGrabEnded()
    {
        grabbedHandle = null;
    }

    void Update()
    {
        if (grabbedHandle != null)
        {
            Vector3 localPos = grabbedHandle.localPosition;

            if (grabbedHandle == leftHandle)
            {
                float newWidth = originalRightPos.x - localPos.x;
                if (newWidth > 0.1f)
                {
                    videoMesh.localScale = new Vector3(newWidth, originalScale.y, originalScale.z);
                    videoMesh.localPosition = new Vector3(localPos.x + newWidth / 2f, videoMesh.localPosition.y, videoMesh.localPosition.z);
                }
            }
            else if (grabbedHandle == rightHandle)
            {
                float newWidth = localPos.x - originalLeftPos.x;
                if (newWidth > 0.1f)
                {
                    videoMesh.localScale = new Vector3(newWidth, originalScale.y, originalScale.z);
                    videoMesh.localPosition = new Vector3(originalLeftPos.x + newWidth / 2f, videoMesh.localPosition.y, videoMesh.localPosition.z);
                }
            }
        }
    }
}
