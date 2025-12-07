using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public Weapon weaponData;
    public Transform muzzlePoint;
    public Transform shootOrigin; // shoot direction pivot (aimPivot)
    public AudioSource audioSource;
    public int currentMagazine;
    public int currentReserve;
    float lastFire = 0f;

    void Start()
    {
        if (weaponData != null)
        {
            currentMagazine = weaponData.magazineSize;
            currentReserve = weaponData.reserveAmmo;
        }
    }

    public void TryFire()
    {
        if (weaponData == null) return;
        if (Time.time - lastFire < weaponData.fireRate) return;
        if (currentMagazine <= 0)
        {
            // optionally play empty sound
            return;
        }

        Fire();
    }

    void Fire()
    {
        lastFire = Time.time;
        currentMagazine--;

        if (weaponData.projectilePrefab != null && muzzlePoint != null)
        {
            GameObject p = Instantiate(weaponData.projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
            Rigidbody rb = p.GetComponent<Rigidbody>();
            if (rb != null) rb.velocity = shootOrigin.forward * weaponData.projectileSpeed;
            Projectile proj = p.GetComponent<Projectile>();
            if (proj != null) proj.damage = weaponData.damage;
        }

        if (audioSource != null && weaponData.shootSound != null)
            audioSource.PlayOneShot(weaponData.shootSound);
    }

    public void Reload()
    {
        if (currentReserve <= 0) return;
        int needed = weaponData.magazineSize - currentMagazine;
        int taken = Mathf.Min(needed, currentReserve);
        currentMagazine += taken;
        currentReserve -= taken;
    }
}
