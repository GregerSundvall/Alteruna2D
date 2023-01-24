using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alteruna;
using System.Collections;
using System.Linq;
using Alteruna.Trinity;

public class RoomMenu : CommunicationBridge
{
	[SerializeField] private Text TitleText;
	[SerializeField] private ScrollRect ScrollRect;
	[SerializeField] private GameObject LANEntryPrefab;
	[SerializeField] private GameObject WANEntryPrefab;
	[SerializeField] private GameObject CloudImage;
	[SerializeField] private GameObject ContentContainer;
	[SerializeField] private Button StartButton;
	[SerializeField] private Button LeaveButton;

	public bool AutomaticallyRefresh = true;
	public float RefreshInterval = 5.0f;
	
	private List<Room> _rooms = new List<Room>();
	private List<GameObject> _roomObjects = new List<GameObject>();
	private float _refreshTime;

	private int count;
	private string _connectionMessage = "Connecting";
	private float _statusTextTime;

	private void Connected(Multiplayer multiplayer, Endpoint endpoint)
	{
		StartButton.interactable = true;
		LeaveButton.interactable = false;
		
		if (TitleText != null)
		{
			TitleText.text = "Rooms";
		}

		//if (isActiveAndEnabled)
		//{
		//	StartCoroutine(nameof(RefreshRooms));
		//}
	}

	private void Disconnected(Multiplayer multiplayer, Endpoint endPoint)
	{
		StartButton.interactable = false;
		LeaveButton.interactable = false;

		_connectionMessage = "Reconnecting";
		if (TitleText != null)
		{
			TitleText.text = "Reconnecting";
		}
	}

	private void UpdateList(Multiplayer multiplayer)
	{
		for (int i = 0; i < _roomObjects.Count; i++)
		{
			Destroy(_roomObjects[i]);
		}

		_roomObjects.Clear();

		if (ContentContainer != null)
		{
			for (int i = 0; i < multiplayer.AvailableRooms.Count; i++)
			{
				Room room = multiplayer.AvailableRooms[i];
				
				if (Multiplayer.InRoom && room.ID == Multiplayer.CurrentRoom.ID)
				{
					continue;
				}

				GameObject entry;
				if (room.Local)
				{
					entry = Instantiate(WANEntryPrefab, ContentContainer.transform);
				}
				else
				{
					entry = Instantiate(LANEntryPrefab, ContentContainer.transform);
				}

				entry.SetActive(true);
				_roomObjects.Add(entry);

				entry.GetComponentInChildren<Text>().text = room.Name;
				entry.GetComponentInChildren<Button>().onClick.AddListener(() => { room.Join(); });
			}
		}
	}

	private void JoinedRoom(Multiplayer multiplayer, Room room, User user)
	{
		StartButton.interactable = false;
		LeaveButton.interactable = true;

		if (TitleText != null)
		{
			TitleText.text = "In Room " + room.Name;
		}
	}

	private void LeftRoom(Multiplayer multiplayer)
	{
		StartButton.interactable = true;
		LeaveButton.interactable = false;

		if (TitleText != null)
		{
			TitleText.text = "Rooms";
		}
	}

	private void FixedUpdate()
	{
		if (Multiplayer.IsConnected)
		{
			if (!AutomaticallyRefresh || (_refreshTime += Time.fixedDeltaTime) < RefreshInterval) return;
			_refreshTime -= RefreshInterval;

			Multiplayer.RefreshRoomList();

			if (TitleText == null) return;
			
			ResponseCode blockedReason = Multiplayer.GetLastBlockResponse();
			
			if (blockedReason == ResponseCode.NaN) return;
			
			string str = blockedReason.ToString();
			str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
			TitleText.text = str;
		}
		else if ((_statusTextTime += Time.fixedDeltaTime) >= 1)
		{
			_statusTextTime -= 1;
			ResponseCode blockedReason = Multiplayer.GetLastBlockResponse();
			if (blockedReason != ResponseCode.NaN)
			{
				string str = blockedReason.ToString();
				str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
				TitleText.text = str;
				return;
			}

			switch (count)
			{
				case 0:
					TitleText.text = _connectionMessage + ".  ";
					break;
				case 1:
					TitleText.text = _connectionMessage + ".. ";
					break;
				default:
					TitleText.text = _connectionMessage + "...";
					count = -1;
					break;
			}

			count++;
		}
	}

	private void Start()
	{
		if (Multiplayer == null)
		{
			
			Debug.LogError("Unable to find a active object of type Multiplayer.");
			if (TitleText != null) TitleText.text = "Missing Multiplayer Component";
			enabled = false;
		}
		else
		{
			Multiplayer.Connected.AddListener(Connected);
			Multiplayer.Disconnected.AddListener(Disconnected);
			Multiplayer.RoomListUpdated.AddListener(UpdateList);
			Multiplayer.RoomJoined.AddListener(JoinedRoom);
			Multiplayer.RoomLeft.AddListener(LeftRoom);
			StartButton.onClick.AddListener(() => { Multiplayer.JoinOnDemandRoom(); });
			LeaveButton.onClick.AddListener(() => { Multiplayer.CurrentRoom?.Leave(); });
			
			if (TitleText != null)
			{
				ResponseCode blockedReason = Multiplayer.GetLastBlockResponse();
				if (blockedReason != ResponseCode.NaN)
				{
					string str = blockedReason.ToString();
					str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
					TitleText.text = str;
				}
				else
				{
					TitleText.text = "Connecting";
				}
			}
		}

		StartButton.interactable = false;
		LeaveButton.interactable = false;
	}
/*
	private IEnumerator RefreshRooms()
	{
		while (AutomaticallyRefresh)
		{
			yield return new WaitForSeconds(RefreshInterval);
			_aump.RefreshRoomList();

			if (TitleText != null)
			{
				ResponseCode blockedReason = _aump.GetLastBlockResponse();
				if (blockedReason != ResponseCode.NaN)
				{
					string str = blockedReason.ToString();
					str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
					TitleText.text = str;
					break;
				}
			}
		}
	}
	*/
}