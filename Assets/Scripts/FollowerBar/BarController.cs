using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

    public int amountOfPeople;
    public Scrollbar followerbar;
    public Text followerBarText;

    public int peopleConvinced = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float amount = peopleConvinced / amountOfPeople;
        followerbar.size = amount;
        Debug.Log(amount);

        followerBarText.text = "People convinced: " + peopleConvinced + " / " + amountOfPeople;
	}
}
