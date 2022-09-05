using Base.Utils;
using MiniGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestroyObject : MonoBehaviour {
    protected static DonotDestroyObject Instance;
    private GameObject btnSlot,
        //btnMiniGame,
        MiniGame;
    //[SerializeField]
    //private MiniGameLoader miniGameLoader;


    // Use this for initialization
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        //DontDestroyOnLoad(this.gameObject);
        RunAwakeEvent();
    }


    void Start () {

        //EventManager.Instance.SubscribeTopic(EventManager.ENABLE_MINIGAME, EnableObject);
        //EventManager.Instance.SubscribeTopic(EventManager.DISABLE_MINIGAME, DisableObject);
    }
	
    private void RunAwakeEvent()
    {
        MiniGame = transform.Find("MiniGame").gameObject;
        //btnMiniGame = transform.Find("MiniGame/btnSlotPoker").gameObject;
        btnSlot = transform.Find("MiniGame/btnMiniGame").gameObject;
    }

    private void EnableObject()
    {
        MiniGame.SetActive(true);
        //btnMiniGame.gameObject.SetActive(true);
        btnSlot.gameObject.SetActive(true);
       // StartCoroutine(miniGameLoader.EnableMiniGame());
    }

    private void DisableObject()
    {
        MiniGame.SetActive(false);
        //btnMiniGame.gameObject.SetActive(false);
        btnSlot.gameObject.SetActive(false);
    }
}
