using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;


public class LobbyItem : NetworkBehaviour
{
	public static LobbyItem LocalConnectionInfo;


	[SerializeField]
	private TextMeshProUGUI nameText = null;

	void Awake()
	{
		transform.SetParent(Lobby.Instance.Anchor);
	}

	public override void OnStartAuthority()
	{
		LocalConnectionInfo = this;

		CmdOnNewConnectionInfo();
	}

	[Command]
	public void CmdOnNewConnectionInfo()
	{
		foreach (Player player in FindObjectsOfType<Player>())
		{
			player.Lobby.InfoPanel.RpcUpdateInfo(player.PlayerID.ID);
		}
	}

	[ClientRpc]
	public void RpcUpdateInfo(int ID)
	{
		nameText.text = ID.ToString();
	}
}
