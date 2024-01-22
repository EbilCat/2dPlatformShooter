using System.Collections.Generic;
using UnityEngine;

public class ScoreTextSpawner : MonoBehaviour
{
    private Dictionary<PlayerData, ScoreText> scores = new Dictionary<PlayerData, ScoreText>();
    [SerializeField] private ScoreText scoreTextPrefab;


//*====================
//* UNITY
//*====================
    private void Awake()
    {
        for (int i = 0; i < PlayerData.allPlayers.Count; i++)
        {
            PlayerData playerData = PlayerData.allPlayers[i];
            this.OnPlayerAdded(playerData);
        }

        PlayerData.OnPlayerAdded += OnPlayerAdded;
        PlayerData.OnPlayerRemoved += OnPlayerRemoved;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < PlayerData.allPlayers.Count; i++)
        {
            PlayerData playerData = PlayerData.allPlayers[i];
            this.OnPlayerRemoved(playerData);
        }

        PlayerData.OnPlayerAdded -= OnPlayerAdded;
        PlayerData.OnPlayerRemoved -= OnPlayerRemoved;
    }


//*====================
//* CALLBACKS
//*====================
    private void OnPlayerAdded(PlayerData playerData)
    {
        ScoreText scoreText = Instantiate<ScoreText>(scoreTextPrefab);
        scoreText.transform.SetParent(this.transform);
        scoreText.transform.localPosition = Vector3.zero;
        scoreText.transform.localRotation = Quaternion.identity;
        scoreText.transform.localScale = Vector3.one;

        scoreText.Init(playerData);
        scores.Add(playerData, scoreText);
    }

    private void OnPlayerRemoved(PlayerData playerData)
    {
        ScoreText scoreText = scores[playerData];
        scores.Remove(playerData);
        Destroy(scoreText.gameObject);
    }
}