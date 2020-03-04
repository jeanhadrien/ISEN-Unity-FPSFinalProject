using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator))] 
public class Playable : MonoBehaviour
{
    private Animator animator;
    private CharacterController _characterController;
    private float horizontalInput, verticalInput;

    private static readonly int IsMovingForward = Animator.StringToHash("isMovingForward");
    private static readonly int IsMovingBackwards = Animator.StringToHash("isMovingBackwards");
    private static readonly int IsMovingRight = Animator.StringToHash("isMovingRight");
    private static readonly int IsMovingLeft = Animator.StringToHash("isMovingLeft");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    

    private Vector3 _horizontalMovement, _verticalMovement;

    private Vector3 _moveDirection = Vector3.zero;
    public float runSpeed = 6.0f;
    public float walkSpeed = 5;
    public float currentSpeed = 0;
    private Transform _cameraTransform;
    public float jumpSpeed = 8.0f;
    public bool ikActive = true;
    public Transform weaponIkLeftHand = null;
    public Transform weaponIkRightHand = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        StartCoroutine(updateVelocity());
        _cameraTransform = GetComponentInChildren<Camera>().gameObject.transform;
        currentSpeed = walkSpeed;
    }

    
    void OnAnimatorIK()
    {
        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {
                

                // Set the right hand target position and rotation, if one has been assigned
                if(weaponIkRightHand != null && weaponIkLeftHand != null) {

                    //animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                    //animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                    //animator.SetIKPosition(AvatarIKGoal.RightHand,weaponIKRightHand.position);
                    //animator.SetIKRotation(AvatarIKGoal.RightHand,weaponIKRightHand.rotation);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,weaponIkLeftHand.position);
                    //animator.SetIKRotation(AvatarIKGoal.LeftHand,weaponIKLeftHand.rotation);
                }
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                animator.SetLookAtWeight(0);
            }
        }
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
            _verticalMovement = _cameraTransform.forward * Input.GetAxis("Vertical");
            _horizontalMovement = _cameraTransform.right * Input.GetAxis("Horizontal");
            _moveDirection = _verticalMovement + _horizontalMovement;
            _moveDirection *= currentSpeed;
            if (Input.GetButton("Jump")) _moveDirection.y = jumpSpeed;
        }
        _moveDirection.y -= 9.81f * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey("left shift"))
        {
            animator.SetBool(IsRunning,true);
            currentSpeed = walkSpeed;
        }
        else
        {
            currentSpeed = runSpeed;
            animator.SetBool(IsRunning,false);
        }



        if(verticalInput>0f)
        {
            animator.SetBool(IsMovingForward,true);
            animator.SetBool(IsMovingBackwards,false);
            ikActive = false;
        }
        else if (verticalInput == 0f)
        {
            animator.SetBool(IsMovingForward,false);
            animator.SetBool(IsMovingBackwards, false);
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

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
        {
            ikActive = true;
        }
        else
        {
            ikActive = false;
        }
    }
}
