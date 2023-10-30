using UnityEngine;

public class CreateButtonGrid : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    public int buttonCount = 12;

    private void Start()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            GameObject btn = Instantiate(_buttonPrefab, transform);
            btn.name = $"Tile {i + 1}";
        }
    }
}