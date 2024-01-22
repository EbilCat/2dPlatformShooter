using TMPro;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private PlayerData playerData;


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
    }
}
