using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playable : MonoBehaviour
{
    private Animator animator;

    private float horizontalInput, verticalInput;

    private static readonly int IsMovingForward = Animator.StringToHash("isMovingForward");
    private static readonly int IsMovingBackwards = Animator.StringToHash("isMovingBackwards");
    private static readonly int IsMovingRight = Animator.StringToHash("isMovingRight");
    private static readonly int IsMovingLeft = Animator.StringToHash("isMovingLeft");

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
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
