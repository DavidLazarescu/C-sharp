using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAround : MonoBehaviour
{

    //Floats
    private float xRotation;

    //Transfroms
    [SerializeField] private Transform player;

    //SettingsHandler
    [SerializeField] private SettingsHandler settingsHandler;

    //Inventory
    public Inventory inv;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        lookAround();
    }

    private void lookAround()
    {
        if(!settingsHandler.inSettings && !inv.inventoryEnabled)
		{
            //Get Input from Mouse on X-Axis
            float mouseX = Input.GetAxis("Mouse X") * settingsHandler.mouseSensitivity * Time.deltaTime;
            //Get Input from Mouse on Y-Axis
            float mouseY = Input.GetAxis("Mouse Y") * settingsHandler.mouseSensitivity * Time.deltaTime;

            //Set Rotation to the Camera on the X-Axis
            player.Rotate(Vector3.up * mouseX);
            //Set mouseY variable to xRotation
            xRotation -= mouseY;
            //Clamp: Can not look more than 90 degrees up / down
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //Set Rotation to the Camera on the Y-Axis
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
