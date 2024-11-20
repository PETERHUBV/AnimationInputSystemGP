using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private PlayerActions playerMovement;
    public float moveSpeed, walkspeed, runspeed;
    public Vector2 moveInput;
    public bool isRunning;

    // Start is called before the first frame update
    private void OnEnable()
    {
        playerMovement = new PlayerActions();
        playerMovement.ActionMap.KeyboardAction.performed += OnMove;
        playerMovement.ActionMap.KeyboardAction.canceled += OnMove;
        playerMovement.ActionMap.Sprint.performed += Sprint_performed;
        playerMovement.ActionMap.Sprint.canceled += Sprint_canceled;
        playerMovement.Enable();
    }

    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
       
        
            isRunning = false;
        
    }

    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            isRunning = true;
        }
    }

        private void OnDisable()
    {
        playerMovement.Disable();
    }
    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveSpeed = isRunning ? runspeed : walkspeed;
        animator.SetFloat("Speed", moveInput.magnitude <= 0 ? 0f:(isRunning? 0.5f : 0.2f));
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        moveDir = transform.TransformDirection(moveDir) * moveSpeed;
        characterController.Move(moveDir * Time.deltaTime);
    }


}


