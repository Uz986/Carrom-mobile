using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine;

public class JoinGame : MonoBehaviour {
	List<GameObject> roomlist = new List<GameObject> ();
	[SerializeField]
	private Text status;
    
	[SerializeField]
	private GameObject roomListItemPrefab;
	[SerializeField]
	private Transform roomListParent;
	private NetworkManager networkManager;
	void Start () {
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) {


			networkManager.StartMatchMaker ();
		}
		RefreshRoomList ();
	
	}
	public void RefreshRoomList()
	{ clearRoomList ();
		networkManager.matchMaker.ListMatches (0, 20, "",true,0,0 ,OnMatchList);
		status.text = "loading...";
	}
	public void OnMatchList(bool success,string extendedInfo,List<MatchInfoSnapshot>matchList)
	{
		status.text = "";
		if (!success|| matchList == null) {
		
		
			status.text = "could't get room list";
			return;
		}


		foreach (MatchInfoSnapshot match in matchList) 
		{
			GameObject _roomListItemGo = Instantiate (roomListItemPrefab);
			_roomListItemGo.transform.SetParent (roomListParent,false);
			RoomListItem _roomListItem = _roomListItemGo.GetComponent<RoomListItem> ();
			if (_roomListItem != null) 
			{
				_roomListItem.setup (match,JoinRoom);


			}

			roomlist.Add (_roomListItemGo);
			}
		if (roomlist.Count == 0) {
			status.text="NO room at the Moment";
		}
	
	}
	void clearRoomList()
	{
		for (int i = 0; i < roomlist.Count; i++) {
			Destroy(roomlist [i]);
		
		}
		roomlist.Clear ();
	}
	public void JoinRoom(MatchInfoSnapshot _match)
	{
		networkManager.matchMaker.JoinMatch (_match.networkId, "","","",0,0, networkManager.OnMatchJoined);
		clearRoomList ();
		status.text="Joining....";


	}



}