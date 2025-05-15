using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AnimatedPerlinNoise : MonoBehaviour
{
    [Header("Noise Settings")]
    public int textureWidth = 256;
    public int textureHeight = 256;
    public float scale = 20f;

    [Header("Animation Settings")]
    public float speedX = 1f;
    public float speedY = 1f;

    private Renderer rend;
    private Texture2D noiseTexture;
    private float offsetX = 0f;
    private float offsetY = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        noiseTexture = new Texture2D(textureWidth, textureHeight);
        noiseTexture.filterMode = FilterMode.Point;
        rend.material.mainTexture = noiseTexture;
    }

    void Update()
    {
        offsetX += Time.deltaTime * speedX;
        offsetY += Time.deltaTime * speedY;

        GenerateNoiseTexture(offsetX, offsetY);
    }

    void GenerateNoiseTexture(float offsetX, float offsetY)
    {
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                float sampleX = (x / (float)textureWidth) * scale + offsetX;
                float sampleY = (y / (float)textureHeight) * scale + offsetY;
                float value = Mathf.PerlinNoise(sampleX, sampleY);
                Color color = new Color(value, value, value); // grayscale
                noiseTexture.SetPixel(x, y, color);
            }
        }

        noiseTexture.Apply();
    }
}
