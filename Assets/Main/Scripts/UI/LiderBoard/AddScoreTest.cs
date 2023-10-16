using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

public class AddScoreTest : MonoBehaviour
{
    public void OnCLick()
    {
        Leaderboard.GetPlayerEntry("Score", (result) =>
        {
            Debug.Log("@@@Записать 100 очков");

            if (result == null)
            {
                Debug.Log("");
                return;
            }
            else
            {
                Debug.Log($"@@@ result {result.score + 100}");
                Leaderboard.SetScore("Score", result.score + 100);
            }
        });
    }
}
