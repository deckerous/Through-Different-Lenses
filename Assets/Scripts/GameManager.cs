using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { countDown, running, raceOver };

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int levelNum = 1; 

    GameStates gameState = GameStates.countDown;

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
        PlayerData.levelToLoad = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        Application.targetFrameRate = 60; // Cap frame rate
    }

    void Update()
    {
        if (isTimerRunning)
        {
            raceTimer += Time.deltaTime; // Increment timer
        }
    }

    void LevelStart()
    {

        Debug.Log("Level Started");
    }

    public GameStates GetGameState()
    {
        return gameState;
    }

    public void OnRaceStart()
    {
        Debug.Log("OnRaceStart");
    }

    public void OnRaceEnd()
    {
        Debug.Log("OnRaceEnd");

    }

    public float GetRaceTime()
    {
        return raceTimer;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }

}