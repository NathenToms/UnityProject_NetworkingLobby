using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectionsMenu : MonoBehaviour
{
	public static ConnectionsMenu Instance;


	[SerializeField]
	private GameObject connectionPrefab = null;


	private Dictionary<int, GameObject> connectionPanelDictionary = new Dictionary<int, GameObject>();

	void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	public void AddConnection(int ID)
	{
		connectionPanelDictionary.Add(ID, Instantiate(connectionPrefab, transform));
		connectionPanelDictionary[ID].GetComponentInChildren<TextMeshProUGUI>().text = ID.ToString();
	}

	public void RemoveConnection(int ID)
	{
		Destroy(connectionPanelDictionary[ID]);
		connectionPanelDictionary.Remove(ID);
	}
}
