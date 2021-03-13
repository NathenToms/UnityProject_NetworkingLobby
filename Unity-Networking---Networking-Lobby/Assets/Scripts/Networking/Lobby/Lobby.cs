using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;


public class Lobby : MonoBehaviour
{
	public static Lobby Instance;

	[SerializeField]
	private GameObject lobbyObject = null;

	public Transform Anchor = null;


	public KeyCode ShowKey = KeyCode.RightShift;

	void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
		}

		Instance = this;
	}   
	
	// Show or Hide the Lobby
	void Update()
	{
		if (Input.GetKeyDown(ShowKey))
		{
			lobbyObject.SetActive(!lobbyObject.activeSelf);
		}
	}
}
