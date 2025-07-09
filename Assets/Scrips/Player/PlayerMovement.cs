using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;          //velocidad la podemos cambiar en unity     
    public float rotationSpeed = 10f;   //rotacion tambien la podemos cambiar en unity

    void Start()
    {

    }



    // 
    void Update()
    {
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");

        //Movimiento
        Vector3 move =  new Vector3(horizontalInput, 0f, verticalInput);   //momiento en x, z
        move.Normalize();
        transform.position =  transform.position + move * speed * Time.deltaTime;   //deltaTime ayuda al movimiento se vea bien
 
        if (move != Vector3.zero)   //nos dice si el objeto se esta moviendo o nop
        {
            transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), rotationSpeed * Time.deltaTime);//quaterion es la funcion para los angulos

        }
        
    }
}
