using UnityEngine;



[RequireComponent(typeof(Camera))]
public class EyeCamera : MonoBehaviour
{
    public Material eyeMaterial;



    private Camera cam;



    void Awake()
    {
        cam = GetComponent<Camera>();
    }

   private void OnPreCull()
    {
        if (cam.stereoTargetEye == StereoTargetEyeMask.Left)
        {
            cam.targetTexture = null;
            cam.Render();
            cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            Graphics.Blit(null, cam.targetTexture, eyeMaterial);
            cam.Render();
        }
    }
}