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


	public override void OnStartAuthority()
	{
		LocalPlayer = this;

		Lobby.CmdSpawnConnectionPanel();
	}

	public virtual void OnChatMessage(string data) { }
	public virtual void OnLobbyUpdate(string data) { }
	public virtual void OnUpdateID(int newID) { }
}
