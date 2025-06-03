using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int levelNum = 1; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = 60; // Cap frame rate
    }

    void Update()
    {
        if (levelNum == 0)
        {
            //TODO: Logic for blur (no glasses wakeup)
        }
        else if (levelNum == 1)
        {
            //TODO: Turn on color filter and turn off blur
        }
        else if (levelNum == 2)
        {
            //TODO: Turn on vignette and turn off color
        }
        else if (levelNum == 3)
        {
            //TODO: Turn on "Glasses LOD" and turn off vignette
        }
        else if (levelNum == 4)
        {
            //TODO: Turn on Superstrength and red turn off LOF
        }
        else if (levelNum == 5)
        {
            //TODO: Turn on fire effects and noise and monster appear
        }
        else if (levelNum == 6)
        {
            //TODO: Turn on tiki theme (find the drink)
        }
        else
        {
            //TODO: Normal vision
        }
    }

    public void nextLevel()
    {
        levelNum++;
    }

}