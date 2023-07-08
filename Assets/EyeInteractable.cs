using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{
    public bool IsHovered { get; set; }

    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    private Material OnHoverActiveMaterial; 
    
    [SerializeField]
    private Material OnHoverInactiveMaterial;

    [SerializeField]
    private MeshRenderer meshRenderer;



    void Update()
    {
      /* if(IsHovered)
        {
            meshRenderer.material = OnHoverActiveMaterial;
            OnObjectHover?.Invoke(gameObject);
        }
        else
        {
            meshRenderer.material = OnHoverInactiveMaterial;

        }*/
    }

    public void Interacted()
    {
        meshRenderer.material = OnHoverActiveMaterial;
        OnObjectHover?.Invoke(gameObject);
    }
}
