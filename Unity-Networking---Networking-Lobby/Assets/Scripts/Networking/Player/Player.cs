using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


[RequireComponent(typeof(PlayerLobby))]
[RequireComponent(typeof(PlayerChat))]
[RequireComponent(typeof(PlayerID))]

public abstract class Player : NetworkBehaviour
{
	// Static Members
	// The Local Player for this client
	public static Player LocalPlayer;


	// The number of player connected to the server
	public static int PlayerCount = 0;


	// Editor References
	// The Players Chat Manager Component
	[SerializeField] private PlayerChat chat = null;

	// The Players Lobby Manager Component
	[SerializeField] private PlayerLobby lobby = null;

	// The Players ID Manager Component
	[SerializeField] private PlayerID playerID = null;


	// Public Members
	// This players username
	[SyncVar(hook = "OnUsernameChange")] public string Username;
	public virtual void OnUsernameChange(string oldUsername, string newUsername) { }

	// This players Color
	[SyncVar(hook = "OnColorChange")] public Color Color;
	public virtual void OnColorChange(Color oldColor, Color newColor) { }


	// Properties
	// Get this players ID
	public int ID { get { return playerID.ID; } set { playerID.ID = value; } }


	// Get this players Lobby Manager
	public PlayerLobby Lobby { get { return lobby; } }

	// Get this players Chat Manager
	public PlayerChat Chat { get { return chat; } }


	// Methods
	// OnStart and OnStop a Client
	public override void OnStartClient() => PlayerCount++;
	public override void OnStopClient() => PlayerCount--;


	public virtual void OnIDChange(int oldID, int newID) { }

	public override void OnStartAuthority()
	{
		LocalPlayer = this;

		Lobby.CmdSpawnConnectionPanel();
	}


	[Command]
	public void Cmd_UpdateUsername(string newUsername) {
		Username = newUsername;
	}

	[Command]
	public void Cmd_UpdateColor(Color newColor) {
		Color = newColor;
	}
}
