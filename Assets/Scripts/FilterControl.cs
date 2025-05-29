using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
// Controls the post processing Filters for glasses
public class FilterControl : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjust;
    private DepthOfField depthOfField;
    void Start()
    {
        //Grab filter values
        volume.profile.TryGet<ColorAdjustments>(out colorAdjust);
        Debug.Log("colorAdjust: " + colorAdjust);
        volume.profile.TryGet<DepthOfField>(out depthOfField);
        Debug.Log("DOF: " + depthOfField);

        //Disable all filters
        colorAdjust.active = false;
        depthOfField.active = false;
    }

    void Update()
    {
        
        if (Keyboard.current.ctrlKey.isPressed)
        {
            depthOfField.active = true;
        }
    }
}
