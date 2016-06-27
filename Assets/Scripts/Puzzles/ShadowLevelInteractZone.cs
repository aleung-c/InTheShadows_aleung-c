using UnityEngine;
using System.Collections;

public class ShadowLevelInteractZone : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player") {
			other.gameObject.GetComponent<AdventurePlayer>().IsInPuzzleInteractZone = true;
			other.gameObject.GetComponent<AdventurePlayer>().CollidingPuzzle = transform.parent.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.name == "Player") {
			other.gameObject.GetComponent<AdventurePlayer>().IsInPuzzleInteractZone = false;
			other.gameObject.GetComponent<AdventurePlayer>().CollidingPuzzle = null;
		}
	}
}
