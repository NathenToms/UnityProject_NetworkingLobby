using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
	public static LobbyManager Lobby;


	public static bool Open = true;


	public override void OnStartServer() => Lobby = new LobbyManager(NetworkServer.maxConnections);

	public override void OnServerConnect(NetworkConnection conn)
	{
		if (Open == false)
		{

			conn.Disconnect();

		}
	}
}

public class LobbyManager
{
	public Stack<int> OpenIDs = new Stack<int>();


	public LobbyManager(int count) { for (int i = count; i >= 0; i--) OpenIDs.Push(i); }

	public void RequestID(Player player)
	{
		player.PlayerID = OpenIDs.Pop();
		RpcUpdateConnectionMenu(true, player.PlayerID);
	}

	public void ReleaseID(int ID)
	{
		OpenIDs.Push(ID);
		RpcUpdateConnectionMenu(false, ID);
	}

	[ClientRpc]
	void RpcUpdateConnectionMenu(bool join, int ID)
	{
		if (join) ConnectionsMenu.Instance.AddConnection(ID);
	}
}
