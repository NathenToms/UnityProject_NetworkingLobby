using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


[RequireComponent(typeof(Player))]
public class PlayerIDManager : NetworkBehaviour
{
    [SyncVar]
    public int ID = -1;

	public override void OnStartServer()
	{
		// Request an ID from the server
		CustomNetworkManager.Lobby.RequestID(GetComponent<Player>());
	}

	public override void OnStopServer()
	{
		// Release our ID
		CustomNetworkManager.Lobby.ReleaseID(ID);
	}
}
