using UnityEngine;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class GameLauncher : MonoBehaviourPunCallbacks
    {
#region Private Fields
        [SerializeField] private byte maxPlayersPerRoom = 4;
        private bool isConnecting;
        private string gameVersion = "1";

#endregion

#region MonoBehaviour CallBacks
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = this.gameVersion;
        }

#endregion

#region Public Methods
        public void Connect()
        {
            isConnecting = true;

            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Joining Room...");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.Log("Connecting to Master");
                PhotonNetwork.ConnectUsingSettings();
            }
        }

#endregion

#region MonoBehaviourPunCallbacks CallBacks                
        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("ConnectedToMaster");
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No existing room found, creating a room");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to create room");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected: " + cause);
            isConnecting = false;
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");
                PhotonNetwork.LoadLevel("PunBasics-Room for 1");

            }
        }

#endregion
    }
}
