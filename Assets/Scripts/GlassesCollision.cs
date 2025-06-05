using UnityEngine;

public class level1glasses : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // on collison with the rig escalate
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            GameManager.nextLevel();
            Debug.Log("level: " + GameManager.getLevelNum());
        }
    }
}
