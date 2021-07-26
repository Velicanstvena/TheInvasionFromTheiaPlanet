using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody rb;
    private Vector2 movement;
    public float moveSpeed = 5f;

    private Transform player;
    public bool playerClose = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerClose)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = Quaternion.Euler(transform.rotation.x, -angle, transform.rotation.z);
        }
    }

    void FixedUpdate()
    {
        if (playerClose) MoveCharacter();
    }

    void MoveCharacter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 40f)
        {
            Vector3 moveDir = (player.transform.position - transform.position).normalized;
            transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (playerClose)
            {
                animator.SetTrigger("sleep");
                playerClose = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!playerClose)
            {
                player = other.gameObject.transform;

                animator.SetTrigger("wakeup");
                animator.SetTrigger("attack");

                playerClose = true;
            }
        }
    }
}
