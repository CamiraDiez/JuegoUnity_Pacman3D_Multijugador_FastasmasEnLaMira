using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFollow : MonoBehaviourPun
{
    public float detectionRadius = 5f;
    public LayerMask playerLayer;

    private NavMeshAgent agent;
    private Transform playerTarget;
    private bool playerInRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Solo el MasterClient controla la IA (recomendado)
        if (!PhotonNetwork.IsMasterClient) return;

        // Buscar al jugador más cercano dentro del radio de detección
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (playersInRange.Length > 0)
        {
            Transform closestPlayer = null;
            float closestDistance = Mathf.Infinity;

            foreach (var hit in playersInRange)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = hit.transform;
                }
            }

            playerTarget = closestPlayer;
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
            playerTarget = null;
        }

        // Si hay un jugador cerca, perseguirlo
        if (playerInRange && playerTarget != null)
        {
            float distance = Vector3.Distance(transform.position, playerTarget.position);
            if (distance > 1.0f)
            {
                agent.SetDestination(playerTarget.position);
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}