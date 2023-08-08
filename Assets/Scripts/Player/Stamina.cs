using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStaminaImage = null;
    [SerializeField] private Sprite emptyStaminaImage = null;
    [SerializeField] private float timeBetweenStaminaRefresh = 3f;

    private Transform staminaContainer = null;
    private int startingStamina = 3;
    private int maxStamina = 3;
    const string STAMINA_CONTAINER = "Stamina Container";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER).transform;
    }

    public void UseStamina(int amount)
    {
        CurrentStamina -= amount;
        UpdateStaminaImages();
        StopAllCoroutines();
        StartCoroutine(RefreshStaminaRoutine());
    }

    public void RefreshStamina(int amount)
    {
        if (CurrentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
            if (CurrentStamina + amount > maxStamina)
                CurrentStamina = maxStamina;
            else
                CurrentStamina += amount;
        UpdateStaminaImages();
    }

    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = startingStamina;
        UpdateStaminaImages();
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina(1);
        }
    }

    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            Transform child = staminaContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();

            if (i < CurrentStamina)
                image.sprite = fullStaminaImage;
            else
                image.sprite = emptyStaminaImage;
        }
    }
}
