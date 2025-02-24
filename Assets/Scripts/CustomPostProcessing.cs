using UnityEngine;

public class CustomPostProcessing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Material mat;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
