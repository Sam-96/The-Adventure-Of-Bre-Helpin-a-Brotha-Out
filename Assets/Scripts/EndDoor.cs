using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor: MonoBehaviour {

	public Transform movingBlock;

	public Transform position1;

	public float smooth;

	private int gemsCollected;

	private PlayerController gemScript;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.FindGameObjectWithTag ("Player");
		if (obj != null) {
			gemScript = obj.GetComponent<PlayerController> ();
		}
		if (gemScript == null)
			Debug.Log ("Script was null");
		gemsCollected = 0;
	}

	// Update is called once per frame
	void Update () {
		gemsCollected = gemScript.getGems ();
	}

	void FixedUpdate () {
		if (gemsCollected == 10) {
			movingBlock.position = Vector3.Lerp (movingBlock.position, position1.position, smooth * Time.deltaTime);
		}
	}




}