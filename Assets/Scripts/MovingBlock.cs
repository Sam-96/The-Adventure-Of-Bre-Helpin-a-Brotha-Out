using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

	public Transform movingBlock;

	public Transform position1;

	public Transform position2;

	public Vector3 newPosition;

	public string currentState;

	public float smooth;

	public float resetTime;


	// Use this for initialization
	void Start () {
		ChangeTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void fixedUpdate () {
		movingBlock.position = Vector3.Lerp (movingBlock.position, newPosition, smooth * Time.deltaTime);
	}

	void ChangeTarget(){
		if (currentState == "Moving to Position 1") {
			currentState = "Moving to Position 2";
			newPosition = position2.position;
		} else if (currentState == "Moving to Position 2") {
			currentState = "Moving to Position 1";
			newPosition = position1.position;
		} else if (currentState == "") {
			currentState = "Moving to Position 2";
			newPosition = position2.position;
		}
		Invoke ("ChangeTarget", resetTime);
	}

}
