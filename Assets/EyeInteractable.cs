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


    [SerializeField] private BoxCollider _collider;
    [SerializeField] private GameEvent clickedDot;
    void Update()
    {
       if(IsHovered)
        {
            meshRenderer.material = OnHoverActiveMaterial;
            OnObjectHover?.Invoke(gameObject);
              Interacted();
            clickedDot?.Raise();
        }
        else
        {
            meshRenderer.material = OnHoverInactiveMaterial;

        }
    }

    public void Interacted()
    {
        meshRenderer.material = OnHoverActiveMaterial;
        this.enabled = false;
        _collider.enabled = false;
        OnObjectHover?.Invoke(gameObject); 
        Debug.Log("Interacted");
    }
}
