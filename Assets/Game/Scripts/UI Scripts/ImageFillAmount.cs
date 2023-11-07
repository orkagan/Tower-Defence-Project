using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageFillAmount : MonoBehaviour
{
    public Image imageGameObject;
    [Range(0,100)] public float imageFillAmount;

    private void OnEnable()
    {
        imageGameObject.type = Image.Type.Filled;
    }

    private void Update()
    {
        imageGameObject.fillAmount = imageFillAmount / 100;
    }
}
