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
        if (playerData.IsDead == false)
        {
            playerData.photonView.RPC("RpcTakeDamage", RpcTarget.All, sourceViewId, damage);
        }
    }

    [PunRPC]
    private void RpcTakeDamage(int sourceViewId, int damage)
    {
        int newHealth = this.playerData.Health - damage;
        if (newHealth < 0)
        {
            newHealth = 0;
        }

        if (playerData.IsLocalPlayer)
        {
            if (newHealth == 0)
            {
                this.playerData.IsDead = true;
                if (sourceViewId == -1)
                {
                    this.playerData.PlayerScore--;
                }
            }

            this.playerData.Health = newHealth;
        }

        if (newHealth == 0 && PlayerData.LocalPlayer.ViewId == sourceViewId)
        {
            PlayerData.LocalPlayer.PlayerScore++;
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