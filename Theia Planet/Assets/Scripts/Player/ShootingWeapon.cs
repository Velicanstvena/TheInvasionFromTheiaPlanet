using UnityEngine;
using Photon.Pun;

public class ShootingWeapon: MonoBehaviourPun
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 30f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private GameObject impactParticle;

    private float nextTimeToFire = 0f;

    private bool shooting = false;
    public delegate void ActionShoot();
    public static event ActionShoot onShoot;

    public bool Shooting
    {
        get
        {
            return shooting;
        }
        set
        {
            shooting = value;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                shooting = true;

            }
            else if (Input.GetButtonUp("Fire1"))
            {
                shooting = false;
            }
        }
    }

    private void Shoot()
    {
        //photonView.RPC("OnShootParticle", RpcTarget.AllBuffered);
        shootParticle.Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Health target = hit.transform.GetComponent<Health>();

            if (target != null)
            {
                //target.CallTakeDamageRPC(damage);
                target.TakeDamage(damage);
            }

            //PhotonNetwork.Instantiate(impactParticle.name, hit.point, Quaternion.LookRotation(hit.normal), 0);
            Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    [PunRPC]
    private void OnShootParticle()
    {
        shootParticle.Play();
    }
}
