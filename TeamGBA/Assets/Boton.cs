using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Boton : MonoBehaviour {

	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		prevState = state;
		state = GamePad.GetState(playerIndex);

		if ((prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) || Input.GetKey ("space")) {//(Input.GetButtonDown ("Fire1")) {
			Invoke("CargarVolverAlMenu", 1);
		}
	}
}
