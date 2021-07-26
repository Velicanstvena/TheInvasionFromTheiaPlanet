using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviourPun
{
    [SerializeField] private float health = 500f;
    [SerializeField] public bool isPlayer = false;

    public delegate void ActionHealth();
    public static event ActionHealth onHealthChange;

    public float HealthValue
    {
        get
        {
            return health;
        }
    }

    public void CallTakeDamageRPC(float damage)
    {
        photonView.RPC("TakeDamage", RpcTarget.AllBuffered, damage);
    }

    [PunRPC]
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (isPlayer)
        {
            gameObject.GetComponentInChildren<AnimationController>().PlayDamageAnimation();
            if (onHealthChange != null)
            {
                onHealthChange();
            }
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
