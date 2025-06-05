using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private static int levelNum = 0;

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
            // blur (no glasses wakeup)
            Debug.Log("blurry activated");
            FilterControl.depthOfField.active = true;
        }
        else if (levelNum == 1)
        {
            // Turn on color filter and turn off blur
            Debug.Log("blurry deactivated");
            FilterControl.depthOfField.active = false;

            FilterControl.colorAdjust.active = true;
            Debug.Log("blue activated");
        }
        else if (levelNum == 2)
        {
            //TODO: Turn on vignette and turn off color
            FilterControl.colorAdjust.active = false;
            Debug.Log("blue deactivated");
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

    public static void nextLevel()
    {
        levelNum++;
    }
    public static int getLevelNum()
    {
        return levelNum;
    }

}