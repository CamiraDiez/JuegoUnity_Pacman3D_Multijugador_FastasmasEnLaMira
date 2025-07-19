using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<PlayerMovement> listaJugadores = new List<PlayerMovement>();
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void AddPlayer(PlayerMovement jugador)
    {
        listaJugadores.Add(jugador);
    }
    private void Update()
    {
        bool todosMuertos = true;
        if(listaJugadores.Count > 0)
        {
            foreach (var jugador in listaJugadores)
            {
                if (jugador.CurrentLives > 0)
                {
                    todosMuertos = false;
                }
            }
            if (todosMuertos)
            {
                //Game Over
                if (PhotonNetwork.IsMasterClient)
                {
                    listaJugadores[0].photonView.RPC("CargarEscena",RpcTarget.All);
                    //PhotonNetwork.LoadLevel("GameOverScene");
                    Debug.Log("GameOver");
                }
                
            }
        }
    }
}
