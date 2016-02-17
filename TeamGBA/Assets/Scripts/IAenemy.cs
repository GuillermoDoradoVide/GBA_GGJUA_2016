using UnityEngine;
using System.Collections;
using System.Collections;
using System;

public class IAenemy : MonoBehaviour {

	private Player p;
	private int Currentpoint;
	public Transform[] patrol;
	public Animator animator;
	public float moveSpeed = 1.0f; //move speed

	Transform target; //the enemy's target
	float rotationSpeed = 3.0f; //speed of turning

	float MaxDist = 5.0f;
	float MinDist = 1.5f;

	float tiempoAtaque;
	double decimalValue;

	float vida = 100.0f;

	bool siendoDanyado = false;

	float r_aux;
	float g_aux;
	float b_aux;
	float a_aux;

	Vector3 here;
	Vector3 pos;

	Transform myTransform; //current transform data of this enemy

	void Awake() {
		myTransform = transform; //cache transform data for easy access/preformance
	}

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").transform; //target the player
		p = GameObject.Find ("Player").GetComponent<Player>();
		MaxDist = 10.0f;
		MinDist = 4.0f;

		tiempoAtaque = 1.0f;

		myTransform.position = patrol[0].position;
		Currentpoint = 0;

		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("GameObject").GetComponent<Contador> ().getEsNoche ()) {
			
			if (tiempoAtaque > 0) {
				tiempoAtaque -= Time.deltaTime;
				decimalValue = Math.Ceiling (tiempoAtaque);
			} else {
				tiempoAtaque = 1.0f;
			}

			if (Vector3.Distance (myTransform.position, target.position) <= MaxDist) {
				
				myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

				here = myTransform.position;
				pos = target.position;

				Debug.DrawLine (here, pos, Color.red);

				if (Physics.Linecast (here, pos)) {
					Debug.DrawLine (here, pos, Color.red);
				}
				
				if (Vector3.Distance (myTransform.position, target.position) <= MinDist) {
					// Atacar
					if (decimalValue == 0) {
						animator.SetTrigger("attack");
						if (p.recibirAtaque () == 1) {
							// El player ha muerto
							Application.LoadLevel("MainMenu");
						}
					}
				} 
			} else {
				// No esta en el rango: patrullas
				if(Vector3.Distance(myTransform.position, patrol[Currentpoint].position) < 0.5f) {
					Currentpoint++;
				}

				if(Currentpoint >= patrol.Length)
				{
					Currentpoint = 0;
				}
				
				myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (patrol [Currentpoint].position - myTransform.position), rotationSpeed * Time.deltaTime);
				myTransform.position = Vector3.MoveTowards (myTransform.position, patrol [Currentpoint].position, moveSpeed * Time.deltaTime); 
			}
				
		} else {
			// Es de noche. Los enemigos no hacen nada.
		}

		if (siendoDanyado == true) {

			if (GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.r > 0.8f)
				r_aux = GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.r - 0.05f;
			if (GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.g < 0.8f)
				g_aux = GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.g + 0.05f;
			if (GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.b < 0.8f)
				b_aux = GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.b + 0.05f;
			if (GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.a < 1.0f)
				a_aux = GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color.a + 0.05f;

			GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color = new Color(r_aux, g_aux, b_aux, a_aux);
		}

		animator.SetBool("seMueve", GameObject.Find ("GameObject").GetComponent<Contador> ().getEsNoche ());
	} 

	public int recibirAtaque(){

		GameObject.Find ("criatura").GetComponent<SkinnedMeshRenderer>().material.color = new Color(0.95f, 0.0f, 0.0f, 0.5f);


		siendoDanyado = true;
		vida = vida - 20.0f;

		Debug.Log ("Recibe ataque, vida " + vida);

		if (vida <= 0) {
			return 1;
		} else {
			return 0;
		}
	}
}