using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject Text;
    [Header("Setting")]
    [SerializeField] private float movementSpeed = 4f;

    [SerializeField] GameObject player;

    public Animator anim;


    private Vector2 previousMovementInput;

    public int facingDirection = 1;

    
    public NetworkVariable<float> horizontal = new(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> vertical = new(writePerm: NetworkVariableWritePermission.Owner);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsOwner) { return; }


    }
    private void FixedUpdate()
    {//
        if (!IsOwner) { return; }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal > 0 && transform.localScale.x > 0 || horizontal < 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        if (IsHost)
        {
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));
        }
        else
        { 
            HandleAnimationServerRpc(horizontal, vertical);
        }

        rb.velocity = new Vector2(horizontal, vertical) * movementSpeed;
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        Text.transform.localScale = new Vector3(Text.transform.localScale.x * -1, Text.transform.localScale.y, Text.transform.localScale.z);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }
        inputReader.MoveEvent += HandleMove;
    }
    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }
        inputReader.MoveEvent -= HandleMove;
    }
    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }
    [ServerRpc]
    private void HandleAnimationServerRpc(float horizontal, float vertical)
    {
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));
    }
}
