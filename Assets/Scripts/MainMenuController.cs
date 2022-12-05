using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void LoadFox()
    {
        SceneManager.LoadScene("FoxGrapes");
    }
    public void LoadTortoise()
    {
        SceneManager.LoadScene("TortoiseHare");
    }
    public void LoadMouse()
    {
        SceneManager.LoadScene("MouseLion");
    }
    public void LoadWolf()
    {
        SceneManager.LoadScene("WolfSheep");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
