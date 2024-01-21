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
        this.playerData.OnHealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        this.playerData.OnHealthChanged -= OnHealthChanged;
    }


//*====================
//* CALLBACKS
//*====================
    private void OnHealthChanged(int obj)
    {
        text.text = obj.ToString(); 
    }
}
