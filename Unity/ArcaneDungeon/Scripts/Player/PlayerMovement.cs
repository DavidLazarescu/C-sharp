using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //ints
    [SerializeField] private int walkSpeed;
    [SerializeField] private int runningSpeed;
    private int movementSpeed;                   //this gets set to walk or run and actually applied multiplied with

    //floats
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;

    //bools
    private bool isGrounded = false;


    //Audio bools
    private bool playWalkSound = false;
    private bool playRunSound = false;
    private bool playJumpSound = false;
    private bool playLadingSound = false;


    //Vector3's
    Vector3 verticalMovementSpeed = new Vector3(0, 0, 0);

    //LayerMasks
    [SerializeField] private LayerMask groundLayerMask;

    //GameObjects
    [SerializeField] private Transform groundCheck;

    //Controller
    [SerializeField] private CharacterController characterController;

    //AudioManager
    private AudioManager audioManager;


    
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    private void Update()
    {
        checkIfGrounded();
        speedRegulator();
        checkIfJumping();
        moveThePlayer();
        playerMovementSounds();
    }

    private void checkIfGrounded()
    {
        bool temp = isGrounded;
        /*Checks if the user is on the ground*/
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayerMask);
        
        //sets "playLadingSound" true, if player was in the air, and after hit the ground
        if (!temp && isGrounded)
            playLadingSound = true;
    }

    private void speedRegulator()
    {
        /*This sets the 'movementSpeed' variable to a certain speed e.g. running*/
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            movementSpeed = runningSpeed;
            playRunSound = true;
        }
        else
        {
            movementSpeed = walkSpeed;
        }
            
    }

    private void setverticalMovementSpeed()
    {
        /*Sets the vertical movement speed, the vertical movement speed is a "force" which gets added to the y coordinate of the
         player transfrom, gravity gets applied to it every frame to have a drag down*/
        if (isGrounded && verticalMovementSpeed.y < 0)
        {
            verticalMovementSpeed.y = -2f;
        }
    }

    private void checkIfJumping()
    {
        /*Checks if the player is jumping*/
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playJumpSound = true;
            verticalMovementSpeed.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void moveThePlayer()
    {
        /*Moves the player by getting ints keyboard input and multiplying it with the previous calculated speed*/


        //Get horizontal input on x
        float x = Input.GetAxis("Horizontal");
        //Get vertical input on z
        float z = Input.GetAxis("Vertical");

        //Applys the new position to the Vector3
        Vector3 move = transform.right * x + transform.forward * z;

        //Player horizontal movement
        characterController.Move(move * movementSpeed * Time.deltaTime);

        //Player vertical movvment
        verticalMovementSpeed.y += gravity * Time.deltaTime;
        characterController.Move(verticalMovementSpeed * Time.deltaTime);


        //Play sounds if players walks
        if ((x != 0 || z != 0))
            playWalkSound = true;

    }

    private void playerMovementSounds()
    {
        
        //RUnning and walking
        if (playRunSound && playWalkSound && !audioManager.isRunning && isGrounded)
        {
            audioManager.playSound("Player_Run", audioManager.playerSounds);
        }
        else if (playWalkSound && !audioManager.isRunning && !audioManager.isWalking && isGrounded)
        {
            audioManager.playSound("Player_Walk", audioManager.playerSounds);
        }


        //Jumping and Landing
        if (playJumpSound)
            audioManager.playSound("Player_Jumping", audioManager.playerSounds);
        else if (playLadingSound)
            audioManager.playSound("Player_Landing", audioManager.playerSounds);


        playWalkSound = false;
        playRunSound = false;
        playJumpSound = false;
        playLadingSound = false;
    }
}
