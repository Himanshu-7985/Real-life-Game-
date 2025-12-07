using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;
    public float lifeTime = 5f;
    public LayerMask hitMask;
    public GameObject hitVFX;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision col)
    {
        var h = col.collider.GetComponent<Health>();
        if (h != null)
        {
            h.ModifyHealth(-damage);
        }

        if (hitVFX != null)
            Instantiate(hitVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
