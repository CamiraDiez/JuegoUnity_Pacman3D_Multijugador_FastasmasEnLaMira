
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;                                       //CAMI QUITA ESTE COMENTARIO   

public class PlayerMovement : MonoBehaviour
{

    // --- COMPONENTES Y AJUSTES PRINCIPALES --- MOVIMIENTO DEL JUGADOR


    public float moveSpeed = 5f;          //velocidad de movimiento 
    //public float move= 5f;   
    public float rotationSpeed = 10f;   //rotacion maxima rota el jugador hacia la direccion de movimiento

    //PhotonView photonView;                             //CAMI QUITA ESTE COMENTARIO 

    void Start()
    {
        //photonView = GetComponent<PhotonView>();              //CAMI QUITA ESTE COMENTARIO

    }

    
    void Update()
    {

        // if (!photonView.IsMine) return;    //Evita que otros jugadores cotrolen a este jugador

        // --- ENTRADAS POR TECLADO FLECHAS ---
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");

        // --- VECTOR DE MOVIMIENTO SEGUN ENTRADA DEL JUGADOR  ---

        //Vector3 move =  new Vector3(horizontalInput, 0f, verticalInput);   //momiento en x, z
        Vector3 direction= new Vector3(horizontalInput, 0f, verticalInput);
        //move.Normalize();
        direction.Normalize();      //Evita que el jugador se mueva mas rapido en diagonal

        // --- MOVER AL PERSONAJE  ---

        //transform.position =  transform.position + move * speed * Time.deltaTime;   //deltaTime ayuda al movimiento se vea bien
        transform.position += direction * moveSpeed * Time.deltaTime;

 
        if (direction != Vector3.zero)   //nos dice si el objeto se esta moviendo o nop
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            //transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), rotationSpeed * Time.deltaTime);//quaterion es la funcion para los angulos
            transform.rotation =  Quaternion.Slerp(transform.rotation,targetRotation, rotationSpeed * Time.deltaTime);


        }
        
    }
}


