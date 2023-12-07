using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _healthBar, _resources, _phaseText;

    public Player MakePlayer
    {
        set => _player = value;
    }

    private void FixedUpdate()
    {
        UpdateResources();
        UpdateHealth();
    }

    public int GetResourceCount
    {
        get => _player.Currency;
        private set => _player.Currency = value;
    }

    public void SetResourceCount(int value, bool decrement = true)
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
        healthBar.fillAmount = _player.GetHealth / 100;

        if (_player.GetHealth <= 0)
        {
            StartCoroutine(nameof(_player.Die));
        }
    }
}
