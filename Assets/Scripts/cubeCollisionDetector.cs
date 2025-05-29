using UnityEngine;

public class cubeCollisionDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("cube: " + gameObject.name + " has collided with " + other.gameObject.name);
        if (gameObject.name == "BlurCube" && other.gameObject.name == "XR Origin (XR Rig)")
        {
            if (!FilterControl.depthOfField.active)
            {
                Debug.Log("blurry activated");
                FilterControl.depthOfField.active = true;
            }
            else
            {
                Debug.Log("blurry deactivated");
                FilterControl.depthOfField.active = false;
            }
        }
            else if (gameObject.name == "BlueCube" && other.gameObject.name == "XR Origin (XR Rig)")
            {
                if (!FilterControl.colorAdjust.active)
                {
                    FilterControl.colorAdjust.active = true;
                    Debug.Log("blue activated");
                }
                else
                {
                    FilterControl.colorAdjust.active = false;
                    Debug.Log("blue deactivated");    
                }

            }

    }
}
