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
                    PhotonNetwork.LoadLevel("GameOverScene");
                    Debug.Log("GameOver");
                }
                
            }
        }
    }
}
