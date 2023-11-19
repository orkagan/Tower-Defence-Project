using UnityEngine;

public class FindCamera : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
    
    private void Start()
    {
        Awake();
    }
}
