using System;
using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private int health = 10;
    public event Action<int> OnHealthChanged;
    public int Health
    {
        get => health;
        private set
        {
            this.health = value;
            this.OnHealthChanged?.Invoke(health);
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
        }
        else
        {
            this.Health = (int)stream.ReceiveNext();
        }
    }

    public void TakeDamage(int damage)
    {
        photonView.RPC("RpcTakeDamage", RpcTarget.All, damage);
    }


    [PunRPC]
    private void RpcTakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            Health -= damage;
        }
    }
}
