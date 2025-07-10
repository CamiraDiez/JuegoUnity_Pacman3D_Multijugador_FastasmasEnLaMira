
using System.Collections;
using System.Collections.Generic;   //para los ciclos for 
using UnityEngine;
using UnityEngine.AI;     //Libreria para usar la ia para los enemigos (importa el sistema NavMesh)

//using Photon.Pun;       //CAMI BORRA EL COMENTARIO


[RequireComponent (typeof (NavMeshAgent))]       //vamos a usar NavMesh y lo agrege por defecto
public class EnemyFollow : MonoBehaviour
{

    // --- REFERENCIAS  ---
    public Transform playerTarget;        //El jugador al que el enemigo va a seguir
    //Vector3 dest;    //aqui ya esta privado, cordenadas x, y, z
    private NavMeshAgent agent;          //moverlo automaticamente en el laberinto 

    // --- DETECCION DEL JUGADOR  ---

    public LayerMask playerLayer;    //para hacer la deteccion de solo el jugador por parte del enemigo
    //public float rangoEnemigo;      //Distancia donde el enemigo puede detectar al jugador ...los fantasmas siguen al jugador solo si esta cerca
    //bool enemigoActivo;             //vamos a guardar si el enemigo ya esta persiguiendo al jugador o nop
    public float detectionRadius = 5f;     //Radio en el que el enemigo detecta al jugador

    private bool playerInRange = false;     //mria si el jugador esta dentro del rango 

    //PhotonView photonView;        //CAMI BORRA ESTE COMENTARIO

    // Se ejecuta una sola vez cuando el enemigo aparece en escena
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();     //usamos la variable "agente" para decirle al enemigo a donde moverse, cual es la velocidad....
    
        //dest = agente.destination;            //variable= dest la posicion actual hacia donde se esta moviendo el enemigo

        //photonView = GetComponent<PhotonView>();    //CAMI BORRA ESTE COMENTARIO 
        
    }

    // 
    void Update()
    {


        //if (!photonView.IsMine) return;


        //enemigoActivo = Physics.CheckSphere(transform.position, rangoEnemigo, LayerPlayer);   //metodo checkspphere
        playerInRange = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        //if(enemigoActivo == true)      //entro en la espera? si si entonces..
        if(playerInRange && playerTarget != null)
        {
            float distance = Vector3.Distance(transform.position, playerTarget.position);
            //if(Vector3.Distance (dest, target.position) > 1.0f)
            if(distance >1.0f)
            {
                //dest = target.position;       //El enemigo nos empiea a perseguir porque esta buscando nuestra posicion
                //agente. destination = dest;
                agent.SetDestination(playerTarget.position);
            }


        }
        
    }
     //vamos a ver la esfera para ver como ocurre todo-Gizmos 
        //private void OnDrawGimos()
    void OnDrawGimosSelected()
    {
            //vamos a dibujar un circulo para mostrar el rango de deteccion
            Gizmos.color = new Color(1f, 0.5f,0f);    //Naranja intenso
            //Gizmos.DrawWireSphere(transform. position, rangoEnemigo);
            Gizmos.DrawWireSphere(transform. position, detectionRadius);
    }

}



