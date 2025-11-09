using UnityEngine;

public class GrabbableGlow : MonoBehaviour
{
    [Header("Materials")]
    public Material normalMaterial;      // Material when not hovered
    public Material glowMaterial;        // Emission material for glow

    [Header("Settings")]
    public MeshRenderer targetRenderer;  // Renderer of the cube
    public float glowFadeSpeed = 5f;     // Speed of glow transition

    private bool isHandNear = false;
    private Material currentMaterial;

    void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<MeshRenderer>();

        currentMaterial = normalMaterial;
        targetRenderer.material = currentMaterial;
    }

    void Update()
    {
        // Smoothly transition between normal and glow
        Material targetMat = isHandNear ? glowMaterial : normalMaterial;
        if (currentMaterial != targetMat)
        {
            currentMaterial.Lerp(currentMaterial, targetMat, Time.deltaTime * glowFadeSpeed);
            targetRenderer.material = currentMaterial;
        }
    }

    // Call this when hand enters hover area (can be tied to DistanceHandGrab or NearHandHover events)
    public void OnHandHoverEnter()
    {
        isHandNear = true;
    }

    // Call this when hand exits hover area
    public void OnHandHoverExit()
    {
        isHandNear = false;
    }
}
