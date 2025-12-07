using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;
    public Button fireButton;
    public Button reloadButton;
    public Button dashButton;
    public Button shieldButton;
    public ThirdPersonController playerController;
    public WeaponController weaponController;
    public SkillSystem skillSystem;

    void Start()
    {
        if (playerController == null) playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        if (weaponController == null && playerController != null) weaponController = playerController.weaponController;
        if (skillSystem == null && playerController != null) skillSystem = playerController.skillSystem;

        fireButton.onClick.AddListener(() => weaponController.TryFire());
        reloadButton.onClick.AddListener(() => weaponController.Reload());
        dashButton.onClick.AddListener(() => skillSystem.UseDash());
        shieldButton.onClick.AddListener(() => skillSystem.UseShield());
    }

    void Update()
    {
        if (playerController != null)
        {
            var h = playerController.GetComponent<Health>();
            if (h != null) healthText.text = $"HP: {h.currentHealth}/{h.maxHealth}";
        }

        if (weaponController != null && weaponController.weaponData != null)
        {
            ammoText.text = $"Ammo: {weaponController.currentMagazine}/{weaponController.currentReserve}";
        }
    }
}
