using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene"); // 切换到游戏主场景
    }
}

