using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
	public static LobbyManager Lobby;


	public static bool Open = true;


	public override void OnStartServer()
	{
		Lobby = new LobbyManager(NetworkServer.maxConnections);

		if (TryGetComponent<NetworkManagerHUD>(out var HUD))
		{
			HUD.enabled = false;
		}
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		if (Open == false)
		{

			conn.Disconnect();

		}
	}

	private void OnGUI()
	{
		if (Lobby == null) return;

		GUILayout.BeginHorizontal("box");

		foreach (int value in Lobby.OpenIDs)
		{
			GUILayout.Label(value.ToString());
		}

		GUILayout.EndHorizontal();
	}
}

public class LobbyManager
{
	public Stack<int> OpenIDs = new Stack<int>();


	public LobbyManager(int count) { for (int i = count; i >= 0; i--) OpenIDs.Push(i); }


	public void RequestID(Player player)
	{
		player.PlayerID.ID = OpenIDs.Pop();
	}

	public void ReleaseID(int ID)
	{
		Debug.Log("adding ID: " + ID);
		OpenIDs.Push(ID);
	}
}
