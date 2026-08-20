using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShirtData", menuName = "Scriptable Objects/ShirtData")]
public class ShirtData : ScriptableObject
{
    [Header("Textures")]
    [SerializeField]
    private List<Texture> textures;

    [Header("Material")]
    [SerializeField]
    private Material material;


    [Header("Selección actual")]
    [SerializeField]
    private int currentIndex = 0;

    public void ApplyTexture(int index)
    {
        material.mainTexture = textures[index];
        currentIndex = index;
    }

    public void ApplyCurrentTexture()
    {
        ApplyTexture(currentIndex);
    }

    public List<Texture> GetTextures()
    {
        return textures;
    }

    public int TotalTextures() { 
        return textures.Count;
    }

    public int GetCurrentIndex() { 
        return currentIndex;
    }
}
