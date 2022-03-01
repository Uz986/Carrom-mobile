using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class adjustspeed : MonoBehaviour {

    public Slider mainSlider;

   
    StrickerTurn strickerturn;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        strickerturn = StrickerTurn.instance;
    }
     
    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()

    {
        
        strickerturn.force = mainSlider.value;
 
    }
}
