using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private PlayerData playerData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.playerData = this.GetComponent<PlayerData>();
        this.playerData.RegisterForHealthChanged(OnHealthChanged);
        this.playerData.RegisterForIsDeadChanged(OnIsRespawningChanged);
    }

    private void OnDestroy()
    {
        this.playerData?.UnregisterFromIsDeadChanged(OnIsRespawningChanged);
        this.playerData?.UnregisterFromHealthChanged(OnHealthChanged);
    }

    private void OnHealthChanged(int obj)
    {
        if(obj <= 0)
        {
            this.playerData.IsDead = true;
        }
    }

    private void OnIsRespawningChanged(bool obj)
    {
        this.spriteRenderer.flipY = obj;
    }
}
