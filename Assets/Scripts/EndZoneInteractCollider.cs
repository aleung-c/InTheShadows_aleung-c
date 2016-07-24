using UnityEngine;
using System.Collections;

public class EndZoneInteractCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<AdventurePlayer>().IsInEndZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<AdventurePlayer>().IsInEndZone = false;
        }
    }
}
