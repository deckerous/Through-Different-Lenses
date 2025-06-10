using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
// Controls the post processing Filters for glasses
public class FilterControl : MonoBehaviour
{
    public Volume volume;
    public static ColorAdjustments colorAdjust;
    public static DepthOfField depthOfField;
    private FilmGrain filmGrain;
    private Vignette vignette;

    void Start()
    {
        //Grab filter values
        volume.profile.TryGet<ColorAdjustments>(out colorAdjust);
        volume.profile.TryGet<DepthOfField>(out depthOfField);
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        volume.profile.TryGet<Vignette>(out vignette);

        //Disable all filters
        colorAdjust.active = false;
        depthOfField.active = false;
        filmGrain.active = false;
        vignette.active = false;

    }

    void Update()
    {
    
        //toggle depth of field blurr enable on control press
        if (Keyboard.current.ctrlKey.isPressed)
        {
            depthOfField.active = !depthOfField.active;
        }

        //toggle red filter on shift press
        if (Keyboard.current.shiftKey.isPressed)
        {
            colorAdjust.active = !colorAdjust.active;
        }

        //toggle grainy filter on alt press
        if (Keyboard.current.altKey.isPressed)
        {
            filmGrain.active = !filmGrain.active;
        }

        //toggle vignette on backspace press
        if (Keyboard.current.backspaceKey.isPressed)
        {
            vignette.active = !vignette.active;
        }
    }
}
