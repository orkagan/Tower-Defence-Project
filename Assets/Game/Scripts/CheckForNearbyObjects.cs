using System;
using UnityEngine;
using UnityEngine.Events;

public class CheckForNearbyObjects : MonoBehaviour
{
    [SerializeField] private string tagName;
    [SerializeField] private bool debugEnter, destroyThis, destroyOther, invokeEvent;
    
    public UnityEvent collisionEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagName)) return;
        
        if (debugEnter)
        {
            Debug.Log($"{other.tag} GameObject entered collision.");
        }

        if (invokeEvent)
        {
            collisionEnter.Invoke();
        }

        if (destroyOther)
        {
            Destroy(other.gameObject);
        }

        if (destroyThis)
        {
            Destroy(gameObject);
        }
    }
}