using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private static int levelNum = 0;
    private static bool transitioning;

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
        transitioning = true;
        Application.targetFrameRate = 60; // Cap frame rate
    }

    void Update()
    {
        if (transitioning && levelNum == 0)
        {
            Debug.Log("blurry activated");
            FilterControl.depthOfField.active = true;
            transitioning = false;
        }
        if (transitioning)
        {
            StartCoroutine(BlinkAndDoEffects());
            transitioning = false;
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
    public static void transition()
    {
        transitioning = true;
    }

    private IEnumerator BlinkAndDoEffects()
    {
        yield return StartCoroutine(Blink.BlinkNow());
    }

    public static void level1Filter()
    {
        Debug.Log("blurry deactivated");
        FilterControl.depthOfField.active = false;

        FilterControl.colorAdjust.active = true;
        Debug.Log("blue activated");
    }
    public static void level2Filter()
    {
        FilterControl.vignette.active = true;
        Debug.Log("vignette activated");

        FilterControl.colorAdjust.active = false;
        Debug.Log("blue deactivated");
    }

}