using AppConfig;
using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowVersion : MonoBehaviour {

    private Text txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        txt.text = ClientGameConfig.Vesion;
    }
	
}
