using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour
{
    public RectTransform eyelid; // Drag the UI Eyelid object here
    public float slideDuration = 0.25f; // Duration of slide in/out

    private static Blink _instance;

    private void Awake()
    {
        _instance = this;
        GameObject eyelidObj = this.gameObject;
        eyelid = eyelidObj.GetComponent<RectTransform>();
        Debug.Log("eyelid " + eyelid);
    }

    public static void BlinkNow()
    {
        if (_instance != null)
        {
            _instance.StartCoroutine(_instance.DoBlink());
        }
        Debug.Log("blinked");
    }

    private IEnumerator DoBlink()
    {
        // eyelid.gameObject.SetActive(true);

        Vector2 startPos = new Vector2(0, eyelid.rect.height);
        Vector2 midPos = Vector2.zero;
        Vector2 endPos = new Vector2(0, eyelid.rect.height);

        yield return SlideEyelid(startPos, midPos);

        yield return new WaitForSeconds(0.1f);

        yield return SlideEyelid(midPos, startPos);

        // eyelid.gameObject.SetActive(false);
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
        eyelid.anchoredPosition = to;
    }
}
