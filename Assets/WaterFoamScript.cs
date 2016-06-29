using UnityEngine;
using System.Collections;

public class WaterFoamScript : MonoBehaviour {

	Vector3			TargetPos;
	Vector3			StartPos;
	float			MaxX;
	float			MinX;

	bool			GoingBack = false;

	// Use this for initialization
	void OnEnable () {
		StartPos = transform.position;
		TargetPos = transform.position;
		TargetPos.x += 3.0F;
	}
	
	// Update is called once per frame
	void Update () {
		if (GoingBack == false) {
			transform.position = Vector3.Lerp (transform.position, TargetPos, 0.05F);
			if ((TargetPos - transform.position).sqrMagnitude < 0.1F)
			{
				GoingBack = true;
			}
		} else {
			transform.position = Vector3.Lerp (transform.position, StartPos, 0.05F);
			if ((StartPos - transform.position).sqrMagnitude < 0.1F)
			{
				GoingBack = false;
			}
		}

	}
}
