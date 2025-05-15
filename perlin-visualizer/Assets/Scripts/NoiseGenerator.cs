using UnityEngine;

public class NoiseVisualizer : MonoBehaviour
{
    [Header("Texture Settings")]
    public int width = 256;
    public int height = 256;

    [Header("Noise Parameters")]
    [Range(1f, 100f)] public float scale = 20f;
    [Range(1, 10)] public int octaves = 4;
    [Range(0f, 1f)] public float persistence = 0.5f;
    [Range(1f, 5f)] public float lacunarity = 2f;
    public Vector2 offset;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        GenerateAndApplyTexture();
    }

    void OnValidate()
    {
        if (Application.isPlaying && rend != null)
            GenerateAndApplyTexture();
    }

    void GenerateAndApplyTexture()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, scale, octaves, persistence, lacunarity, offset);
        Texture2D texture = GenerateTexture(noiseMap);
        rend.sharedMaterial.mainTexture = texture;
    }

    Texture2D GenerateTexture(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, value);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
}
