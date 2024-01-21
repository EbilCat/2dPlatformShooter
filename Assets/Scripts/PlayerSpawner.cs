using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    private GameObject localPlayer;

    private void Start()
    {
        SpawnPlayer();
    }

    public override void OnJoinedRoom()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if(PhotonNetwork.InRoom && this.localPlayer == null)
        {
            Debug.Log("Spawning local player");
            this.localPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
    }
}
