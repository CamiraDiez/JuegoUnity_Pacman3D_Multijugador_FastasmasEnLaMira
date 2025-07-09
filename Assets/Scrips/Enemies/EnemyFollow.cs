using System.Collections;
using System.Collections.Generic;   //para los ciclos for 
using UnityEngine;
using UnityEngine.AI;     //Libreria para usar la ia para los enemigos (importa el sistema NavMesh)


[RequireComponent (typeof (NavMeshAgent))]       //vamos a usar NavMesh y lo agrege por defecto
public class EnemyFollow : MonoBehaviour
{

    public Transform target;
    Vector3 dest;    //aqui ya esta privado, cordenadas x, y, z
    NavMeshAgent agente;          //moverlo automaticamente en el laberinto 

    public LayerMask LayerPlayer;    //para hacer la deteccion de solo el jugador por parte del enemigo
    public float rangoEnemigo;      //Distancia donde el enemigo puede detectar al jugador ...los fantasmas siguen al jugador solo si esta cerca
    bool enemigoActivo;             //vamos a guardar si el enemigo ya esta persiguiendo al jugador o nop

    // Se ejecuta una sola vez cuando el enemigo aparece en escena
    void Start()
    {
        agente = GetComponent<NavMesh>();     //usamos la variable "agente" para decirle al enemigo a donde moverse, cual es la velocidad....
    
        dest = agente.destination;            //variable= dest la posicion actual hacia donde se esta moviendo el enemigo
        
    }

    // 
    void Update()
    {
        
    }
}
