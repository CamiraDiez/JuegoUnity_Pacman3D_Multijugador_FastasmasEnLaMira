using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviourPunCallbacks
{

    // --- COMPONENTES Y AJUSTES PRINCIPALES ---
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;

    // Posiciones para teletransporte
    private Vector3 posicionDer = new Vector3(19.24f, 0.15f, 60.97f);
    private Vector3 posicionIzq = new Vector3(38.77f, 0.15f, 60.97f);
    private Vector3 posicionInicial = new Vector3(29.88f, 0.15f, 61.14f);

    // NUEVAS VARIABLES PARA LAS VIDAS
    [Header("Salud del Jugador")]
    public int maxLives = 3; // Vidas máximas del jugador
    private int currentLives; // Vidas actuales
    private int JugadoresVivos = 2;
    public int currentPlayers; //Jugadores Actuales

    // Variable para controlar si el jugador puede moverse
    private bool canMove = true;

    // Opcional: Nombre de la escena a cargar cuando las vidas llegan a cero
    public string gameOverSceneName = "GameOverScene";

    int monedas = 0;
    public Vector3 respawnPoint = new Vector3(37.09f, 0.454f, 68.79f); // Una posición por defecto para mover la moneda.

    GameObject imagenEncontrada;
    GameObject imagenEncontrada2;

    public string tagDelObjeto = "Gano";
    public Vector3 nuevaPosicion = new Vector3(37.09f, 0.454f, 68.79f);


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("ERROR: Rigidbody no encontrado en el jugador.", this);
        }

        // Inicializar las vidas al inicio del juego
        currentLives = maxLives;
        //Debug.Log($"Vidas iniciales del jugador: {currentLives}");
        currentPlayers = JugadoresVivos;
        
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        if (!canMove)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 targetVelocity = moveDirection * moveSpeed;

        rb.MovePosition(transform.position + targetVelocity * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Detecto la Colisión con: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);

        if (!photonView.IsMine) return; // Solo el dueño del jugador procesa las colisiones de daño

        if (collision.gameObject.CompareTag("Moneda"))
        {
            monedas++;
            Debug.Log("Has encontrado una moneda");
            
            if (PhotonNetwork.IsMasterClient)
            {
                GetComponent<PhotonView>().RPC("GetMoney1", RpcTarget.All, monedas);

            }
            else
            {
                GetComponent<PhotonView>().RPC("GetMoney2", RpcTarget.All, monedas);
                
            }
            PhotonView coinPhotonView = collision.gameObject.GetComponent<PhotonView>();

            if (coinPhotonView != null)
            {
                float randomX = Random.Range(20.1f,37.09f);
                float randomZ = Random.Range(52.63f, 68.9f);
                respawnPoint = new Vector3(randomX, 0.5f, randomZ);
                NavMeshHit hit;
                bool found = NavMesh.SamplePosition(respawnPoint, out hit,1f,NavMesh.AllAreas);

                if (found)
                {
                    photonView.RPC("RPC_SyncCoinPosition", RpcTarget.All, coinPhotonView.ViewID, hit.position);
                }
                
            }
            else
            {
                Debug.LogWarning("PlayerMovement: La moneda detectada no tiene un PhotonView. No se puede mover por red.");
            }

        }

        if (collision.gameObject.CompareTag("Gano"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GetComponent<PhotonView>().RPC("Win1", RpcTarget.All, monedas);

            }
            else
            {
                GetComponent<PhotonView>().RPC("Win2", RpcTarget.All, monedas);

            }
        }

        // Lógica de portales
        if (collision.gameObject.CompareTag("PortalIzq"))
        {
            //Debug.Log("Llegó al portal de la Izquierda. Teletransportando a la derecha.");
            TeleportPlayer(posicionDer);
        }
        else if (collision.gameObject.CompareTag("PortalDer"))
        {
            //Debug.Log("Llegó al portal de la Derecha. Teletransportando a la izquierda.");
            TeleportPlayer(posicionIzq);
        }
        // Lógica de colisión con el enemigo
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("¡Colisión con el enemigo detectada!");

            // Llama al método para quitar vida
            TakeDamage(1); // Quita 1 vida al tocar al enemigo. Puedes ajustar este valor.
        }
    }

    // Método para manejar la lógica de recibir daño
    public void TakeDamage(int damageAmount)
    {
        // Asegúrate de que solo el MasterClient o el dueño del objeto aplique el daño.
        // En este caso, como es el jugador el que recibe el daño, lo maneja el dueño.
        if (!photonView.IsMine) return;

        currentLives -= damageAmount;
        //Debug.Log($"El jugador ha recibido {damageAmount} de daño. Vidas restantes: {currentLives}");
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("GetHealth1", RpcTarget.All, currentLives);
        }
        else
        {
            GetComponent<PhotonView>().RPC("GetHealth2", RpcTarget.All, currentLives);
        }

        // Si las vidas llegan a cero o menos, el jugador "muere"
        if (currentLives <= 0)
        {
            Die();
        }
        else // Si aún tiene vidas, teletransporta a la posición inicial
        {
            TeleportPlayer(posicionInicial);
        }
    }

    // Método para manejar la muerte del jugador
    private void Die()
    {
        //JugadoresVivos--;
        //Debug.Log("Jugadores Vivos: " + JugadoresVivos);
        
        // Detener el movimiento y cualquier otra lógica del jugador
        canMove = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (PhotonNetwork.IsMasterClient)
        {
            currentPlayers--;
            Debug.Log("¡El jugador 1 fue el que ha muerto!");
            GetComponent<PhotonView>().RPC("Vivos", RpcTarget.All, currentPlayers);
        }
        else if (!PhotonNetwork.IsMasterClient) 
        {
            currentPlayers--;
            Debug.Log("¡El jugador 2 fue el que ha muerto!");
            GetComponent<PhotonView>().RPC("Vivos", RpcTarget.All, currentPlayers);
        }

        PhotonNetwork.Destroy(photonView.gameObject);

        if (currentPlayers == 0)
        
        {
            GetComponent<PhotonView>().RPC("Vivos", RpcTarget.All, 0);
            Debug.Log("Game Over");
            
            if(PhotonNetwork.IsMasterClient){
              GetComponent<PhotonView>().RPC("RPC_LoadScene", RpcTarget.All, "GameOver");
            }
        }      
    }

    // Método que centraliza la lógica de teletransporte y control de movimiento
    private void TeleportPlayer(Vector3 targetPos)
    {
        canMove = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Mover el Rigidbody a la posición deseada
        rb.MovePosition(targetPos);

        Debug.Log($"Intentando MovePosition a: {targetPos}");

        // Reactivar el movimiento después de un breve retraso
        StartCoroutine(ReactivateMovementAfterDelay(0.1f));
    }

    private IEnumerator ReactivateMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
        Debug.Log("Movimiento reactivado.");
    }

    public void MoverObjetoPorTag()
    {
        // 1. Encontrar el GameObject por su tag.
        GameObject objetoEncontrado = GameObject.FindGameObjectWithTag(tagDelObjeto);

        // 2. Verificar si el GameObject fue encontrado antes de intentar usarlo.
        if (objetoEncontrado != null)
        {
            // 3. Acceder al componente Transform y asignar la nueva posición.
                       
            float randomX = Random.Range(20.1f, 37.09f);
            float randomZ = Random.Range(52.63f, 68.9f);
            nuevaPosicion = new Vector3(randomX, 0.5f, randomZ);
            NavMeshHit hit;
            bool found = NavMesh.SamplePosition(nuevaPosicion, out hit, 1f, NavMesh.AllAreas);

            if (found)
            {
                //photonView.RPC("RPC_SyncCoinPosition", RpcTarget.All, coinPhotonView.ViewID, hit.position);
                objetoEncontrado.transform.position = nuevaPosicion;
                Debug.Log($"Objeto con tag '{tagDelObjeto}' movido a {nuevaPosicion}.");
            }
            
        }
        else
        {
            // Si el objeto no se encuentra, muestra una advertencia útil.
            Debug.LogWarning($"ERROR: No se encontró ningún GameObject con el tag '{tagDelObjeto}' en la escena. " +
                             "Asegúrate de que el tag esté bien escrito y que el GameObject esté activo en la jerarquía.");
        }
    }

    [PunRPC]
    private void Vivos(int currentPlayers)
    {
        ConsoleVivos.instance.RegisterText("Players: " + currentPlayers);
    }

    [PunRPC]
    private void GetHealth1(int currentLives)
    {
        ConsoleLives1.instance1.RegisterText("healt is: " + currentLives);
        if(currentLives == 2)
        {
            imagenEncontrada = GameObject.FindGameObjectWithTag("Vidas1.3");
            imagenEncontrada.SetActive(false);
        }
        if (currentLives == 1)
        {
            imagenEncontrada = GameObject.FindGameObjectWithTag("Vidas1.2");
            imagenEncontrada.SetActive(false);
        }
        if (currentLives == 0)
        {
            imagenEncontrada = GameObject.FindGameObjectWithTag("Vidas1.1");
            imagenEncontrada.SetActive(false);
            
        }

    }
    [PunRPC]
    private void GetHealth2(int currentLives)
    {
        ConsoleLives2.instance1.RegisterText("healt is: " + currentLives);
        if (currentLives == 2)
        {
            imagenEncontrada2 = GameObject.FindGameObjectWithTag("Vidas2.3");
            imagenEncontrada2.SetActive(false);
        }
        if (currentLives == 1)
        {
            imagenEncontrada2 = GameObject.FindGameObjectWithTag("Vidas2.2");
            imagenEncontrada2.SetActive(false);
        }
        if (currentLives == 0)
        {
            imagenEncontrada2 = GameObject.FindGameObjectWithTag("Vidas2.1");
            imagenEncontrada2.SetActive(false);
        }
    }

    [PunRPC]
    private void Win1(int cerezas)
    {
        Debug.Log("Nivel Superado");
        PhotonNetwork.LoadLevel("NivelSuperado");
    }
    [PunRPC]
    private void Win2(int cerezas)
    {
        Debug.Log("Nivel Superado");
        PhotonNetwork.LoadLevel("NivelSuperado");
    }

    [PunRPC]
    private void GetMoney1(int monedas)
    {
        ConsoleMoney1.instance1.RegisterText(""+ monedas);
        if (monedas >= 5)
        {
            MoverObjetoPorTag();
        }

    }
    [PunRPC]
    private void GetMoney2(int monedas)
    {
        ConsoleMoney2.instance1.RegisterText("" + monedas);
        if (monedas >= 5)
        {
            MoverObjetoPorTag();
        }
    }

    [PunRPC]
    private void RPC_LoadScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    [PunRPC] // ¡Esta etiqueta es ABSOLUTAMENTE IMPRESCINDIBLE para que Photon lo reconozca como un RPC!
    private void RPC_SyncCoinPosition(int coinViewID, Vector3 syncedPosition)
    {
        // Todos los clientes buscan la moneda usando su ID.
        PhotonView coinPV = PhotonView.Find(coinViewID);

        if (coinPV != null)
        {
            // Solo el Master Client tiene la autoridad para modificar la posición de objetos de red
            // y garantizar que se refleje correctamente. Los demás clientes solo aplican el cambio.
            if (PhotonNetwork.IsMasterClient)
            {
                // El Master Client (dueño implícito o autorizado) mueve el objeto.
                // Photon se encargará de que esta posición se replique si hay PhotonTransformView.
                coinPV.gameObject.transform.position = syncedPosition;
                Debug.Log($"Master Client: Moneda {coinPV.gameObject.name} (ViewID: {coinViewID}) movida autoritativamente a: {syncedPosition}");
            }
            else
            {
                // Los clientes que no son Master Client simplemente aplican la posición recibida.
                coinPV.gameObject.transform.position = syncedPosition;
                Debug.Log($"Cliente: Moneda {coinPV.gameObject.name} (ViewID: {coinViewID}) sincronizada a la posición: {syncedPosition}");
            }

            // Si la moneda se desactivó temporalmente en el jugador que la recogió, actívala de nuevo.
            // Asegúrate de que la moneda esté activa en la nueva posición.
            coinPV.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Cliente: No se encontró la moneda con ViewID: {coinViewID} para sincronizar su posición. Puede que haya sido destruida o aún no instanciada.");
        }
    }
}