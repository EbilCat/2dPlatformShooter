using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks, IPunObservable
{
    public static List<PlayerData> allPlayers = new List<PlayerData>();
    public static event Action<PlayerData> OnPlayerAdded;
    public static event Action<PlayerData> OnPlayerRemoved;

    public static PlayerData LocalPlayer { get; private set;}
    public int ViewId { get => this.photonView.ViewID; }
    public bool IsLocalPlayer { get => this.photonView.IsMine; }


//*====================
//* PlayerName
//*====================
    [SerializeField] private string playerName = "None";
    private event Action<string> OnPlayerNameChanged;
    public string PlayerName
    {
        get => playerName;
        set
        {
            this.playerName = value;
            this.OnPlayerNameChanged?.Invoke(playerName);
        }
    }
    public void RegisterForPlayerNameChanged(Action<string> callback, bool fireCallback = true) 
    { 
        OnPlayerNameChanged -= callback;
        OnPlayerNameChanged += callback;
        if(fireCallback) { callback(playerName); }
    }
    public void UnregisterFromPlayerNameChanged(Action<string> callback) 
    { 
        OnPlayerNameChanged -= callback;
    }


//*====================
//* PlayerScore
//*====================
    [SerializeField] private int playerScore = 0;
    private event Action<int> OnPlayerScoreChanged;
    public int PlayerScore
    {
        get => playerScore;
        set
        {
            this.playerScore = value;
            this.OnPlayerScoreChanged?.Invoke(playerScore);
        }
    }
    public void RegisterForPlayerScoreChanged(Action<int> callback, bool fireCallback = true) 
    { 
        OnPlayerScoreChanged -= callback;
        OnPlayerScoreChanged += callback;
        if(fireCallback) { callback(playerScore); }
    }
    public void UnregisterFromPlayerScoreChanged(Action<int> callback) 
    { 
        OnPlayerScoreChanged -= callback;
    }


//*====================
//* Health
//*====================
    [SerializeField] private int currentHealth = 10;
    public int maxHealth = 10;
    private event Action<int> OnHealthChanged;
    public int Health
    {
        get => currentHealth;
        set
        {
            this.currentHealth = value;
            this.OnHealthChanged?.Invoke(currentHealth);
        }
    }
    public void RegisterForHealthChanged(Action<int> callback, bool fireCallback = true) 
    { 
        OnHealthChanged -= callback;
        OnHealthChanged += callback;
        if(fireCallback) { callback(currentHealth); }
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
        this.PlayerName = this.gameObject.name;
        if(this.photonView.IsMine == true)
        {
            LocalPlayer = this;
        }
        allPlayers.Add(this);
        OnPlayerAdded?.Invoke(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if(this.photonView.IsMine == true)
        {
            LocalPlayer = null;
        }
        allPlayers.Remove(this);
        OnPlayerRemoved?.Invoke(this);
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
            stream.SendNext(PlayerScore);
        }
        else
        {
            this.Health = (int)stream.ReceiveNext();
            this.IsDead = (bool)stream.ReceiveNext();
            this.PlayerScore = (int)stream.ReceiveNext();
        }
    }
}
