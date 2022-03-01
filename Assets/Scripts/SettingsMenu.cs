using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

	public AudioMixer audiomixer;

	public Dropdown ResolutionDropdown;
	Resolution [] resolutions;

	void Start()
	{
		//resolutions = Screen.resolutions;
		//ResolutionDropdown.ClearOptions ();

		//List<string> options =new List<string>();

		//for (int i = 0; i < resolutions.Length; i++) 
		//{
		//	string option = resolutions[i].width + "x" + resolutions[i].height;
		//	options.Add (option);
		//}
		//ResolutionDropdown.AddOptions (options); 
	}

	public void SetVolume (float volume)
	{
		audiomixer.SetFloat ("volume", volume);
	}

	public void SetQuality(int QualityIndex)
	{
		QualitySettings.SetQualityLevel (QualityIndex);
	}

	public void SetFullScreen(bool isfullscreen)
	{
		Screen.fullScreen = isfullscreen;
	}
} 		

