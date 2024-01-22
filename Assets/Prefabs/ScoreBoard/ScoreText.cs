using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private PlayerData playerData;
    private TextMeshProUGUI text;

    public void Init(PlayerData playerData)
    {
        this.text = this.GetComponent<TextMeshProUGUI>();
        this.playerData = playerData;
        this.playerData.RegisterForPlayerNameChanged(OnPlayerNameChanged);
    }

    private void OnDestroy()
    {
        this.playerData?.UnregisterFromPlayerNameChanged(OnPlayerNameChanged);
    }


//*====================
//* CALLBACKS
//*====================    
    private void OnPlayerNameChanged(string obj)
    {
        this.text.text = obj;
    }
}