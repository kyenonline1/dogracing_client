
using UnityEngine;
using UnityEngine.UI;
using Utilities.Custom;

namespace View.Common.Help
{
    public class HelpInGameView : MonoBehaviour
    {

        [SerializeField]
        private Text txtTittle;
        [SerializeField]
        private Text txtContent;
        [SerializeField]
        private UIMyButton btnClose;

        //bool isFillContent = false;

        private void Awake()
        {
            btnClose._onClick.AddListener(CloseHelp);
        }

        public void OpenHelp()
        {
            gameObject.SetActive(true);
            //if (!isFillContent)
            //{
            //    //string gameid = (string)DataDispatcher.Instance().GetExtra(Game.Gameconfig.GameConfig.KEY_DATADISPATCHER.SCENE_LOBBY, Game.Gameconfig.GameConfig.KEY_DATADISPATCHER.KEY_GAMEID);
            //    //txtTittle.text = Languages.Language.GetKey(string.Format("HELP_GAMEID_{0}", gameid));
            //    //txtContent.text = Languages.Language.GetKey(string.Format("HELP_{0}", gameid));
            //}
        }

        private void CloseHelp()
        {
            gameObject.SetActive(false);
        }

    }
}
