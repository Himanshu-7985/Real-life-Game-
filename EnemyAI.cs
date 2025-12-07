using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float chaseRange = 20f;
    public float shootRange = 12f;
    public float shootInterval = 1.2f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int damage = 10;

    Transform player;
    Rigidbody rb;
    float lastShot = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Vector3 dir = (player.position - transform.position);
        float dist = dir.magnitude;

        if (dist < chaseRange)
        {
            if (dist > shootRange)
            {
                Vector3 move = dir.normalized * moveSpeed;
                rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
            }
            else
            {
                // shoot
                if (Time.time - lastShot > shootInterval)
                {
                    ShootAt(player.position);
                    lastShot = Time.time;
                }
            }

            // rotate
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);
        }
    }

    void ShootAt(Vector3 target)
    {
        if (projectilePrefab == null || firePoint == null) return;
        GameObject p = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbp = p.GetComponent<Rigidbody>();
        if (rbp != null) rbp.velocity = (target - firePoint.position).normalized * 25f;
        Projectile proj = p.GetComponent<Projectile>();
        if (proj != null) proj.damage = damage;
    }
}
