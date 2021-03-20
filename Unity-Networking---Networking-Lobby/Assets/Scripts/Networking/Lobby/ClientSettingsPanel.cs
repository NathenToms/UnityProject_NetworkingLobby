using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientSettingsPanel : MonoBehaviour
{
	[SerializeField] private InputField username = null;

	[SerializeField] private Slider slider_R = null;
	[SerializeField] private Slider slider_G = null;
	[SerializeField] private Slider slider_B = null;

	[SerializeField] private Image playerColor = null;


	public void UpdateRed(float value) => playerColor.color = new Color(value, playerColor.color.g, playerColor.color.b);

	public void UpdateGreen(float value) => playerColor.color = new Color(playerColor.color.r, value, playerColor.color.b);

	public void UpdateBlue(float value) => playerColor.color = new Color(playerColor.color.r, playerColor.color.g, value);


	private void Start()
	{
		username.text = Player.LocalPlayer.Username;

		slider_R.value = Player.LocalPlayer.Color.r;
		slider_G.value = Player.LocalPlayer.Color.g;
		slider_B.value = Player.LocalPlayer.Color.b;

		playerColor.color = Player.LocalPlayer.Color;
	}


	public void Save()
	{
		Player.LocalPlayer.Cmd_UpdateUsername(username.text);
		Player.LocalPlayer.Cmd_UpdateColor(new Color(slider_R.value, slider_G.value, slider_B.value));

		LobbyItem.LocalItem.CmdOnNewConnectionInfo();

		Destroy(gameObject);
	}
}
