using UnityEngine;
using NaughtyAttributes;

public class S_Combat : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<S_Projectile>())
        {
            S_Health health = GetComponent<S_Health>();
            if (health != null)
            {
                health.TakeDamage(other.GetComponent<S_Projectile>().Damage);
            }
        }
    }
}
