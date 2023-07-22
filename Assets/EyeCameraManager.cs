using UnityEngine;
using UnityEngine.XR;

public class EyeCameraManager : MonoBehaviour
{
    public Camera rightEyeCamera;

    void Start()
    {
        
       rightEyeCamera.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().allowXRRendering = false;
        XRSettings.gameViewRenderMode = GameViewRenderMode.RightEye;
        rightEyeCamera.stereoTargetEye = StereoTargetEyeMask.Right;
    }   

}