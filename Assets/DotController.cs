using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{

    [SerializeField] private Dot dot;
    private void OnEnable()
    {
        Destroy(gameObject, dot.visibleDuration) ;
    }


}
