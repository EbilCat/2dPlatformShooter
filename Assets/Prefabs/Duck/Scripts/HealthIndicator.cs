using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Image healthBar;


//*====================
//* UNITY
//*====================
    private void Awake()
    {
        this.playerData.RegisterForHealthChanged(OnHealthChanged);
    }

    private void OnDestroy()
    {
        this.playerData.UnregisterFromHealthChanged(OnHealthChanged);
    }


//*====================
//* CALLBACKS
//*====================
    private void OnHealthChanged(int obj)
    {
        text.text = obj.ToString(); 
        healthBar.rectTransform.anchorMax = new Vector2((float)obj / (float)this.playerData.maxHealth, 1.0f);
    }
}
