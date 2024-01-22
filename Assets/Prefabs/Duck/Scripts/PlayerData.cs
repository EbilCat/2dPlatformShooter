using System;
using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerData LocalPlayer { get; private set;}
    public int ViewId { get => this.photonView.ViewID; }
    public bool IsLocalPlayer { get => this.photonView.IsMine; }


//*====================
//* Health
//*====================
    [SerializeField] private int health = 10;
    private event Action<int> OnHealthChanged;
    public int Health
    {
        get => health;
        set
        {
            this.health = value;
            this.OnHealthChanged?.Invoke(health);
        }
    }
    public void RegisterForHealthChanged(Action<int> callback, bool fireCallback = true) 
    { 
        OnHealthChanged -= callback;
        OnHealthChanged += callback;
        if(fireCallback) { callback(health); }
    }
    public void UnregisterFromHealthChanged(Action<int> callback) 
    { 
        OnHealthChanged -= callback;
    }


//*====================
//* IsRespawning
//*====================
    private bool isDead = false;
    private event Action<bool> OnIsDeadChanged;
    public bool IsDead
    {
        get => isDead;
        set
        {
            this.isDead = value;
            this.OnIsDeadChanged?.Invoke(isDead);
        }
    }
    public void RegisterForIsDeadChanged(Action<bool> callback, bool fireCallback = true) 
    { 
        OnIsDeadChanged -= callback;
        OnIsDeadChanged += callback;
        if(fireCallback) { callback(isDead); }
    }
    public void UnregisterFromIsDeadChanged(Action<bool> callback) 
    { 
        OnIsDeadChanged -= callback;
    }


//*====================
//* MonoBehaviourPunCallbacks
//*====================
    public override void OnEnable()
    {
        base.OnEnable();
        this.gameObject.name = $"Player {this.photonView.ViewID.ToString()}";
        if(this.photonView.IsMine == true)
        {
            LocalPlayer = this;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        LocalPlayer = null;
    }


//*====================
//* IPunObservable
//*====================
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
            stream.SendNext(IsDead);
        }
        else
        {
            this.Health = (int)stream.ReceiveNext();
            this.IsDead = (bool)stream.ReceiveNext();
        }
    }
}
