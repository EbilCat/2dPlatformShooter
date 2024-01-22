using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private PlayerData playerData;
    [SerializeField] private SpriteRenderer spriteRenderer;


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
    private void OnIsDeadChanged(bool obj)
    {
        this.spriteRenderer.flipY = obj;
    }
}
