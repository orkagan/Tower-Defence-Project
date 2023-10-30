using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public void CreateObject()
    {
        Instantiate(_gameObject, transform);
    }
}
