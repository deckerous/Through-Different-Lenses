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
    }

    public static void BlinkNow()
    {
        if (_instance != null)
        {
            _instance.StartCoroutine(_instance.DoBlink());
        }
    }

    private IEnumerator DoBlink()
    {
        eyelid.gameObject.SetActive(true);

        Vector2 startPos = new Vector2(0, eyelid.rect.height);
        Vector2 midPos = Vector2.zero;
        Vector2 endPos = new Vector2(0, eyelid.rect.height);

        // Slide in
        yield return SlideEyelid(startPos, midPos);

        // Pause while "eyes are closed"
        yield return new WaitForSeconds(0.1f);

        // Slide out
        yield return SlideEyelid(midPos, endPos);

        eyelid.gameObject.SetActive(false);
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
