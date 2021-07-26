using UnityEngine;
using Photon.Pun;

public class AnimationController : MonoBehaviourPun
{
    [SerializeField] private Animator animator;
    [SerializeField] private ShootingWeapon weapon;

    [SerializeField] private string runAnimationName = "Run";
    [SerializeField] private string runLeftAnimationName = "RunLeft";
    [SerializeField] private string runRightAnimationName = "RunRight";
    [SerializeField] private string runBackAnimationName = "RunBack";

    [SerializeField] private string attackAnimationName = "Attack";
    [SerializeField] private string attackAndRunAnimationName = "RunAttack";
    [SerializeField] private string attackAndRunLeftAnimationName = "RunLeftAttack";
    [SerializeField] private string attackAndRunRightAnimationName = "RunRightAttack";
    [SerializeField] private string attackAndRunBackAnimationName = "RunBackAttack";

    [SerializeField] private string takeDamageAnimationName = "Damaged";

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButton("Fire1"))
            {
                animator.SetBool(runAnimationName, false);
                animator.SetBool(attackAnimationName, true);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                animator.SetBool(attackAnimationName, false);
                animator.SetBool(attackAndRunAnimationName, false);
                animator.SetBool(attackAndRunLeftAnimationName, false);
                animator.SetBool(attackAndRunRightAnimationName, false);
                animator.SetBool(attackAndRunBackAnimationName, false);
            }

            if (weapon.Shooting)
            {
                CheckForMovementWhileShooting();
                return;
            }

            CheckForMovement();
        }
    }

    private void CheckForMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool(runAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool(runAnimationName, false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool(runAnimationName, false);
            animator.SetBool(runLeftAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool(runLeftAnimationName, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool(runAnimationName, false);
            animator.SetBool(runRightAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.D))
        {
            animator.SetBool(runRightAnimationName, false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool(runAnimationName, false);
            animator.SetBool(runBackAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.S))
        {
            animator.SetBool(runBackAnimationName, false);
        }
    }

    private void CheckForMovementWhileShooting()
    {
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool(runAnimationName, false);
            animator.SetBool(attackAnimationName, true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool(attackAnimationName, false);
            animator.SetBool(attackAndRunAnimationName, false);
            animator.SetBool(attackAndRunLeftAnimationName, false);
            animator.SetBool(attackAndRunRightAnimationName, false);
            animator.SetBool(attackAndRunBackAnimationName, false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log(attackAndRunAnimationName);
            animator.SetBool(attackAnimationName, false);
            animator.SetBool(runAnimationName, false);
            animator.SetBool(attackAndRunAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool(attackAndRunAnimationName, false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log(attackAndRunLeftAnimationName);
            animator.SetBool(attackAnimationName, false);
            animator.SetBool(runLeftAnimationName, false);
            animator.SetBool(attackAndRunLeftAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool(attackAndRunLeftAnimationName, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log(attackAndRunRightAnimationName);
            animator.SetBool(attackAnimationName, false);
            animator.SetBool(runRightAnimationName, false);
            animator.SetBool(attackAndRunRightAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.D))
        {
            animator.SetBool(attackAndRunRightAnimationName, false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log(attackAndRunBackAnimationName);
            animator.SetBool(attackAnimationName, false);
            animator.SetBool(runBackAnimationName, false);
            animator.SetBool(attackAndRunBackAnimationName, true);
        }
        else if (!Input.GetKey(KeyCode.S))
        {
            animator.SetBool(attackAndRunBackAnimationName, false);
        }
    }

    public void PlayDamageAnimation()
    {
        animator.SetTrigger(takeDamageAnimationName);
    }
}
