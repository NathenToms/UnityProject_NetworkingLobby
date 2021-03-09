using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
	public static Player LocalPlayer;


	[SyncVar]
	public int PlayerID = -1;


	[Command]
	public void CmdSendMessageToChatBox(string message)
	{
		FindObjectOfType<ChatBox>().RpcReceiveNewMessage($"[{PlayerID}] | " + message);
	}


	public override void OnStartAuthority()
	{
		LocalPlayer = this;
		RequestID();
	}
	public override void OnStopClient()
	{
		Debug.Log("RELESE: " + PlayerID);
		ReleaseID();
	}

	[Command] private void RequestID() => CustomNetworkManager.Lobby.RequestID(this);
	[Command] private void ReleaseID() => CustomNetworkManager.Lobby.ReleaseID(this.PlayerID);
}
