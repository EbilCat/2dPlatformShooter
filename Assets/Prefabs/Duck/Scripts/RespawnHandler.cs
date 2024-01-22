using System.Collections;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    private PlayerData playerData;


//*====================
//* UNITY
//*====================
    private void Awake()
    {
        this.playerData = this.GetComponent<PlayerData>();

        this.playerData.RegisterForIsDeadChanged(OnIsDeadChanged);

    }

    private void OnDestroy()
    {
        this.playerData?.UnregisterFromIsDeadChanged(OnIsDeadChanged);
    }


//*====================
//* CALLBACKS
//*====================
    private void OnIsDeadChanged(bool isDead)
    {
        if (playerData.IsLocalPlayer)
        {
            if (isDead)
            {
                StartCoroutine(Respawn());
            }
        }
    }


//*====================
//* PRIVATE
//*====================
    private IEnumerator Respawn()
    {
        //Print the time of when the function is first called.
        Debug.Log("Waiting for respawn");
        yield return new WaitForSeconds(3);
        this.playerData.IsDead = false;
        this.playerData.Health = this.playerData.maxHealth;
        this.playerData.transform.position = Vector3.zero;
    }
}