using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject vidas1;
    [SerializeField] private GameObject infoPlayer1;
    [SerializeField] private GameObject vidas2;
    [SerializeField] private GameObject infoPlayer2;
    [SerializeField] private GameObject canvaJuego;
    [SerializeField] private GameObject waitRoom;
    [SerializeField] private GameObject ContVidas1;
    [SerializeField] private GameObject ContVidas2;
    [SerializeField] public GameObject objetoDePremio;

    public PhotonView playerPrefab;
    public Transform spawPoint;
   
    public Button ConnectButton;
    public Button JoinRoomButton;

    public int NoJugadores;

    public byte MaxJugadoresRoom = 2;

    public PlayerMovement playerMovementSript;

    public void ConnectToPhoton()
    {
        var settings = PhotonNetwork.PhotonServerSettings;
        settings.DevRegion = "us";
        PhotonNetwork.ConnectUsingSettings();
        JoinRoomButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        ConsoleText.instance.RegisterText("OnConnected To Master");
        ConnectButton.interactable = false;
        JoinRoomButton.interactable = true;

    }

    public void JoinRandom()
    {
        if (!PhotonNetwork.JoinRandomRoom())
        {
            ConsoleText.instance.RegisterText("Fail Joining Room");
        }

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        if(PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = MaxJugadoresRoom}))
        {
            ConsoleText.instance.RegisterText("Room Create");
        }
        else
        {
            ConsoleText.instance.RegisterText("Failed motivo: " + returnCode + "msj:" + message);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        ConsoleText.instance.RegisterText("On Joined Room");
        JoinRoomButton.interactable = false;
        PhotonNetwork.Instantiate(playerPrefab.name, spawPoint.position, spawPoint.rotation);

    }

    [PunRPC]

    private void FixedUpdate()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            NoJugadores = PhotonNetwork.CurrentRoom.PlayerCount;

            ConsolePlayerText.instance1.RegisterText("# de Jugadores: "+NoJugadores+"/"+MaxJugadoresRoom);
        }

        if(NoJugadores == 2)
        {
            canvaJuego.SetActive(true);
            waitRoom.SetActive(false);
            play.SetActive(true);
            ghost.SetActive(true);
            ContVidas1.SetActive(true);
            ContVidas2.SetActive(true);
            objetoDePremio.SetActive(true);

        }
    }

    /*public void Jugar()
    {
        infoPlayer1.SetActive(true);
        infoPlayer2.SetActive(true);
        vidas1.SetActive(true);
        vidas2.SetActive(true);
    }*/
}
