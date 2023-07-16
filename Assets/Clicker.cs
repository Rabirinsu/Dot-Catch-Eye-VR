using UnityEngine;
public class Clicker : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] private LayerMask layermask;

    [Header("EVENTS ")] [SerializeField] private GameEvent clickedDot;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, layermask))
            {
                hit.transform.gameObject.SendMessage("Interacted", SendMessageOptions.DontRequireReceiver);
                clickedDot?.Raise();
            }
        }
    }
}