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
        this.playerData.RegisterForPlayerNameChanged(OnPlayerNameChanged, false);
        this.playerData.RegisterForPlayerScoreChanged(OnPlayerScoreChanged);
    }

    private void OnDestroy()
    {
        this.playerData?.UnregisterFromPlayerNameChanged(OnPlayerNameChanged);
        this.playerData?.UnregisterFromPlayerScoreChanged(OnPlayerScoreChanged);
    }

//*====================
//* CALLBACKS
//*====================    
    private void OnPlayerNameChanged(string obj)
    {
        this.ReevaluateText();
    }

    private void OnPlayerScoreChanged(int obj)
    {
        this.ReevaluateText();
    }


//*====================
//* PRIVATE
//*====================
    private void ReevaluateText()
    {
        this.text.text = $"{this.playerData.PlayerName}: {this.playerData.PlayerScore}";
    }
}