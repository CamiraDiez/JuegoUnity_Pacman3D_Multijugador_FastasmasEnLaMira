using Photon.Pun;
using UnityEngine;

public class PlayerMulti : MonoBehaviour
{
    [SerializeField] private int health = 2;

    public int Health => health;

    [PunRPC]
    private void GetHealth(int health)
    {
        ConsoleLives1.instance1.RegisterText("healt is: " + health);
    }
    //[ContextMenu("Sent rpc Health")]

    public void SentRPCHealth()
    {
        GetComponent<PhotonView>().RPC("GetHealth", RpcTarget.All,health);
    }

    private void Update()
    {
        SentRPCHealth();
    }
}
