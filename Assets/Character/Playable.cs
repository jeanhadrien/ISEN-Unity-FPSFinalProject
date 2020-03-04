using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Playable : MonoBehaviour
{
    private Animator animator;

    private float horizontalInput, verticalInput;

    private static readonly int IsMovingForward = Animator.StringToHash("isMovingForward");
    private static readonly int IsMovingBackwards = Animator.StringToHash("isMovingBackwards");
    private static readonly int IsMovingRight = Animator.StringToHash("isMovingRight");
    private static readonly int IsMovingLeft = Animator.StringToHash("isMovingLeft");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    
    private CharacterController _characterController;
    private Vector3 _horizontalMovement, _verticalMovement;

    private Vector3 _moveDirection = Vector3.zero;
    public float speed = 6.0f;

    public Transform cameraTransform;
    public float jumpSpeed = 8.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        StartCoroutine(updateVelocity());
        cameraTransform = GetComponentInChildren<Camera>().gameObject.transform;
    }

    IEnumerator updateVelocity()
    {
        while (true)
        {
            Vector3 oldPosition = transform.position;
            yield return new WaitForSeconds(0.1f);
            Vector3 diffPosition = transform.position - oldPosition;
            MyUIManager.SetTextCenter(diffPosition.ToString());
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_characterController.isGrounded)
        {
            _verticalMovement = cameraTransform.forward * Input.GetAxis("Vertical");
            _horizontalMovement = cameraTransform.right * Input.GetAxis("Horizontal");
            _moveDirection = _verticalMovement + _horizontalMovement;
            _moveDirection *= speed;
            if (Input.GetButton("Jump")) _moveDirection.y = jumpSpeed;
        }
        _moveDirection.y -= 9.81f * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey("left shift"))
        {
            animator.SetBool(IsRunning,true);
        }
        else
        {
            animator.SetBool(IsRunning,false);
        }
        
        if(verticalInput>0f)
        {
            animator.SetBool(IsMovingForward,true);
            animator.SetBool(IsMovingBackwards,false);
        }
        else if (verticalInput == 0f)
        {
            animator.SetBool(IsMovingForward,false);
            animator.SetBool(IsMovingBackwards,false);
        }
        else
        {
            animator.SetBool(IsMovingForward,false);
            animator.SetBool(IsMovingBackwards,true);
        }
        
        if(horizontalInput>0f)
        {
            animator.SetBool(IsMovingRight,true);
            animator.SetBool(IsMovingLeft,false);
        }
        else if (horizontalInput == 0f)
        {
            animator.SetBool(IsMovingRight,false);
            animator.SetBool(IsMovingLeft,false);
        }
        else
        {
            animator.SetBool(IsMovingRight,false);
            animator.SetBool(IsMovingLeft,true);
        }
    }
}
