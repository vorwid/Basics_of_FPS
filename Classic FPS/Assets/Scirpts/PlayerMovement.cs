using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrenght = 20f;
    public float verticalRotationLimit = 80f;

    float forwardMovement;
    float sidewaysMovement;
    float verticalVelocity;

    float verticalRotation = 0;
    CharacterController cc;
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rozgladanie sie na boki
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        //Rozgladanie sie gora i dol
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Player movement
        forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
        sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
           forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
           sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed; 
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if(Input.GetButton("Jump"))
        {
            verticalVelocity = jumpStrenght;
        }

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);

    }
}
