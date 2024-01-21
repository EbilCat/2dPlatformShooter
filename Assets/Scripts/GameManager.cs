using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if(PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene(0);
        }
    }
}
