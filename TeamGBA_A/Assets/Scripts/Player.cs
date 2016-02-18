using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour {

	public Vector2 ForceMultiplier;
	public Animator animator;
	public float speed = 2.5f;

	public ParticleSystem picked;
	
	float vida = 100.0f;
	float movHorizontal = 0.0f;
	float movVertical = 0.0f;

	bool playerIndexSet = false;
	bool siendoDanyado = false;
	bool esNoche = false;
	bool onGround = true;

	float r_aux;
	float g_aux;
	float b_aux;
	float a_aux;

	int numReliquias;

	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	Rigidbody rb;

	private Transform m_Transform;

	Vector3 cross;

	Rigidbody[] rigidbodys;

	// Use this for initialization
	void Start () {

		m_Transform = GetComponent<Transform>();
		rigidbodys = this.GetComponentsInChildren<Rigidbody> ();
		animator = GetComponent<Animator>();
		numReliquias = 0;
		picked.Stop ();

		//Debug.Log(GetComponent<Renderer> ().material.color);
		//GameObject.Find ("Arma").GetComponent<Animator> ().Stop ();
	}

	// Update is called once per frame
	void Update () {

		if (picked.isPlaying) {


			Debug.Log ("sep");

		}

		animator.SetBool("jump", onGround);

		if ((prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) || Input.GetKey ("space")) {//(Input.GetButtonDown ("Fire1")) {
			if (esNoche) {
				//Atacar
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("attack") == false) {
					animator.SetTrigger ("att");
					GameObject.Find ("ColliderArma").GetComponent<Transform> ().position = new Vector3 (GameObject.Find ("ColliderArma").GetComponent<Transform> ().position.x, 3.0f, GameObject.Find ("ColliderArma").GetComponent<Transform> ().position.z);
				}

			} else if (onGround == true) {
				//Saltar
				onGround = false;

				for (int i = 0; i < rigidbodys.Length; i++) {
					rb = rigidbodys [i];
					if (rb) {
						Vector3 f = new Vector3 (1.0f, 1.4f * ForceMultiplier.y, 1.0f);
						rb.AddForce (f);
					}
				}
			}
		} else if (state.Buttons.A == ButtonState.Released || !Input.GetKey ("space")) {
			GameObject.Find ("ColliderArma").GetComponent<Transform> ().position = new Vector3 (GameObject.Find ("ColliderArma").GetComponent<Transform> ().position.x, 7.0f, GameObject.Find ("ColliderArma").GetComponent<Transform> ().position.z);
		}

		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}

		prevState = state;
		state = GamePad.GetState(playerIndex);

		// Set vibration according to triggers
		GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

		movHorizontal = state.ThumbSticks.Left.X;
		movVertical = state.ThumbSticks.Left.Y;

		if (Input.GetKey ("a")) {
			movHorizontal = -1;
		}
		if (Input.GetKey ("d")) {
			movHorizontal = 1;
		}
		if (Input.GetKey ("s")) {
			movVertical = -1;
		}
		if (Input.GetKey ("w")) {
			movVertical = 1;
		}

		animator.SetFloat("velocity", Math.Abs(movVertical) + Math.Abs(movHorizontal));

		m_Transform.Translate(Time.deltaTime * movHorizontal * speed, 0.0f, Time.deltaTime * movVertical * speed, Space.World);
		//m_Transform.localRotation = Quaternion.Euler (0.0f, (Mathf.Atan2 (movVertical, -movHorizontal) * Mathf.Rad2Deg), 0.0f);

		if (!(movVertical == 0.0f && movHorizontal == 0.0f)) {
			m_Transform.localRotation = Quaternion.Euler (0.0f, (Mathf.Atan2 (movVertical, -movHorizontal) * Mathf.Rad2Deg), 0.0f);
			if(!GetComponent<AudioSource>().isPlaying && onGround)
				GetComponent<AudioSource>().Play();
			//m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, 100.0f);
		}

		if (siendoDanyado == true) {
			if (GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.r < 0.8f)
				r_aux = GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.r + 0.05f;
			if (GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.g < 0.8f)
				g_aux = GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.g + 0.05f;
			if (GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.b < 0.8f)
				b_aux = GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.b + 0.05f;
			if (GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.a < 1.0f)
				a_aux = GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color.a + 0.05f;
			
			GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color = new Color(r_aux, g_aux, b_aux, a_aux);
		}

	}

	void OnCollisionEnter(Collision other) {

		if (other.gameObject.tag.Equals ("Pickeable")) {

			picked.Clear ();
			picked.Simulate (1.0f);
			picked.Play ();

			string text = "Item " + other.gameObject.name.Split (' ') [1];

			Image image;
			image = GameObject.Find (text).GetComponent<Image> ();
				
			Color c = image.color;
			c.a = 1.0f;
			image.color = c;
				
			Destroy (other.gameObject);

			numReliquias++;

			if(numReliquias == 5){
				Application.LoadLevel("MainMenu");
			}

		} else {
			onGround = true;
		}
	}

	public void cambiarPersonajeNoche(){
		//GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		esNoche = true;
	}

	public void cambiarPersonajeDia(){
		//GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		esNoche = false;
	}

	public int recibirAtaque(){
		GameObject.Find ("CubePlayer").GetComponent<SkinnedMeshRenderer>().material.color = new Color(0.8f, 0.0f, 0.0f, 0.5f);
		siendoDanyado = true;

		vida = vida - 5.0f;

		GameObject.Find ("Barra").GetComponent<Transform> ().localScale = new Vector3((1.3f*vida)/100.0f, GameObject.Find ("Barra").GetComponent<Transform> ().localScale.y, GameObject.Find ("Barra").GetComponent<Transform> ().localScale.z);

		if (vida <= 0.0f) {
			return 1;
		} else {
			return 0;
		}
	}
}
