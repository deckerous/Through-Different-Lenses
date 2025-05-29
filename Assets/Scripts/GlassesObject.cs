using UnityEngine;

public class GlassesObject : MonoBehaviour
{
    public GameObject toReveal;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Glasses: " + gameObject.name + " has collided with " + other.gameObject.name);
        if (gameObject.name == "BlurGlasses" && other.gameObject.name == "XR Origin (XR Rig)")
        {
            if (!FilterControl.depthOfField.active)
            {
                Debug.Log("blurry activated");
                FilterControl.colorAdjust.active = false;
                FilterControl.depthOfField.active = true;
            }
            else
            {
                Debug.Log("blurry deactivated");
                FilterControl.depthOfField.active = false;
            }
        }
        else if (gameObject.name == "BlueGlasses" && other.gameObject.name == "XR Origin (XR Rig)")
        {
            if (!FilterControl.colorAdjust.active)
            {
                FilterControl.colorAdjust.active = true;
                FilterControl.depthOfField.active = false;
                Debug.Log("blue activated");
            }
            else
            {
                FilterControl.colorAdjust.active = false;
                Debug.Log("blue deactivated");    
            }

        }
        //gameObject.SetActive(false);
        toReveal.SetActive(true);
    }
}
