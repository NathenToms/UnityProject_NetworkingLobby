using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
	public static Player LocalPlayer;

	public Player_Chat Chat = null;
	public Player_Lobby Lobby = null;

	public PlayerIDManager PlayerID = null;

	public override void OnStartAuthority()
	{
		LocalPlayer = this;

		Lobby.CmdSpawnConnectionPanel();
	}
}
