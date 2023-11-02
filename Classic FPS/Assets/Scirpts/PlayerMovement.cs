using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    
    public float PlayerWalkingSpeed = 5f;
    public float PlayerRunningSpeed = 15f;
    public float JumpStrenght = 20f;
    public float verticalRotationLimit = 80f;

    float forwardMovement;
    float sidewaysMovement;
    float VerticalVelosity;

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
        if (cc.isGrounded)
        {
            forwardMovement = Input.GetAxis("Vertical") * PlayerWalkingSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * PlayerWalkingSpeed;

            if (Input.GetKey(KeyCode.LeftShift)) //sprawdzamy czy gracz chce sprint
            {
                forwardMovement = Input.GetAxis("Vertical") * PlayerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * PlayerRunningSpeed;
            }
        }

        VerticalVelosity += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButton("Jump") && cc.isGrounded) //sprawdzamy czy gracz jest uziemiony
        {
            VerticalVelosity = JumpStrenght;
        }

        Vector3 PlayerMovement = new Vector3(sidewaysMovement, VerticalVelosity, forwardMovement);

        cc.Move(transform.rotation * PlayerMovement * Time.deltaTime);
    }
}
