using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextureCenter : MonoBehaviour
{
    [SerializeField]
    private ShirtData customShirt;

    private int last;
    private int index;

    private void Awake()
    {
        last = customShirt.TotalTextures() - 1;
        index = customShirt.GetCurrentIndex();
    }

    public void Previous() {
        index--;
        if (!isValid(index))
        {
            Debug.Log("Primera textura");
            index = 0;
        }
        customShirt.ApplyTexture(index);
    }

    public void Next()
    {
        index++;
        if (!isValid(index))
        {
            Debug.Log("Ultima textura");
            index = last;
        }
        customShirt.ApplyTexture(index);
    }

    private bool isValid(int i) {
        return i >= 0 && i <= last; ;
    }
}
