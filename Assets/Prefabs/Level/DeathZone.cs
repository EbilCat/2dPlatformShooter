using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
        if(playerData != null && playerData.IsLocalPlayer)
        {
            playerData.Health = 0;
        }
    }
}
