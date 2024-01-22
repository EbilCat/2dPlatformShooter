using Photon.Pun;
using UnityEngine.SceneManagement;

public class ExitGameButton : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }
}