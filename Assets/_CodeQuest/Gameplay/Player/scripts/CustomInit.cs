using UnityEngine;

public class CustomInit : MonoBehaviour
{
    [SerializeField]
    private ShirtData customShirt;

    private void Start()
    {
        customShirt.ApplyCurrentTexture();
    }
}
