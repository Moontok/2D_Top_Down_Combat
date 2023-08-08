using UnityEngine;

public class ActiveActionBar : Singleton<ActiveActionBar>
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.ActionBar.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());        
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void EquipStartingWeapon()
    {
        ToggleAcitveHightlight(0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleAcitveHightlight(numValue);
    }

    private void ToggleAcitveHightlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform actionSlot in this.transform)
        {
            actionSlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (PlayerHealth.Instance.IsDead) return;

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = this.transform.GetChild(activeSlotIndexNum);
        InventorySlot actionBarSlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = actionBarSlot.GetWeaponInfo();

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);
        // GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        // ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        // newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
