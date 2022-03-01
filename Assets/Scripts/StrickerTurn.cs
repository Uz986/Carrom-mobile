using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class StrickerTurn : MonoBehaviour
{
    //public enum StrikerState {
    //    Positioning,
    //    Moving
    //};
    //public StrikerState _strikerState;
    public GameObject LineRenderray;
    public int sideIndex;
    public CarromSides[] sides;

    [HideInInspector]
    public CarromSides currentSide;

    public float force;

    private Vector3 strikerPosition;

    public Rigidbody rb;

    public Transform aimTransform;
    public CameraScript cameraScript;

    private bool canthit = false;

    [HideInInspector]
    public bool Isplayerturn = true;

    int PrevTurnIndex=0;

    [HideInInspector]
    public bool OneMoreTurn = false;

    SinglePlayerManager singlepalyermanager;

    bool MediumDifficulty;

    rotate rot;


     GameObject[] blackgoti;
     GameObject[] whitegoti;

    [HideInInspector]
    public GameObject LastGoti = null;

    [HideInInspector]
    public bool queenScore = false;

    [SerializeField]
    GameObject Gameplay;

    [SerializeField]
    GameObject GameendPanel;

    [SerializeField]
    Text Wontxt;

    

    void Start()
    {
        AssignSide(0);
        rot = rotate.instance;
        singlepalyermanager = SinglePlayerManager.instance ;
        Debug.Log(singlepalyermanager.Level+" difficulty");
        MediumDifficulty = true;

     
          GetComponent<Renderer>().sharedMaterial = Resources.Load(PlayerPrefs.GetInt("CurrentStricker",0).ToString(), typeof(Material)) as Material;
      

    }

    public static StrickerTurn instance;

    void Awake()
    {

        if (instance != null)
        {
            Debug.Log("more then one strickerturn");
            return;
        }
        instance = this;
       

    }


    public void AssignSide(int index)
    {
        currentSide = sides[index];
        PositionStriker();
    }

    void PositionStriker()
    {
        transform.position = currentSide.transform.position;
        transform.rotation = currentSide.transform.rotation;
    }

    //public void ChangeState(StrikerState state)
    //{
    //    _strikerState = state;
    //}

    public void shoot() {

        if (canthit)
            return;

        LineRenderray.SetActive(false);
        //ChangeState(StrikerState.Moving);
        rb.AddForce(aimTransform.forward * force);
        StartCoroutine(respawn());
    }

    
    private void FixedUpdate()
    {
        if (canthit)
            return;
      
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LineRenderray.SetActive(false);
            //ChangeState(StrikerState.Moving);
            rb.AddForce(aimTransform.forward * force);
            StartCoroutine(respawn());
        }

        if (Input.GetKey(KeyCode.A)|| 0 > CrossPlatformInputManager.GetAxis("Horizontal"))
        {
            
            transform.position = new Vector3(transform.position.x - 3 * Time.deltaTime, transform.position.y, transform.position.z);

            if (transform.position.x < currentSide.boundLeft)
                transform.position = new Vector3(currentSide.boundLeft, transform.position.y, transform.position.z);

            checkforspawningposition( -1);

        }
        else if (Input.GetKey(KeyCode.D)|| 0 < CrossPlatformInputManager.GetAxis("Horizontal"))
        {
            transform.position = new Vector3(transform.position.x + 3 * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x > currentSide.boundRight)
                transform.position = new Vector3(currentSide.boundRight, transform.position.y, transform.position.z);

            checkforspawningposition( 1);
         
        }

       //if (Input.GetKeyUp(KeyCode.R))
       //{
       //    ResetTransform(2);

       //     Quaternion position = Quaternion.Euler(0, Random.Range(100f ,270f), 0);
       //     transform.rotation = position;


       // }

       
    }

    //public void callrespawnfromout()
    //{
    //    StartCoroutine(respawn());
    //}

    IEnumerator respawn()
    {
        canthit = true;
        rot.canrotate = false;
        yield return new WaitForSeconds(3);


        GameEndCheck();
        //change turn
        Isplayerturn = !Isplayerturn;

        if(OneMoreTurn)
        {
            //if someone made score turn goes back
            Isplayerturn = !Isplayerturn;

            if (Isplayerturn)
            {
                canthit = false;
                rot.canrotate = true;
                LineRenderray.SetActive(true);
            }

            ResetTransform(PrevTurnIndex);
            
            

            OneMoreTurn = false;
        }
        else
        {
            if (queenScore == true)
            {
                queenScore = false;
                Score_2p.redgoti.SetActive(true);
            }

            if (Isplayerturn)
            {
                canthit = false;
                rot.canrotate = true;
                ResetTransform(0);
                PrevTurnIndex = 0;
                LineRenderray.SetActive(true);
            }
            else
            {
                ResetTransform(2);
                PrevTurnIndex = 2;
            }
        }

      
        
         
     }

    public void ResetTransform(int playerIndex)
    {
       
       
        currentSide = sides[playerIndex];
        transform.position = sides[playerIndex].transform.position;
        transform.rotation = sides[playerIndex].transform.localRotation;
        cameraScript.SetPosition(playerIndex);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //  transform.Rotate(Vector3.up, -0.6561228f );

      


        if (!Isplayerturn)
        {
            StartCoroutine(waitforautostrike());
        }
        else
        {
            checkforspawningposition(1);  //send 1 muliplier for increasing x
        }
    }

  

    IEnumerator waitforautostrike()
    {
        if (singlepalyermanager.Level == 1)
            EasyLockonTarget();

        //on medium difficulty switch between easy and hard mode
        else if (singlepalyermanager.Level == 2)
        {
            if (MediumDifficulty)
            {
                EasyLockonTarget();
                MediumDifficulty = !MediumDifficulty;
            }
            else
            {
                HardLockonTarget();
                MediumDifficulty = !MediumDifficulty;
            }

        }

        else if (singlepalyermanager.Level == 3)
            HardLockonTarget();
        

        yield return new WaitForSeconds(2);
        LineRenderray.SetActive(false);
        rb.AddForce(aimTransform.forward * 2000);
        StartCoroutine(respawn());  
    }

    void EasyLockonTarget()
    {


         transform.position = new Vector3(UnityEngine.Random.Range(currentSide.boundLeft, currentSide.boundRight), transform.position.y, transform.position.z);
        checkforspawningposition(1); //send 1 muliplier for increasing x
        float temp;
        float distance=0;
        GameObject target;
    
        GameObject go = GameObject.FindGameObjectWithTag("RGoti");
        // target = go;
        //for red 
        target = go;
        if (go != null)
        {

            temp = Vector3.Distance(go.transform.position, transform.position);
            distance = temp;
            target = go;
           
        }



        Debug.Log(distance);

        //for black


        blackgoti = GameObject.FindGameObjectsWithTag("BGoti");

        if (blackgoti.Length > 0) //if there is no black goti ignore searching
        {

            if (distance == 0)
            {
                distance = Vector3.Distance(blackgoti[0].transform.position, transform.position);
                target = blackgoti[0];
            }

            foreach (GameObject bg in blackgoti)
            {

                temp = Vector3.Distance(bg.transform.position, transform.position);
                if (distance > temp)
                {
                    Debug.Log(bg.name + distance);
                    distance = temp;
                    target = bg;
                }

            }

        }
        //for white
        whitegoti = GameObject.FindGameObjectsWithTag("WGoti");
       
      if(whitegoti.Length > 0)
        {
            if (distance == 0)
            {
                distance = Vector3.Distance(whitegoti[0].transform.position, transform.position);
                target = whitegoti[0];
            }

            foreach (GameObject wg in whitegoti)
            {

                temp = Vector3.Distance(wg.transform.position, transform.position);
                if (distance > temp)
                {
                    Debug.Log(wg.name + distance);
                    distance = temp;
                    target = wg;
                }

            }
        }


      if(target==null)
        {
            //game ended

            return;
        }

        Debug.Log(target.name+"target");

        Vector3 targetposition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        transform.LookAt(targetposition);

      
    }

    void HardLockonTarget()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(currentSide.boundLeft, currentSide.boundRight), transform.position.y, transform.position.z);
        checkforspawningposition( 1); //send 1 muliplier for increasing x
        



        LayerMask mask = ~(1 << 0 | 1<<9); //all layers except 0 and 9
        Collider[] hits = Physics.OverlapSphere(transform.position, 1000f, mask);
        GameObject[] Holes=new GameObject[4];
        int i = 0;
        foreach (Collider hit in hits) //save holes in temporary gameobject
        {
            if(hit.tag=="Hole")
            {
                Holes[i]= hit.gameObject;
                Debug.Log(hit.gameObject.name);
                i++;
            }
           
        }
        //loop for checking all  goti one by one
        foreach (Collider hit in hits)
        {

            RaycastHit temphit;
            RaycastHit temphitReflect;

            //loop for check each goti against all four holes
            for (int k = 0; k < 4; k++)
            { 

            
            if (hit.tag == "DoubleCollider")
            {
                int mas = ~(1 << 2); //all layers except 2nd
                Physics.Raycast(hit.gameObject.transform.position, Holes[k].transform.position - hit.gameObject.transform.position, out temphit, 1000f, mas);


             //  Debug.DrawRay(hit.gameObject.transform.position, Holes[k].transform.position - hit.gameObject.transform.position, Color.magenta, 5f);

                Debug.Log(hit.name);

                if (temphit.collider.gameObject.tag == "Hole") //goes in hole without any object between it
                {
                    transform.forward = hit.gameObject.transform.position - transform.position;
                    Debug.Log(hit.gameObject.name);
                    for (int j = 0; j < 20; j++)
                    {
                        bool skip = false;

                        if (Physics.Raycast(transform.position, transform.forward, out temphit)) //aim from striker to goti
                        {

                            if (temphit.collider.gameObject == hit.gameObject) //means no gameobject between stricker and goti
                            {
                                Vector3 reflectedvector = temphit.normal * -10;
                                    //   Ray tempray = new Ray(temphit.point, reflectedvector); //gives a reflected bounce of aim

                                    Ray r = new Ray(temphit.point, reflectedvector);
                                    Vector3 pluspoint = r.GetPoint(0.9f); // increase the position of starting point of casting ray
                                    Physics.Raycast(pluspoint, reflectedvector, out temphitReflect, 1000f, (1 << 10|1<<9));

                                Debug.DrawRay(pluspoint, reflectedvector, Color.white, 5);
                                //Debug.Log(temphitReflect.collider.gameObject.tag);
                                Debug.Log(j);

                                if (temphitReflect.collider==null)
                                {
                                       
                                    skip = false;
                                }
                                else 
                                {
                                      Debug.Log(temphitReflect.collider.gameObject.name);
                                        if (temphitReflect.collider.gameObject.tag == "Hole")
                                        {
                                         skip = true;
                                        }
                                }

                                if (skip)
                                {
                                    Debug.Log("locked on "+hit.name);
                                    return;
                                }


                                else
                                {
                                    if (j < 10)
                                    {
                                        Debug.DrawRay(transform.position, transform.forward, Color.blue, 5);
                                        transform.Rotate(Vector3.down, 0.6f);

                                    }
                                    else if (j == 10)
                                    {
                                        Debug.DrawRay(transform.position, transform.forward, Color.green, 5);
                                        transform.Rotate(Vector3.up, 6.8f);

                                        Debug.DrawRay(transform.position, transform.forward, Color.red, 5);


                                    }
                                    else
                                    {
                                        transform.Rotate(Vector3.up, 0.6f);
                                        Debug.DrawRay(transform.position, transform.forward, Color.grey, 5);
                                    }



                                }
                            }

                        }

                    }

                }
                //
            }

        }
            }
        
       
    }


    void checkforspawningposition(int multiplier_move)
    {

        LayerMask mask = ~(1 << 0 | 1 << 8); //all layers except 0 and 8

        Collider[] hits = Physics.OverlapSphere(transform.position,0.24f,mask);
       
        foreach (Collider hit in hits)
        {
           
            if (hit.tag=="RGoti"||hit.tag=="WGoti"||hit.tag=="BGoti")
            {
                Debug.Log("change pos");
                  transform.position = new Vector3(transform.position.x+(0.25f* multiplier_move), transform.position.y, transform.position.z);

                if(multiplier_move>0)
                {
                    if (transform.position.x > currentSide.boundRight)
                        transform.position = new Vector3(currentSide.boundLeft, transform.position.y, transform.position.z);
                   
                }
             else
                {
                    if (transform.position.x < currentSide.boundLeft) 
                        transform.position = new Vector3(currentSide.boundRight, transform.position.y, transform.position.z);
                   
                }

                checkforspawningposition( multiplier_move);
              
                return;
            }
        }
       
       
       
    }

    void GameEndCheck()
    {
      

        LayerMask mask = (1 << 9 ); //only 9

        Collider[] hits = Physics.OverlapSphere(transform.position, 1000f, mask);

        if(hits.Length==0)
        {
            //game ends
            Gameplay.SetActive(false);
            GameendPanel.SetActive(true);
            if(Score_2p.PlayerScore>Score_2p.AIScore)
            {
                Wontxt.text = "You Won";
            }
            else
            {
                Wontxt.text = "You Lose";
            }
          
            return;
        }

        if(LastGoti!=null)
        {
            Debug.Log("last goti not null");
            return;
        }

      
        
        if (hits.Length < 3)
        {
            Debug.Log("hits 3");
            int checkqueenAlive = -1;

            if (hits[0].tag == "RGoti")
            {
                checkqueenAlive = 0;
                Debug.Log("hits 0");
            }
            else if(hits.Length==1)
            {
                checkqueenAlive = 1;
            }
            else if (hits[1].tag == "RGoti")
            {
                checkqueenAlive = 1;
                Debug.Log("hits 1");
            }


            if (checkqueenAlive == 0)
            {
                LastGoti = hits[1].gameObject;
                LastGoti.AddComponent<LastGoti>();
            }
            else if (checkqueenAlive == 1)
            {
                LastGoti = hits[0].gameObject;
                LastGoti.AddComponent<LastGoti>();
                //  hits[0].gameObject.AddComponent<rotate>();
            }
        }
    }
}






