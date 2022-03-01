using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IMP3 : MonoBehaviour {

	public void BMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
	}


}