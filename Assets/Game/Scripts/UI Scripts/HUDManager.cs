using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region Singleton
    public static HUDManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _healthBar, _resources, _phaseText;

    public int GetResourceCount
    {
        get => _player.currency;
        set => GetResourceCount = value;
    }

    public void SetResourceCount(int value, bool decrement = false)
    {
        if (decrement)
            GetResourceCount -= value;
        else
            GetResourceCount += value;
    }
}
