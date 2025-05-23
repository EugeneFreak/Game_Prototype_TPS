using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities 
{
    public static int playerDeaths = 0;

    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "Next time you will be at number " + countReference;

	}

    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Debug.Log($"Player death: {playerDeaths}");
        string message = UpdateDeathCount(ref playerDeaths);
        Debug.Log($"Player death: {playerDeaths}");
    }

    public static bool RestartLevel(int sceneIndex)
    {
        if (sceneIndex < 0)
        {
            throw new System.AggregateException("Scvene undex cannot be negative");
        }
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;

        return true;
    }
}
