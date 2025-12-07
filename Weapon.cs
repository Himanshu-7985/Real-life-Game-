using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName = "Rifle";
    public GameObject projectilePrefab;
    public int magazineSize = 30;
    public int reserveAmmo = 90;
    public float fireRate = 0.1f;
    public float projectileSpeed = 45f;
    public int damage = 20;
    public float recoil = 1f;
    public AudioClip shootSound;
    public Sprite icon;
}
