using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFollow : MonoBehaviourPun
{
    [Header("Detección y Persecución")]
    public float detectionRadius = 5f;
    public LayerMask playerLayer; // Asegúrate de que tu jugador esté en esta capa
    public float stoppingDistance = 1.0f; // Distancia para que el NavMeshAgent se detenga antes de superponerse

    private NavMeshAgent agent;
    private Transform playerTarget;
    private bool playerInRange = false;

    void Awake() // Usar Awake para inicializar antes que Start
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.stoppingDistance = stoppingDistance; // Asignar la distancia de frenado
        }
        else
        {
            Debug.LogError("NavMeshAgent no encontrado en el enemigo.", this);
        }
    }

    void Update()
    {
        // Solo el MasterClient controla la IA para evitar duplicar lógica en clientes
        if (!PhotonNetwork.IsMasterClient) return;

        // --- Lógica de Detección del Jugador ---
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (playersInRange.Length > 0)
        {
            // Encuentra al jugador más cercano
            Transform closestPlayer = null;
            float closestDistance = Mathf.Infinity;

            foreach (var hit in playersInRange)
            {
                // Solo considera jugadores que tengan un PhotonView y que sean el objeto raíz instanciado por Photon
                if (hit.transform.root.GetComponent<PhotonView>() != null)
                {
                    float distance = Vector3.Distance(transform.position, hit.transform.root.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlayer = hit.transform.root; // Obtén la raíz del GameObject del jugador
                    }
                }
            }

            playerTarget = closestPlayer;
            playerInRange = (closestPlayer != null); // Solo in range si realmente encontramos un jugador válido
        }
        else
        {
            playerInRange = false;
            playerTarget = null;
        }

        // --- Lógica de Persecución ---
        if (playerInRange && playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);
        }
        else
        {
            // Si no hay jugador en rango, el enemigo puede patrullar o quedarse quieto
            agent.ResetPath(); // O agent.SetDestination(transform.position); para detenerse suavemente
        }
    }

    // --- Detección de Colisión/Contacto con el Jugador (Usando Trigger) ---
    private void OnTriggerEnter(Collider other)
    {
        // Esta condición asegura que solo el MasterClient procesa el evento de trigger,
        // consistente con la lógica de IA.
        if (!PhotonNetwork.IsMasterClient) return;

        // Comprueba si el objeto con el que colisionamos pertenece a la capa del jugador
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            // Opcional: Comprueba si es el jugador correcto o solo cualquier objeto en esa capa
            // Si tu jugador instanciado tiene un componente PhotonView en su raíz:
            if (other.transform.root.GetComponent<PhotonView>() != null)
            {
                Debug.Log($"¡Enemigo ha tocado al jugador! Objeto: {other.gameObject.name}, Tag: {other.tag}");
                // Aquí puedes añadir la lógica de lo que pasa al "tocar" al jugador:
                // - Hacer daño al jugador
                // - Detener al enemigo (agent.ResetPath() o agent.isStopped = true;)
                // - Activar una animación de ataque

                agent.isStopped = true; // Detener al enemigo
                // Por ejemplo: StartCoroutine(AttackPlayerRoutine(other.transform.root));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            if (other.transform.root.GetComponent<PhotonView>() != null)
            {
                Debug.Log($"El enemigo ha dejado de tocar al jugador. Objeto: {other.gameObject.name}");
                agent.isStopped = false; // Reanudar movimiento si es necesario
            }
        }
    }

    // Opcional: Para visualizar el radio de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.75f);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Para visualizar la stoppingDistance del agente
        if (agent != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
        }
    }
}