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
            FilterControl.depthOfField.active = true;
            transitioning = false;
        }
        if (transitioning)   // triggered in the GlassesCollision Script 
        {
            StartCoroutine(BlinkAndDoEffects()); // then calls the async method that blinks
            transitioning = false;
        }
    }

    public static void nextLevel()
    {
        levelNum++;
        transition();
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
        yield return StartCoroutine(Blink.BlinkNow()); // calls blinknow in Blink.cs which has logic for level handling and then the filters are applied below
    }

    public static void level1Filter()
    {
        Debug.Log("level 1 time");
        FilterControl.depthOfField.active = false;

        FilterControl.colorAdjust.active = true;
    }
    public static void level2Filter()
    {
        Debug.Log("level 2 time");
        FilterControl.vignette.active = true;
        // adjust the vignette heavily
        FilterControl.colorAdjust.active = false;
    }
    public static void level3Filter()
    {
        Debug.Log("level 3 time");

        FilterControl.vignette.active = false;
        // Debug.Log("vignette deactivated");
        // Debug.Log("lod on!");
        //TODO LOD
    }

    public static void level4Filter()
    {
        Debug.Log("level 4 time");


        GameObject clothes = GameObject.Find("Decor/clothes");
        //GameObject spheretuah = GameObject.Find("Sphere2");
        clothes.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
        //spheretuah.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;

    }

    public static void level5Filter()
    {
        Debug.Log("level 5 time");
        FilterControl.filmGrain.active = true;

        // Debug.Log("fire filter on + monster here");

    }
    public static void level6Filter()
    {
        Debug.Log("level 6 time");
        FilterControl.filmGrain.active = false;

        // Debug.Log("tiki filter on + monster gone");
        GameObject.Find("Monster").SetActive(false);
        GameObject.Find("House/floor").SetActive(false);
        GameObject.Find("Monster").SetActive(false);
        GameObject.Find("Decor/clothes").SetActive(false);
        GameObject.Find("Decor/clothes (1)").SetActive(false);
        GameObject.Find("Beach").SetActive(true);

        //disable: floor, scenedirector/plane, piles of clothes, flame effect

    }
}