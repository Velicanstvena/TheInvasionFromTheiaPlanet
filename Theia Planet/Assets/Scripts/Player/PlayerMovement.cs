using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    #region Variables

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 12f;

    [SerializeField] private float gravity = -20f;
    [SerializeField] private Vector3 velocity;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded = false;

    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Camera camera;

    private Vector3 smoothMove;
    private Quaternion smoothRotation;

    public static GameObject LocalPlayerInstance;

    #endregion

    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerMovement.LocalPlayerInstance = this.gameObject;
        }
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            camera.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
        }
        else
        {
            SmoothMovement();
        }
    }

    private void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 20);
        transform.rotation = Quaternion.Lerp(transform.rotation, smoothRotation, Time.deltaTime * 20);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            smoothRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
