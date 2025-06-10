using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour
{
    private Transform eyelid;
    public float slideDuration = 0.25f;
    private static Blink _instance;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    private void Awake()
    {
        _instance = this;

        eyelid = this.transform;

        openPosition = eyelid.position;
        closedPosition = new Vector3(openPosition.x, -0f, openPosition.z); // Adjust Y as needed
        Debug.Log("eyelid position: " + eyelid.position);
    }

    public static IEnumerator BlinkNow()
    {
        if (_instance != null)
        {
            yield return _instance.StartCoroutine(_instance.DoBlink());
        }
        else
        {
            Debug.LogWarning("Blink instance is null");
        }

        Debug.Log("blinked");
    }

    private IEnumerator DoBlink()
    {
        // Slide down
        yield return SlideEyelid(openPosition, closedPosition);

        // Wait briefly
        yield return new WaitForSeconds(0.1f);

        int level = GameManager.getLevelNum();
        switch (level)
        {
            case 1: GameManager.level1Filter(); break;
            case 2: GameManager.level2Filter(); break;
            case 3: GameManager.level3Filter(); break;
            case 4: GameManager.level4Filter(); break;
            case 5: GameManager.level5Filter(); break;
            case 6: GameManager.level6Filter(); break;
        }

        // Slide up
        yield return SlideEyelid(closedPosition, openPosition);
    }

    private IEnumerator SlideEyelid(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            eyelid.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        eyelid.position = to;
    }
}
