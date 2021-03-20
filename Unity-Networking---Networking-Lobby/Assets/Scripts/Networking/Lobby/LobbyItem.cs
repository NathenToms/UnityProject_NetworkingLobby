using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

// A LobbyItem is the panel that represents a player in the lobby list
public class LobbyItem : NetworkBehaviour
{
	public static LobbyItem LocalItem;

	//
	[SerializeField] private GameObject settingsPrefab = null;

	//
	[SerializeField] private TextMeshProUGUI nameText = null;

	//
	[SerializeField] private GameObject editButton = null;

	// 
	[SerializeField] private Image playerColor = null;


	void Awake()
	{
		transform.SetParent(Lobby.Instance.Anchor);
	}

	public override void OnStartAuthority()
	{
		LocalItem = this;
		CmdOnNewConnectionInfo();
		editButton.SetActive(true);
	}

	public void OpenSettings()
	{
		Instantiate(settingsPrefab, FindObjectOfType<Canvas>().transform);
	}

	//
	[Command]
	public void CmdOnNewConnectionInfo()
	{
		foreach (Player player in FindObjectsOfType<Player>())
		{
			player.Lobby.InfoPanel.RpcUpdateInfo(player.ID, player.Username, player.Color);
		}
	}

	//
	[ClientRpc]
	public void RpcUpdateInfo(int ID, string username, Color color)
	{
		nameText.text = username;
		playerColor.color = color;
	}
}
