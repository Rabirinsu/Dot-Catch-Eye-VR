using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{

    [SerializeField] private Dot dot;
    private void OnEnable()
    {
        if(GameManager.instance.sesionCount == 1)
        gameObject.layer = LayerMask.NameToLayer($"RightEye");
      else
         gameObject.layer = LayerMask.NameToLayer($"LeftEye");
        Debug.Log("gameManager.instance.sesionCount " + GameManager.instance.sesionCount);
        Destroy(gameObject, dot.visibleDuration) ;
    }


}
