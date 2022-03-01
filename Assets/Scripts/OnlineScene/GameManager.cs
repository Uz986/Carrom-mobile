using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More then one gamemanager in scene");
        }
        else
            instance = this;
    }

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, StrickerOnline> players = new Dictionary<string, StrickerOnline>();

    public static void RegisteredPlayer(string _netID, StrickerOnline _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;

        Debug.Log(PLAYER_ID_PREFIX + _netID);
    }
    public static StrickerOnline GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    public static StrickerOnline[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }
    void OnDisable()
    {

        Destroy(this);
    }
}
