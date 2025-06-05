using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour
{
    private RectTransform eyelid;
    public float slideDuration = 0.25f;
    private static Blink _instance;

    private void Awake()
    {
        _instance = this;

        // Find the RectTransform from the current GameObject
        GameObject eyelidObj = this.gameObject;
        eyelid = eyelidObj.GetComponent<RectTransform>();
        Debug.Log("eyelid " + eyelid);
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
        Vector2 startPos = new Vector2(0, eyelid.rect.height);
        Vector2 midPos = Vector2.zero;

        // Slide down
        yield return SlideEyelid(startPos, midPos);

        // Pause in middle (eyelid fully down) and process filters
        yield return new WaitForSeconds(0.1f);
        if (GameManager.getLevelNum() == 1)
        {
            GameManager.level1Filter();
        }
        else if (GameManager.getLevelNum() == 2)
        {
            GameManager.level2Filter();
        }

        // Slide up
            yield return SlideEyelid(midPos, startPos);
    }

    private IEnumerator SlideEyelid(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            eyelid.anchoredPosition = Vector2.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        eyelid.anchoredPosition = to; // Snap to final position
    }
}
