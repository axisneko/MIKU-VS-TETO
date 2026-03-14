using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] public float radius = 15.0f;
    [SerializeField] public float force = 500.0f;
    [SerializeField] public float delay = 0f;
    [SerializeField] public float delayTimer = 0f;
    [SerializeField] public bool explodeOnCollision = false;
    [SerializeField] public GameObject effectsPrefab;
    [SerializeField] public float effectsDisplayTime = 2.0f;
    public LayerMask explosionLayers;

    private void Awake()
    {
        delayTimer = 0f;
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= delay && !explodeOnCollision)
        {
            DoExplosion();
            Destroy(gameObject);
        }
    }

    private void DoExplosion() {
        HandleEffects();
        HandleDestruction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explodeOnCollision && enabled)
        {
            DoExplosion();
            Destroy(gameObject);
        }
    }

    private void HandleDestruction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, explosionLayers);

        foreach (Collider collider in colliders){
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null) {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }

            if (collider.tag == "hitbox_body")
            {
                float damage = (radius-Vector3.Distance(transform.position, collider.transform.position))*force/radius/25;
                if (collider.transform.parent.transform.parent.GetComponent<EnemyStandardMechanics>() != null)
                {
                    collider.transform.parent.transform.parent.GetComponent<EnemyStandardMechanics>().dealDamage(damage.ConvertTo<int>());
                }
                if (collider.transform.parent.transform.parent.GetComponent<PlayerHealth>() != null)
                {
                    collider.transform.parent.transform.parent.GetComponent<PlayerHealth>().dealDamage(damage.ConvertTo<int>());
                }
            }
        }
    }

    private void HandleEffects()
    {
        if (effectsPrefab != null)
        {
            var scale = radius / 5f;
            GameObject effect = Instantiate(effectsPrefab, transform.position, Quaternion.identity);
            effect.transform.localScale = new Vector3(scale, scale, scale);
            Destroy(effect, effectsDisplayTime);
        }
    }
}
