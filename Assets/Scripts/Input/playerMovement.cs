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

    [Header("Setting")]
    [SerializeField] private float movementSpeed = 4f;

    [SerializeField] GameObject player;

    public Animator anim;

    private Vector2 previousMovementInput;
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
    {
        if (!IsOwner) { return; }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
     // anim.SetFloat("horizontal",Mathf.Abs(horizontal));
    //  anim.SetFloat("vertical", Mathf.Abs(vertical));
        rb.velocity = new Vector2(horizontal, vertical) * movementSpeed;
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
    
}
