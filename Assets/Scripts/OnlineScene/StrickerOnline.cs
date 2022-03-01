using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.Networking.Match;
public class StrickerOnline : NetworkBehaviour
{
 
   
    public GameObject LineRenderray;
    // public int sideIndex;
   

    [SerializeField]
    Behaviour[] componenetstodisable;

    public float force;

    private Vector3 strikerPosition;

    public Rigidbody rb;

    public Transform aimTransform;
    

   

    



    [SyncVar]
    public bool OneMoreTurn = false;



   
    [SyncVar]
    public bool MyTurn ;

    [SyncVar]
    public int Score=0;

    

    [SerializeField]
    Text Wontxt;

    public bool IWon;

    public bool GameOver=false;

    private Transform SpawnPos;

    
    NetworkManager networkmanager;

    [SerializeField]
    MeshRenderer strickermesh;

    [SerializeField]
    CapsuleCollider strickercollider;

    [SerializeField]
    GameObject GamePlayUI;

    [SerializeField]
    GameObject Canvas;

    [SerializeField]
    GameObject GameEndPanel;
        
   

       [SerializeField]
    GameObject[] spawnGameobject;



    [SerializeField]
    Text Myscore;

    [SerializeField]
    Text OponentScore;

    [SerializeField]
    Text myUsername;

    [SerializeField]
    Text opUsername;

   

    [SyncVar]
     public bool GameEnd = false;

    [SyncVar]
    public string Username = "Loading....";
    void Start()
    {
      
         networkmanager = NetworkManager.singleton;

       
       
        if (!isLocalPlayer)
        {
            DisableCompoenets();
            if (isServer)
            {
                strickermesh.enabled = false;
                strickercollider.enabled = false;
                MyTurn = false;

            }



        }
        else
        {
            string _username = "Loading....";
            if (UserAccountManager.IsLoggedIn)
                _username = UserAccountManager.LoggedIn_playerUsername;
            else
                _username = transform.name;

            CmdSetUsername(transform.name, _username);


            List<Transform> t = networkmanager.startPositions;
            if (isServer)
            {
                SpawnPos = t[0]; //if first player
                GameObject campos = GameObject.FindGameObjectWithTag("cam2");

                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OnlineCameraScript>().SetPosition(campos);
                LineRenderray.SetActive(true);
                GamePlayUI.SetActive(true);
                Canvas.SetActive(true);
                MyTurn = true; //turn of first player right now
                StartCoroutine(GameUpdate());
             //  gameObject.name= UserAccountManager.LoggedIn_playerUsername;
            }
            else
            {
                SpawnPos = t[1]; //if 2nd player
                GameObject campos = GameObject.FindGameObjectWithTag("cam1");
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OnlineCameraScript>().SetPosition(campos);

                Canvas.SetActive(true);
                MyTurn = false;
               Cmdspawn();
               // gameObject.name = UserAccountManager.LoggedIn_playerUsername;
                CmdDisableForTurn();
                StartCoroutine(GameUpdate());
            }

            
        }
    }

  
    [Command]
    void CmdSetUsername(string PlayerID, string _username)
    {
        StrickerOnline strickerplayer = GameManager.GetPlayer(PlayerID);
        if (strickerplayer != null)
        {
            strickerplayer.Username = _username;
        }
    }

    IEnumerator GameUpdate()
    {
        //updating score
        StrickerOnline[] strickeronline = GameManager.GetAllPlayers();
        if (isLocalPlayer)
        {
           
            foreach (StrickerOnline st in strickeronline)
            {
                if (st.name == gameObject.name)
                {
                    Myscore.text = Score.ToString();
                }
                else if (st.name != gameObject.name)
                {
                    OponentScore.text = st.Score.ToString();
                }
            }
        }
            yield return new WaitForSeconds(1);

        //updating name for players
        foreach (StrickerOnline st in strickeronline)
        {
            if (st.name == gameObject.name)
            {
                    myUsername.text = Username;
            
            }
            else
            {
                 opUsername.text = st.Username;
              
            }
        }
        //checking if game end
        if (GameEnd)
        {
            GameOver = true;
            GamePlayUI.SetActive(false);
            GameEndPanel.SetActive(true);

            if (int.Parse(OponentScore.text) > Score)
            {
                Wontxt.text = "You Lost";
                IWon = false;
            }
            else
            {
                Wontxt.text = "You Won";
                IWon = true;
            }
        }


        StartCoroutine(GameUpdate());
    }

    public override void OnStartClient()
    {
        Debug.Log("start client");
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        StrickerOnline _strickeronline = GetComponent<StrickerOnline>();

        GameManager.RegisteredPlayer(_netID, _strickeronline);
    }

    [Command]
    void Cmdspawn()
    {
        foreach (GameObject go in spawnGameobject)
        {
            GameObject goo = Instantiate(go);
            NetworkServer.Spawn(goo);
            //  NetworkServer.SpawnWithClientAuthority(goo, base.connectionToClient);

            
        }
    }
    [Command]
    void CmdchangeAuthority()
    {
        StrickerOnline[] stO = GameManager.GetAllPlayers();

        LayerMask mask = (1 << 9); //only 9

        Collider[] hits = Physics.OverlapSphere(transform.position, 1000f, mask);
        foreach (Collider hit in hits)
        {
          if(stO[1].name!=gameObject.name)
            {
                hit.gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(stO[1].GetComponent<NetworkIdentity>().connectionToClient);
                Debug.Log("client auth");
            }
            else
            {
                hit.gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(stO[1].GetComponent<NetworkIdentity>().connectionToClient);
                Debug.Log("server auth");
            }
          
              
          
        }
    }

    void DisableCompoenets()
    {
        for (int i = 0; i < componenetstodisable.Length; i++)
        {

            componenetstodisable[i].enabled = false;
        }

    }
  

    public void SaveForce(float _force)
    {
        force = _force;
    }

    public void shoot() {


            if (!isLocalPlayer)
            return;

        Cmdshoot(force);


    }

    [Command]
    void Cmdshoot(float _force)
    {
        Rpcshoot(_force);
    }
    [ClientRpc]
    void Rpcshoot(float _force)
    {
        LineRenderray.SetActive(false);

        rb.AddForce(aimTransform.forward * _force);
        Debug.Log(_force);
        StartCoroutine(respawn());
    }
    //work here enter enableforturn without cmd

    [Command]
   public void CmdEnableForTurn(bool IgnoreFirstplayerturn)
    {
        if(!isLocalPlayer&&IgnoreFirstplayerturn)
        {
            Debug.Log(gameObject.name+" not local player but server");
            StrickerOnline[] strickeronline = GameManager.GetAllPlayers();
            foreach (StrickerOnline st in strickeronline)
            {
                if(st.name!=gameObject.name)
                {
                    st.CmdEnableForTurn(false);
                }
            }
            return;
        }

        RpcEnableForTurn();
    }
    [ClientRpc]
    void RpcEnableForTurn()
    {
        if(isLocalPlayer)
        {
            GamePlayUI.SetActive(true);
            LineRenderray.SetActive(true);
            
            for (int i = 0; i < componenetstodisable.Length; i++)
            {

                componenetstodisable[i].enabled = true;
            }
           
        }
        
        strickermesh.enabled = true;
        strickercollider.enabled = true;
        MyTurn = true;

    

      this.GetComponent<Movement>().checkforspawningposition(1);
        Debug.Log(gameObject.name + "enable");
    }
   

  [Command]
    void CmdDisableForTurn()
    {
        RpcDisableForTurn();
    }
    [ClientRpc]
    void RpcDisableForTurn()
    {
        for (int i = 0; i < componenetstodisable.Length; i++)
        {

            componenetstodisable[i].enabled = false;
        }

        GamePlayUI.SetActive(false);
        LineRenderray.SetActive(false);
        strickermesh.enabled = false;
        strickercollider.enabled = false;
        Debug.Log(gameObject.name + "disable");
    }


    IEnumerator respawn()
    {
        if (!isLocalPlayer)
            yield break;

        GameEndCheck();
        StrickerOnline[] strickeronline = GameManager.GetAllPlayers();

        

        yield return new WaitForSeconds(3);
        resettransform();
    

            CmdDisableForTurn();

       
      

        if (MyTurn&&!OneMoreTurn)
        {
            
            
            foreach (StrickerOnline st in strickeronline)
            {
                if(st.name!=gameObject.name)
                {
                    CmdchangeAuthority();
                    if(isServer)
                    st.CmdEnableForTurn(false);
                    else
                        CmdEnableForTurn(true);

                    Debug.Log(gameObject.name);
                }
                else
                {
                    Debug.Log(st.name+"same user"+gameObject.name);
                }
            }

         


        }
        else
        {
            CmdEnableForTurn(false);
            OneMoreTurn = false;
        }
    }

    [Command]
    void CmdmyturnEnable()
    {
        RpcmyturnDisable();
    }
    [ClientRpc]
    void RpcmyturnDisable()
    {
        MyTurn = false;
        Debug.Log("myturn false");
    }

 

    void resettransform()
    {
        if (!isLocalPlayer)
            return;

        transform.position = SpawnPos.position;
        transform.rotation = SpawnPos.rotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }




    void GameEndCheck()
    {
      

        LayerMask mask = (1 << 9 ); //only 9

        Collider[] hits = Physics.OverlapSphere(transform.position, 1000f, mask);

        if (hits.Length<=2)
        {
            Debug.Log(hits.Length + " hits l");
          //  last2goti = true;
        }
        Debug.Log(hits.Length + " hits 2"+hits);
        if (hits.Length == 0||hits==null)
        {
            StrickerOnline[] strickeronline = GameManager.GetAllPlayers();

                strickeronline[0].GameEnd = true;
                strickeronline[1].GameEnd = true;
         

        }
    }
    [Command]
    public void CmdScore(GameObject _goti,int _score)
    {
     
        RpcScore(_goti,_score);
       
    }
    [ClientRpc]
    void RpcScore(GameObject _goti, int _score)
    {
       
            Debug.Log(_score + " " + _goti);
            Score = Score + _score;
            CmdDestroy(_goti);

      

      OneMoreTurn = true;
       
    }
    [Command]
    void CmdDestroy(GameObject _goti)
    {
        NetworkServer.Destroy(_goti);
    }
   
 


    
   
    public void LeaveRoom()
    {
       
        MatchInfo matchInfo = networkmanager.matchInfo;
        networkmanager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkmanager.OnDropConnection);
        networkmanager.StopHost();

    }
}






