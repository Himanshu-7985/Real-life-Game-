using UnityEngine;
using System.Collections;

public class SkillSystem : MonoBehaviour
{
    public float dashDistance = 6f;
    public float dashCooldown = 5f;
    public float shieldDuration = 4f;
    public float shieldCooldown = 10f;

    float lastDash = -999f;
    float lastShield = -999f;
    Health health;
    bool shieldActive = false;

    void Start()
    {
        health = GetComponent<Health>();
    }

    public bool CanDash() => Time.time - lastDash >= dashCooldown;
    public bool CanShield() => Time.time - lastShield >= shieldCooldown;

    public void UseDash()
    {
        if (!CanDash()) return;
        lastDash = Time.time;
        StartCoroutine(DoDash());
    }

    IEnumerator DoDash()
    {
        Vector3 forward = transform.forward;
        CharacterController cc = GetComponent<CharacterController>();
        // if using rigidbody, apply velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.AddForce(forward * 12f, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.2f);
    }

    public void UseShield()
    {
        if (!CanShield()) return;
        lastShield = Time.time;
        StartCoroutine(DoShield());
    }

    IEnumerator DoShield()
    {
        shieldActive = true;
        // conceptually reduce damage
        if (health != null) health.maxHealth += 50; // temporary buff
        yield return new WaitForSeconds(shieldDuration);
        if (health != null) health.maxHealth -= 50;
        shieldActive = false;
    }
}
