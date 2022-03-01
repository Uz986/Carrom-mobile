using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class HostGame : MonoBehaviour {
	[SerializeField]
	private uint roomSize=2;
    
    private string roomName;
    [SerializeField]
    InputField roomnamefiled;

   

   

	private NetworkManager networkManager;
	void Start()
	{
        networkManager = NetworkManager.singleton;
        

        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
           
        }
        
    }
	
	public void CreateRoom ()
	{
        roomName =roomnamefiled.text;
		if (roomName != "" && roomName != null) 
		{
          

			Debug.Log ("creating Room " + roomName + " with room for " + roomSize + " players ");
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
		
	}

  
}
