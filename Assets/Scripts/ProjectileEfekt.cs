using UnityEngine;

public class ProjectileEfekt : MonoBehaviour
{
    public int damage;
    public float selfDestructTime = 5f;

    [Header("AOE")]
    public GameObject aoePrefab;
    public float aoeRadius = 3f;
    public float aoeDuration = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, selfDestructTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CreateAOE(transform.position);
        Destroy(gameObject);
    }

    private void CreateAOE(Vector3 position)
    {
        GameObject aoe = Instantiate(aoePrefab, position, Quaternion.identity);

        // Zmieniamy tylko skalę wizualną (opcjonalnie)
        aoe.transform.localScale = Vector3.one * aoeRadius * 2f;

        // Tutaj faktycznie wykrywamy wrogów w zasięgu
        Collider[] hitColliders = Physics.OverlapSphere(position, aoeRadius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(aoe, aoeDuration);
    }
}
