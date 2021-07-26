using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<Health>().CallTakeDamageRPC(damage);
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
