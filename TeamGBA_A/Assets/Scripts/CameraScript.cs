using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public float distanceX;
	public float distanceY;
	public float distanceZ;

	// Use this for initializations
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x-distanceX, target.position.y-distanceY, target.position.z - distanceZ);
	}
}
