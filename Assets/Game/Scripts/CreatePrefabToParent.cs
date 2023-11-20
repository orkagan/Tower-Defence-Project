using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrefabToParent : MonoBehaviour
{
    [Tooltip("The prefab to be instantiated into the parent.")]
    public GameObject prefabGameObject;
    [Tooltip("The parent GameObject for the prefab to be instantiated into.")]
    public GameObject prefabParent;

    public void PrefabIntoParent()
    {
        Instantiate(prefabGameObject, prefabParent.transform);
    }
}
