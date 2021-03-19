using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


// The PlayerChat Component is a part of the Player Object.
[RequireComponent(typeof(Player))]

public class PlayerChat : NetworkBehaviour
{
	[Command]
	public void CmdSendMessageToChatBox(string message)
	{
		FindObjectOfType<ChatBox>().RpcReceiveNewMessage(message);
	}
}
