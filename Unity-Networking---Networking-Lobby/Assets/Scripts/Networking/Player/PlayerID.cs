using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


[RequireComponent(typeof(Player))]
public class PlayerID : NetworkBehaviour
{
	[SyncVar(hook = "UpdateID")]
	public int ID = -1;

	public override void OnStartServer() => CustomNetworkManager.RequestID(GetComponent<Player>());
	public override void OnStopServer()  => CustomNetworkManager.ReleaseID(ID);

	public void UpdateID(int oldID, int newID)
	{
		Player player = GetComponent<Player>();

		player.OnIDChange(oldID, newID);
		player.Cmd_UpdateUsername($"Player {newID}");
	}
}
