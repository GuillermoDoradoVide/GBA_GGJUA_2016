using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public Vector2 ForceMultiplier;

	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	private Transform m_Transform;

	float speed = 2.5f;
	float angle = 0.0f;
	Vector3 cross;


	Rigidbody[] rigidbodys;

	// Use this for initialization
	void Start () {
		m_Transform = GetComponent<Transform>();
		rigidbodys = this.GetComponentsInChildren<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Boton pulsado 1");
		}
		if (Input.GetButtonDown ("Fire2")) {
			Debug.Log ("Boton pulsado 2");
		}
		if (Input.GetButtonDown ("Fire3")) {
			Debug.Log ("Boton pulsado 3");
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

		// Detect if a button was pressed this frame
		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
		{
			GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);

			for (int i = 0; i < rigidbodys.Length; i++) {
				Rigidbody rb = rigidbodys [i];
				if (rb) {
					//if (i == 1) {
					Vector3 f = new Vector3 (1.0f, 1.0f * ForceMultiplier.y, 1.0f);
						//if (!onGround && f.y > 0)
						//	f.y = 0;
						rb.AddForce (f);
					//}
				}
			}



			//m_Transform.position += transform.up * Time.deltaTime * 10.0f;

		}
		// Detect if a button was released this frame
		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
		{
			GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}

		// Set vibration according to triggers
		GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

		// Make the current object turn
		m_Transform.Translate(Vector3.forward * Time.deltaTime * state.ThumbSticks.Left.Y * speed);
		m_Transform.Translate(Vector3.right * Time.deltaTime * state.ThumbSticks.Left.X * speed);

//		for (int i = 0; i < rigidbodys.Length; i++) {
//			Rigidbody rb = rigidbodys [i];
//			if (rb) {
//				
//				float moveHorizontal = state.ThumbSticks.Left.X * 25.0f;
//				float moveVertical = state.ThumbSticks.Left.Y * 25.0f;
//
//				Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
//
//				rb.AddForce (movement * speed);
//			}
//		}
		/////////PRUEBAS
		float valor = 0;

		//state.ThumbSticks.Left.X;
		//state.ThumbSticks.Left.Y;

		Vector3 destino = new Vector3 (state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 1.0f);
		Vector3 origen = new Vector3 (0.0f, 0.0f, 1.0f);

		//float result = state.ThumbSticks.Left.X;

		Debug.Log(Mathf.Asin(state.ThumbSticks.Left.X));

		angle = Vector3.Angle(destino, origen);
		cross = Vector3.Cross(destino, origen);

		if (cross.y < 0)
			angle = -angle;

		transform.Rotate (0, state.ThumbSticks.Right.X * 50.0f * Time.deltaTime, 0);


	}
}
