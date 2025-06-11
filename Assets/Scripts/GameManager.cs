using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    // Layers we will hide
    private static GameObject Beach;
    private static GameObject Monster;
    private static GameObject Fire;
    private static GameObject LODLayer;
    private static GameObject CoatRack;

    // Glasses hidden until used
    private static GameObject SunGlasses;
    private static GameObject LightGlasses;
    private static GameObject StrongGlasses;
    private static GameObject LODGlasses;
    private static GameObject FireGlasses;

    // Objects for LOD
    private static GameObject Table;
    private static GameObject Lamp;
    private static GameObject pens;
    private static GameObject PhotoFrame;
    private static GameObject PhotoFrame2;
    private static GameObject Dresser;
    private static GameObject Lamp1;
    private static GameObject Nightstand;
    private static GameObject Trophy;
    private static GameObject cup1;


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
        // Get all the hidden objects and hide them
        Beach = GameObject.Find("Beach");
        Monster = GameObject.Find("Monster");
        Fire = GameObject.Find("Fire");
        LODLayer = GameObject.Find("LOD");
        CoatRack = GameObject.Find("CoatRack");
        Table = GameObject.Find("Furniture/Table");
        Dresser = GameObject.Find("Furniture/Dresser");
        Nightstand = GameObject.Find("Furniture/Nightstand");

        Lamp = GameObject.Find("Lamp");
        pens = GameObject.Find("pens");
        PhotoFrame = GameObject.Find("PhotoFrame");
        PhotoFrame2 = GameObject.Find("PhotoFrame2");
        Lamp1 = GameObject.Find("Lamp1");
        Trophy = GameObject.Find("Trophy");
        cup1 = GameObject.Find("cup1");

        Beach.SetActive(false);
        Monster.SetActive(false);
        Fire.SetActive(false);
        LODLayer.SetActive(false);

        // Get all the glasses and hide them
        SunGlasses = GameObject.Find("/Glasses/SunGlasses");
        LightGlasses = GameObject.Find("/Glasses/HeadlightGlasses");
        StrongGlasses = GameObject.Find("/Glasses/StrongGlasses");
        LODGlasses = GameObject.Find("/Glasses/Drawer/CubeGlasses");
        FireGlasses = GameObject.Find("/Glasses/FireGlasses");

        LightGlasses.SetActive(false);
        StrongGlasses.SetActive(false);
        LODGlasses.SetActive(false);
        FireGlasses.SetActive(false);

        // Start with a blink
        transitioning = true;
        Application.targetFrameRate = 60; // Cap frame rate
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(BlinkAndDoEffects());
            nextLevel();
        }
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
        SunGlasses.SetActive(false);
        LightGlasses.SetActive(true);
    }
    public static void level2Filter()
    {
        Debug.Log("level 2 time");
        FilterControl.vignette.active = true;
        // adjust the vignette heavily
        FilterControl.colorAdjust.active = false;
        // Turn on headlight
        // Disable lights
        // Turn off ambient
        LightGlasses.SetActive(false);
        LODGlasses.SetActive(true);
    }
    public static void level3Filter()
    {
        Debug.Log("level 3 time");

        FilterControl.vignette.active = false;
        // Debug.Log("vignette deactivated");
        // Debug.Log("lod on!");
        LODGlasses.SetActive(false);
        StrongGlasses.SetActive(true);
        LODLayer.SetActive(true);
        Table.GetComponent<LODGroup>().enabled = true;
        Lamp.GetComponent<LODGroup>().enabled = true;
        pens.GetComponent<LODGroup>().enabled = true;
        PhotoFrame.GetComponent<LODGroup>().enabled = true;
        PhotoFrame2.GetComponent<LODGroup>().enabled = true;
        Dresser.GetComponent<LODGroup>().enabled = true;
        Lamp1.GetComponent<LODGroup>().enabled = true;
        Nightstand.GetComponent<LODGroup>().enabled = true;
        Trophy.GetComponent<LODGroup>().enabled = true;
        cup1.GetComponent<LODGroup>().enabled = true;

    }

    public static void level4Filter()
    {
        Debug.Log("level 4 time");
        LODLayer.SetActive(false);
        GameObject clothes = GameObject.Find("Decor/clothes");
        //GameObject spheretuah = GameObject.Find("Sphere2");
        clothes.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
        //spheretuah.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;

        Table.GetComponent<LODGroup>().enabled = false;
        Lamp.GetComponent<LODGroup>().enabled = false;
        pens.GetComponent<LODGroup>().enabled = false;
        PhotoFrame.GetComponent<LODGroup>().enabled = false;
        PhotoFrame2.GetComponent<LODGroup>().enabled = false;
        Dresser.GetComponent<LODGroup>().enabled = false;
        Lamp1.GetComponent<LODGroup>().enabled = false;
        Nightstand.GetComponent<LODGroup>().enabled = false;
        Trophy.GetComponent<LODGroup>().enabled = false;
        cup1.GetComponent<LODGroup>().enabled = false;

        StrongGlasses.SetActive(false);
        FireGlasses.SetActive(true);
    }

    public static void level5Filter()
    {
        Debug.Log("level 5 time");
        FilterControl.filmGrain.active = true;
        // Debug.Log("fire filter on + monster here");
        FireGlasses.SetActive(false);
        Fire.SetActive(true);
        Monster.SetActive(true);
        CoatRack.SetActive(false);
    }
    public static void level6Filter()
    {
        Debug.Log("level 6 time");
        FilterControl.filmGrain.active = false;

        Beach.SetActive(true);
        Fire.SetActive(false);
        //Monster.SetActive(false);

        //disable: floor, scenedirector/plane, piles of clothes, flame effect

    }
    public static void normalVision()
    {
        Debug.Log("back to normal");
        FilterControl.filmGrain.active = false;

        Beach.SetActive(false);
        Fire.SetActive(false);
        
        Monster.SetActive(false);
        CoatRack.SetActive(true);
        
        //disable: floor, scenedirector/plane, piles of clothes, flame effect

    }
}