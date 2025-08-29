using UnityEngine;

public class HoverEnlarge : MonoBehaviour
{
    private Vector3 originalScale;
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1.5f); // Size when hovered

    void Start()
    {
        // Save the original scale of the object
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        // When mouse hovers over the object, enlarge it
        transform.localScale = hoverScale;
    }

    void OnMouseExit()
    {
        // When mouse leaves, reset to original size
        transform.localScale = originalScale;
    }
}

// I used chatgpt to create this code
