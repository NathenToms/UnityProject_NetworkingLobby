using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;


public class ChatBox : NetworkBehaviour
{
	// The text field the player has types there message into.
	[SerializeField] private TMP_InputField inputField = null;

	// The anchor we parent messages to whenever a new message is created.
	[SerializeField] private Transform contentAnchor = null;

	// The message prefab we use to instantiate a new message
	[SerializeField] private GameObject messagePrefab = null;

	// The chat object itself, we use this to hide the chat box when the player doesn't want to see it.
	[SerializeField] private GameObject chatObject = null;


	// The Key we press to toggle the viability of the chat
	public KeyCode ShowKey = KeyCode.LeftShift;

	// Show or Hide the Chat
	void Update()
	{
		if (Input.GetKeyDown(ShowKey)) {
			chatObject.SetActive(!chatObject.activeSelf);

			if (chatObject.activeSelf == true)
			{
				inputField.Select();
				inputField.ActivateInputField();
			}
		}
	}

	// Send a message to the server
	public void Send()
	{
		string message = inputField.text;

		// Validate Message the message
		// Make sure its not profanity or black
		if (ValidateMessage(message))
		{
			// Find the local players 'ChatBox' manager and send the message
			Player.LocalPlayer.Chat.CmdSendMessageToChatBox($"[{Player.LocalPlayer.ID}] " + message);

		}

		inputField.Select();
		inputField.ActivateInputField();

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

		
		// Call the On message event
		Player.LocalPlayer.OnChatMessage(message);
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

	// Validate the message
	public bool ValidateMessage(string message)
	{
		return true;
	}
}
