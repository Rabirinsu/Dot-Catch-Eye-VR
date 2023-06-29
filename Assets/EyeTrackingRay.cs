using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EyeTrackingRay : MonoBehaviour
{

    [SerializeField]
    private float rayDistance;
    [SerializeField]
    private float rayWidth;
    [SerializeField]
    private LayerMask layerMask;  
    [SerializeField]
    private Color rayColorDefault;
    [SerializeField]
    private Color rayColorHover; 
    [SerializeField]
    private LineRenderer lineRenderer;

    private List<EyeInteractable> eyeInteractables = new List<EyeInteractable>();
    
    

  private  void Start()
    {
        SetupRay();
    }

    private void SetupRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.startColor = rayColorDefault;
        lineRenderer.endColor = rayColorDefault;
        lineRenderer.SetPosition(0, transform.position);
        var targetoffset = transform.position.z + rayDistance;
        var targetPos = new Vector3(transform.position.x, transform.position.y, targetoffset);
        lineRenderer.SetPosition(1, targetPos);
    }
    private void FixedUpdate()
    {
        var raycastDirection = transform.TransformDirection(Vector3.forward) * rayDistance;

        if(Physics.Raycast(transform.position, raycastDirection, out var hit, Mathf.Infinity, layerMask))
        {
            UnSelect();
            lineRenderer.startColor = rayColorHover;
            lineRenderer.endColor = rayColorHover;
            var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
            eyeInteractables.Add(eyeInteractable);
            eyeInteractable.IsHovered = true;
        }
        else
        {
            lineRenderer.startColor = rayColorDefault;
            lineRenderer.endColor = rayColorDefault;
            UnSelect(true);
        }
    }
    private void UnSelect(bool clear = false)
    {
        foreach(var interactable in eyeInteractables)
        {
            interactable.IsHovered = false;
        }
        if(clear)
        {
            eyeInteractables.Clear();
        }
    }
}
