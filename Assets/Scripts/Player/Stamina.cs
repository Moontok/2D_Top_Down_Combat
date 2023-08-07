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
    }

    public void RefreshStamina(int amount)
    {
        if (CurrentStamina < maxStamina)
            if (CurrentStamina + amount > maxStamina)
                CurrentStamina = maxStamina;
            else
                CurrentStamina += amount;
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
            if (i < CurrentStamina)
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            else
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
        }

        if (CurrentStamina < maxStamina)
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
    }
}
