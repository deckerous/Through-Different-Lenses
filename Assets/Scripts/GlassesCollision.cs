using UnityEngine;

public class Glasses : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // on collison with the rig escalate
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            GameManager.nextLevel();
            Debug.Log("level: " + GameManager.getLevelNum());
            gameObject.SetActive(false); // get rid of gameobject once collided 
            GameManager.transition();
        }
    }
}
