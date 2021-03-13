using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;


public class ChatBox : NetworkBehaviour
{
	[SerializeField]
	private TMP_InputField inputField = null;

	[SerializeField]
	private Transform contentAnchor = null;

	[SerializeField]
	private GameObject chatObject = null;

	[SerializeField]
	private GameObject messagePrefab = null;


	public KeyCode ShowKey = KeyCode.LeftShift;

	// Show or Hide the Chat
	void Update()
	{
		if (Input.GetKeyDown(ShowKey)) {
			chatObject.SetActive(!chatObject.activeSelf);
		}
	}

	// Send a message to the server
	public void Send()
	{
		string message = inputField.text;


		// Find the local players 'ChatBox' manager and send the message
		Player.LocalPlayer.Chat.CmdSendMessageToChatBox(message);

		inputField.text = "";
	}

	public void OnEndEdit() { if (Input.GetKeyDown(KeyCode.Return)) Send(); }


	[ClientRpc]
	// This client has received a new message
	// This method tells the chat box to add it to the chat
	public void RpcReceiveNewMessage(string	 message)
	{
		StartCoroutine(AddMessage(message));
	}

	// Add a message to the chat
	IEnumerator AddMessage(string message)
	{
		// Instantiate the message prefab
		GameObject go = Instantiate(messagePrefab, contentAnchor);


		// Find the TextMeshProUGUI (text) component on the new message
		var messageText = go.GetComponentInChildren<TextMeshProUGUI>();

		messageText.text = message;


		// Wait for a frame
		// We do this because the TextMeshProUGUI doesn't update the 'textInfo.lineCount' for the first frame
		yield return null;


		// Get the ling count from the new message
		int lineCount = GetLineCount(messageText.GetComponentInChildren<TextMeshProUGUI>());

		ResizeMessageBox(go, lineCount, messageText.fontSize);
	}

	// Get the line count from a TextMeshProUGUI
	// Note this does NOT work on the firsts frame
	public int GetLineCount(TextMeshProUGUI textMeshPro)
	{
		return textMeshPro.textInfo.lineCount;
	}

	// Resize the new message to fit its text
	public void ResizeMessageBox(GameObject messageObject, int lineCount, float fontSize)
	{
		messageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (lineCount * (fontSize + 4) + 5));
	}
}
