using UnityEngine;
using System.Collections;

public class Flotar : MonoBehaviour {
	//0 arriba
	//1 abajo

	private Transform m_Transform;
	private float yini;
	private double yfin;
	private bool dir;
	float i;

	// Use this for initialization
	void Start () {
		m_Transform = GetComponent<Transform>();
		yini = m_Transform.position.y;
		yfin = yini - 0.5;
		dir = true;
		i = 0.3f;
	}

	// Update is called once per frame
	void Update () {
		if (m_Transform.position.y >= yini) {
			dir = true;
		} else if (m_Transform.position.y <= yfin) {
			dir = false;
		}

		m_Transform.Rotate(30 * Vector3.forward * Time.deltaTime);

		if (dir) {
			m_Transform.Translate (Vector3.back * Time.deltaTime * i);
		} else {
			m_Transform.Translate (Vector3.forward * Time.deltaTime * i);
		}
	}
}
