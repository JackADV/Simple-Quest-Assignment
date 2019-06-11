using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    #region Varibles
    [Header("Stats")]
    public float maxhealth = 10f; // Max health of the player
    public float curHealth = 10f; // Current health of the player
    public int exp = 100; // Current EXP of the player
    public float moveSpeed = 10f; // Players movement speed

    [Header("Equipment")]
    public int gold = 1000; // Gold in the players inventory

    [Header("Movement")]
    public Vector3 moveDirection;
    public static bool canMove; // True or false statement whether the player 
    private CharacterController charC;

    [Header("MouseLook")]
    public Rotationalaxis mouseAxis = Rotationalaxis.mouseXandY; // The direction the mouse will move the camera

        [Header("Sensitivity")] // How sensitive the camera/mouse interaction will be
    [Range(0, 10)]
    public float sensX = 1f;
    [Range(0, 10)]
    public float sensY = 1f;

    [Header("Mouse Rotation")] // Declaring the roation of the mouse and camera
    private float rotationX = 0f;
    private float rotationY = 0f;
    private float minY = -60f;
    private float maxY = 60f;

    [Header("Camera")]
    public new GameObject camera; // Declaring the camera of the player


    #endregion

    #region Start
    void Start()
    {
        canMove = true;
        charC = GetComponent<CharacterController>();

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    #endregion

    #region Update
    void Update()
    {
        mouselook();
        Move();
        Interact();
    }
    #endregion
    #region Quest Complete
    public void QuestComplete()
    {
        gold += 1000; // When the quest is completed recieve this reward
    }
    #endregion

    #region Move
    public void Move()
    {
        if (canMove)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // The two axis in which you are moving
            moveDirection = transform.TransformDirection(moveDirection); // move that direction
            moveDirection *= moveSpeed; // Travel at this speed
            charC.Move(moveDirection * Time.deltaTime); // Moving via real time
        }
    }
    #endregion
    #region Interact
    public void Interact()
    {
        if (Input.GetKeyUp(KeyCode.E)) // If the E key is pressed
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                if (hitInfo.collider.CompareTag("NPC")) // The raycast is checking the tag of what you are trying to interact with to make sure it has the correct tag to start the approapiate dialogue
                {
                    Debug.Log("Talking to the NPC"); // A debug log for testing as to whether the interaction is working and taking place
                    NPC dialogue = hitInfo.collider.GetComponent<NPC>();

                    if (dialogue != null) // Dialogue is active
                    {
                        dialogue.startQuest.SetActive(true); // Quest is now active
                        canMove = false; // You cannot move
                        Cursor.lockState = CursorLockMode.None; // Cursor is not locked
                        Cursor.visible = true; // You can see the cursor
                    }

                }

                if (hitInfo.collider.CompareTag("ButtonQuest"))
                {
                    Debug.Log("Talking to the Quest Box"); // A debug log for testing as to whether the interaction is working and taking place
                    ButtonQuest dialogue = hitInfo.collider.GetComponent<ButtonQuest>(); // Get the dialogue from the quest button

                    if (dialogue != null) // Dialogue is active
                    {
                        dialogue.button.SetActive(true); // Dialogue box appears
                        canMove = false; // You cannot move
                        Cursor.lockState = CursorLockMode.None; // Cursor is not locked
                        Cursor.visible = true; // You can see the cursor
                    }

                }
            }
        }
    }
    #endregion
    #region MouseLook
    public void mouselook()
    {
        if (canMove) // If not talking to the NPC/quest giver the cursor is not visible and you are able to move as per normal
        {

            rotationY += Input.GetAxis("Mouse Y") * sensY; // Move the mouse on the Y axis
            rotationX += Input.GetAxis("Mouse X") * sensX; // Move the mouse on the X axis
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            Cursor.lockState = CursorLockMode.Locked; // The cursor is locked
            Cursor.visible = false; // The cursor is not visible

        }
    }
    #endregion

    #region Rotationalaxis
    public enum Rotationalaxis
    {
        mouseXandY // The two axis which you can rotate
    }
    #endregion

    #region Interact End
    public void InteractEnd()
    {
        canMove = true; // I can move again
    }
    #endregion
}


