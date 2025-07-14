using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
//using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    // --- COMPONENTES Y AJUSTES PRINCIPALES --- MOVIMIENTO DEL JUGADOR

    public float moveSpeed = 5f;          //velocidad de movimiento 
    public float rotationSpeed = 10f;   //rotacion maxima rota el jugador hacia la direccion de movimiento
    private Rigidbody rb;
    private Vector3 posicionDer = new Vector3(19.24f, 0.15f, 60.97f);
    private Vector3 posicionIzq = new Vector3(38.77f, 0.15f, 60.97f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {

        //if (!photonView.IsMine == false) return;    //Evita que otros jugadores cotrolen a este jugador

        if (photonView.IsMine)
        {
            // --- ENTRADAS POR TECLADO FLECHAS ---
            float horizontalInput = -Input.GetAxis("Horizontal");
            float verticalInput = -Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput)* moveSpeed * Time.deltaTime;
            direction.Normalize();
            rb.MovePosition(direction *moveSpeed*Time.deltaTime);
            //transform.position += direction * moveSpeed * Time.deltaTime;
            //transform.Translate(direction);


            if (direction != Vector3.zero)   //nos dice si el objeto se esta moviendo o nop
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            }
            //MovePosition para respetar colisiones físicas
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PortalIzq"))
        {
            rb.MovePosition(posicionDer);
        }
        if (other.CompareTag("PortalDer"))
        {
            rb.MovePosition(posicionIzq);
        }
    }
}
