using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


// The CustomNetworkManager extends the normal NetworkManager.
// 1) We manage if the lobby is Open or Closed.
// 2) We manage the visibility of the NetworkManagerHUD.
// 3) We manage player IDS.

public class CustomNetworkManager : NetworkManager
{
	// A Stack of open ID we can hand out to connecting players.
	public static Stack<int> OpenIDs = new Stack<int>();


	// If the Lobby open? or do we need to refuse new connections.
	public static bool Open = true;


	// When the server starts we fill out the IDStack with 
	// We turn off the NetworkManagerHUD
	public override void OnStartServer()
	{
		for (int i = NetworkServer.maxConnections; i >= 0; i--) OpenIDs.Push(i); 

		if (TryGetComponent<NetworkManagerHUD>(out var HUD))
		{
			HUD.enabled = false;
		}
	}

	// Refuse connections if the server is not open
	public override void OnServerConnect(NetworkConnection conn)
	{
		if (Open == false)
		{
			conn.Disconnect();
		}
	}

	// When a client connects is Requests an ID
	public static void RequestID(Player player) {
		player.ID = OpenIDs.Pop();
	}

	// When a client disconnects it returns its ID
	public static void ReleaseID(int ID) {
		OpenIDs.Push(ID);
	}
}
