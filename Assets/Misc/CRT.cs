using UnityEngine;
    
[ExecuteInEditMode]
public class CRT : MonoBehaviour
{
    public Material material;
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_MainTex", source);
        Graphics.Blit(source, destination, material);
    }
}
