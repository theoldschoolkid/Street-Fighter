using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic script to deactivate objects after 2 secs
public class DeactivateGameObject : MonoBehaviour {

    public float timer = 2f;

    
    void Start() {
        Invoke("DeactivateAfterTime", timer);
    }

    void DeactivateAfterTime() {
        Destroy(this.gameObject);
    }

}
