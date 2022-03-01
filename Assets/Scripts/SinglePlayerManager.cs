using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerManager : MonoBehaviour {

    // Use this for initialization

   public static SinglePlayerManager instance;
   public  int Level;

    public int material = 0;
	void Awake () {
		if (instance!=null)
        {
            Debug.Log("more then one SinglePlayerManager ");
        }
        instance = this;
	}

  
	
	// Update is called once per frame
	public void LevelSelect(int i)
    {
        Level = i;
    }
    public void buymat(int i)
    {
        material = i;
    }
}
