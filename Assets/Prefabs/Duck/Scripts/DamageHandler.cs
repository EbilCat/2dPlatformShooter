using Photon.Pun;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private PlayerData playerData;


//*====================
//* UNITY
//*====================
    private void Awake()
    {
        this.playerData = this.GetComponent<PlayerData>();
    }


//*====================
//* TAKE DAMAGE
//*====================
    public void TakeDamage(int sourceViewId, int damage)
    {
        playerData.photonView.RPC("RpcTakeDamage", RpcTarget.All, sourceViewId, damage);
    }

    [PunRPC]
    private void RpcTakeDamage(int sourceViewId, int damage)
    {
        if (playerData.IsLocalPlayer)
        {
            int newHealth = this.playerData.Health - damage;
            if(newHealth < 0) { newHealth = 0; }
            this.playerData.Health = newHealth;
        }
    }


    [ContextMenu("Damage")]
    private void Damage()
    {
        this.playerData.Health--;
    }


    [ContextMenu("Kill")]
    private void Kill()
    {
        this.playerData.Health = 0;
    }
}