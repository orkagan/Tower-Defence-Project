using UnityEngine;

public class FindCamera : MonoBehaviour
{
    [SerializeField] bool _faceCamera = false;

    Canvas canvas => GetComponent<Canvas>();
    Camera cam => Camera.main;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Start()
    {
        Awake();
    }

    private void Update()
    {
        if (_faceCamera)
            canvas.transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
    }
}
