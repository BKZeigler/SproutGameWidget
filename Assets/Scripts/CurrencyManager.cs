using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField]
    private int currentCurrency = 0;

    private const string CurrencyKey = "Currency";

    public int CurrentCurrency => currentCurrency;

    /// <summary>
    /// Fires whenever currency changes.
    /// Parameter = new currency amount.
    /// </summary>
    public event Action<int> OnCurrencyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadCurrency();
    }


    /// <summary>
    /// Adds currency.
    /// </summary>
    public void AddCurrency(int amount)
    {
        if (amount <= 0)
            return;

        currentCurrency += amount;
        NotifyCurrencyChanged();
    }

    /// <summary>
    /// Removes currency if enough exists.
    /// Returns true if successful.
    /// </summary>
    public bool RemoveCurrency(int amount)
    {
        if (amount <= 0)
            return false;

        if (currentCurrency < amount)
            return false;

        currentCurrency -= amount;
        NotifyCurrencyChanged();

        return true;
    }

    /// <summary>
    /// Returns whether the player can afford a cost.
    /// </summary>
    public bool CanAfford(int amount)
    {
        return currentCurrency >= amount;
    }

    /// <summary>
    /// Sets currency directly.
    /// Useful for debugging, rewards, save systems, etc.
    /// </summary>
    public void SetCurrency(int amount)
    {
        currentCurrency = Mathf.Max(0, amount);
        NotifyCurrencyChanged();
    }

    /// <summary>
    /// Gets current currency amount.
    /// </summary>
    public int GetCurrency()
    {
        return currentCurrency;
    }

    private void NotifyCurrencyChanged()
    {
        OnCurrencyChanged?.Invoke(currentCurrency);
    }

    public void SaveCurrency()
    {
        PlayerPrefs.SetInt(CurrencyKey, currentCurrency);
        PlayerPrefs.Save();
    }

    public void LoadCurrency()
    {
        currentCurrency = PlayerPrefs.GetInt(CurrencyKey, 0);
        NotifyCurrencyChanged();
    }


}

