using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _healthBar, _resources, _phaseText;

    private void Start()
    {
        SetText("Build Phase");
        
        GameStateHandler.Instance.onBuildPhase.AddListener(() => SetText("Build Phase"));
        GameStateHandler.Instance.onAttackPhase.AddListener(() => SetText("Attack Phase"));
    }

    private void FixedUpdate()
    {
        UpdateResources();
        UpdateHealth();
    }

    private void SetText(string text)
    {
        _phaseText.GetComponent<Text>().text = text;
    }

    public int GetResourceCount
    {
        get => _player.currency;
        private set => _player.currency = value;
    }

    public void SetResourceCount(int value, bool decrement = false)
    {
        if (decrement)
            GetResourceCount -= value;
        else
            GetResourceCount += value;
    }

    private void UpdateResources()
    {
        Text money = _resources.GetComponent<Text>();
        money.text = GetResourceCount.ToString();
    }

    private void UpdateHealth()
    {
        Image healthBar = _healthBar.GetComponent<Image>();
        healthBar.fillAmount = _player.GetHealth;
    }
}
