/*
https://unity3d.com/learn/tutorials/tic-tac-toe/restarting-game
See this link to restart the game without running back the app again
 */
using UnityEngine;
﻿using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using extOSC;

public class PlayerController : MonoBehaviour {
	public float speed; //public variables are accesible on the editor.
	public Text countText;
	public Text winText;
	private Rigidbody rb;
	private int count;
	public string pickupAddress;
	public string winAddress;
	public OSCTransmitter transmitter;



	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = Environment.UserName.ToString();
		print("UserName: " + Environment.UserName); //access user name
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
		}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Pick up"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
			//send osc message OnTriggerEnter
			var message = new OSCMessage(pickupAddress);
			message.AddValue(OSCValue.Int(1));
			transmitter.Send(message);
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if( count >= 8)
		{
			winText.text = "You Win";
			//send osc when complete!
			var message = new OSCMessage(winAddress);
			message.AddValue(OSCValue.String("win"));
			transmitter.Send(message);
		}
	}
}
