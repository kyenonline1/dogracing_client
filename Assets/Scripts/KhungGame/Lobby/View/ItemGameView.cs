using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Lobby
{
    public class ItemGameView : MonoBehaviour
    {

       // private string GameId;
        public bool isDownload;
        //public GameObject objDownload;
        public Image imgDownload;
        //public Slider slider;
        //public GameObject IconDownload;

        public void BtnClickItemGame()
        {
//            Debug.Log("BtnClickItemGame-----------: " + GameId);
//#if !ASSET_BUNDLE
//            isDownload = false;
//#endif
//            if (isDownload)
//            {
//                imgDownload.gameObject.SetActive(true);
//                LoadAssetBundle.DownloadAsset(LobbyViewScript.Instance.dicLoadGame[GameId], imgDownload, () =>
//                {
//                    //objDownload.SetActive(false);
//                    GetComponentInChildren<Image>().material = null;
//                    isDownload = false;
//                    imgDownload.gameObject.SetActive(false);
//                });
//                return;
//            }
//            if (eventClickListGame != null)
//            {
//                eventClickListGame(GameId);
//            }
        }

        public delegate void dlgClickListGame(string id);
        public dlgClickListGame eventClickListGame;

        public void SetData(string gameId)
        {
            //GameId = gameId;
        }
    }
}
