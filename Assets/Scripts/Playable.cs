using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class Playable : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;
    private float _horizontalInput, _verticalInput;
    
    private static readonly int IsMovingForward = Animator.StringToHash("isMovingForward");
    private static readonly int IsMovingBackwards = Animator.StringToHash("isMovingBackwards");
    private static readonly int IsMovingRight = Animator.StringToHash("isMovingRight");
    private static readonly int IsMovingLeft = Animator.StringToHash("isMovingLeft");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    
    private float currentSpeed;
    private Vector3 _horizontalMovement, _verticalMovement;
    private Transform _cameraTransform;
    private Vector3 _moveDirection = Vector3.zero;
    private FireManager _fire;

    
    public float runSpeed = 6.0f;
    public float walkSpeed = 5f;
    public float jumpForce = 8.0f;
    
    public Transform weaponIkLeftHand;
    public Transform weaponIkRightHand;
    
    [NonSerialized] public bool isAiming;
    [NonSerialized] public Vector3 GunTargetPosition;
    [NonSerialized] public RaycastHit GunTargetRaycastHit;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        StartCoroutine(UpdateVelocity());
        _cameraTransform = Camera.main.transform;
        currentSpeed = walkSpeed;
        _fire = GetComponent<FireManager>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        MovePlayer();
        UpdateSpeed();
        UpdateAnimationStates();
    }


    /// <summary>
    /// Inverse Kinematics linking hands and weapon
    /// </summary>
    /// <param name="layerIndex"></param>
    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator)
        {
            if (weaponIkRightHand != null && weaponIkLeftHand != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.RightHand,
                    weaponIkRightHand.position + weaponIkRightHand.transform.TransformDirection(Vector3.right) * 0.09f);
                _animator.SetIKRotation(AvatarIKGoal.RightHand,
                    weaponIkRightHand.rotation * Quaternion.AngleAxis(-90, Vector3.up) *
                    Quaternion.AngleAxis(-90, Vector3.forward));
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, weaponIkLeftHand.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftHand,
                    weaponIkLeftHand.rotation * Quaternion.AngleAxis(180, Vector3.forward));
            }
        }
    }
    
    /// <summary>
    /// Computes and print player speed
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateVelocity()
    {
        while (true)
        {
            var oldPosition = transform.position;
            yield return new WaitForSeconds(0.1f);
            var diffPosition = transform.position - oldPosition;
            var speed = Math.Abs(diffPosition.x) + Math.Abs(diffPosition.y) + Math.Abs(diffPosition.z);
            MyUiManager.SetTextUpL(speed.ToString());
        }
    }

    
    /// <summary>
    /// Move player in world
    /// </summary>
    private void MovePlayer()
    {
        // get movement direction from input
        _verticalMovement = _cameraTransform.forward * _verticalInput;
        _verticalMovement.y = 0;
        _horizontalMovement = _cameraTransform.right * _horizontalInput;
        _horizontalMovement.y = 0;
        _moveDirection = _verticalMovement + _horizontalMovement;
        _moveDirection *= currentSpeed;
        
        // if running forward, add more speed to match animation
        if (_verticalInput > 0f && !Input.GetKey("left shift")) { _verticalMovement += _cameraTransform.forward * 0.3f; }
        
        // jump only if grounded
        if (_characterController.isGrounded)
        {
            // jump is buggy as well
            if (Input.GetButton("Jump")) _moveDirection.y = jumpForce;
        }

        // using rigidbody is buggy, so we use our own gravity
        _moveDirection.y -= 9.81f * Time.fixedDeltaTime;

        // we finally update direction
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Update current speed and animation states, based on shift key
    /// </summary>
    private void UpdateSpeed()
    {
        if (Input.GetKey("left shift"))
        {
            _animator.SetBool(IsRunning, true);
            currentSpeed = walkSpeed;
        }
        else
        {
            currentSpeed = runSpeed;
            _animator.SetBool(IsRunning, false);
        }
    }

    /// <summary>
    /// Update animation states based on input 
    /// </summary>
    void UpdateAnimationStates()
    {
        
        // Player is describe as aiming only if in IDLE animation state
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE")) { isAiming = true; }
        else { isAiming = false; }
        
        if (_verticalInput > 0f)
        {
            _animator.SetBool(IsMovingForward, true);
            _animator.SetBool(IsMovingBackwards, false);
        }
        else if (_verticalInput == 0f)
        {
            _animator.SetBool(IsMovingForward, false);
            _animator.SetBool(IsMovingBackwards, false);
        }
        else
        {
            _animator.SetBool(IsMovingForward, false);
            _animator.SetBool(IsMovingBackwards, true);
        }

        if (_horizontalInput > 0f)
        {
            _animator.SetBool(IsMovingRight, true);
            _animator.SetBool(IsMovingLeft, false);
        }
        else if (_horizontalInput == 0f)
        {
            _animator.SetBool(IsMovingRight, false);
            _animator.SetBool(IsMovingLeft, false);
        }
        else
        {
            _animator.SetBool(IsMovingRight, false);
            _animator.SetBool(IsMovingLeft, true);
        }

    }

    public void SetGunTargetPosition(Vector3 pos)
    {
        GunTargetPosition = pos;
    }

    public void SetGunTargetRaycastHit(RaycastHit hit)
    {
        GunTargetRaycastHit = hit;
    }
}