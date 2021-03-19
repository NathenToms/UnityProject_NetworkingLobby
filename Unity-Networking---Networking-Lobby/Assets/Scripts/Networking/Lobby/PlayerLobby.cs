using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


[RequireComponent(typeof(Player))]
public class PlayerLobby : NetworkBehaviour
{
	[SerializeField]
	private LobbyItem infoPanelPrefab = null;

	[HideInInspector]
	public LobbyItem InfoPanel = null;

	[Command]
	public void CmdSpawnConnectionPanel()
	{
		InfoPanel = Instantiate(infoPanelPrefab, Lobby.Instance.transform);

		// Spawn the connection info panel on the server and all clients
		NetworkServer.Spawn(InfoPanel.gameObject, connectionToClient);
	}
}
