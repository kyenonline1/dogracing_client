
using UnityEngine;
using UnityEngine.UI;

namespace View.HelpGame
{
    public class HelpGameViewScript : MonoBehaviour
    {

        private UIMyButton btnClose;
        private UIMyButton btnHelp;
        private UIMyButton btnRule;
        //private Text     txtContent;

        private ScrollRect scrollButton;
        private UIMyButton btnXocDia;
        private UIMyButton btnMauBinh;
        private UIMyButton btnPoker;
        private UIMyButton btnLieng;
        private UIMyButton btnXiTo;
        private UIMyButton btnBaCay;
        private UIMyButton btnXam;
        private UIMyButton btnTLMN;
        private UIMyButton btnTLMB;
        private UIMyButton btnPhom;


        private enum HelpGame
        {
            XOCDIA,
            MAUBINH,
            POKER,
            LIENG,
            XITO,
            BACAY,
            XAM,
            TLMN,
            TLMB,
            PHOM
        }

        //private HelpGame curentGame = HelpGame.XOCDIA;

        // Use this for initialization
        private void Awake()
        {
            InitProperties();
        }

        private void Start()
        {
            AddListenerButton();
        }

        private void InitProperties()
        {
            btnClose = transform.Find("Popup/BtnClose").GetComponent<UIMyButton>();
            btnHelp = transform.Find("Popup/Content/BtnHelp").GetComponent<UIMyButton>();
            btnRule = transform.Find("Popup/Content/BtnRule").GetComponent<UIMyButton>();
            //txtContent = transform.Find("Popup/Content/bgRule/Text").GetComponent<Text>();

            scrollButton = transform.Find("Popup/lstButtonGame/ScrollView").GetComponent<ScrollRect>();
            btnXocDia = scrollButton.transform.Find("Viewport/Content/BtnXocDia").GetComponent<UIMyButton>();
            btnMauBinh = scrollButton.transform.Find("Viewport/Content/BtnMauBinh").GetComponent<UIMyButton>();
            btnPoker = scrollButton.transform.Find("Viewport/Content/BtnPoker").GetComponent<UIMyButton>();
            btnLieng = scrollButton.transform.Find("Viewport/Content/BtnLieng").GetComponent<UIMyButton>();
            btnXiTo = scrollButton.transform.Find("Viewport/Content/BtnXiTo").GetComponent<UIMyButton>();
            btnBaCay = scrollButton.transform.Find("Viewport/Content/BtnBaCay").GetComponent<UIMyButton>();
            btnXam = scrollButton.transform.Find("Viewport/Content/BtnXam").GetComponent<UIMyButton>();
            btnTLMN = scrollButton.transform.Find("Viewport/Content/BtnTLMN").GetComponent<UIMyButton>();
            btnTLMB = scrollButton.transform.Find("Viewport/Content/BtnTLMB").GetComponent<UIMyButton>();
            btnPhom = scrollButton.transform.Find("Viewport/Content/BtnPhom").GetComponent<UIMyButton>();
        }

        private void AddListenerButton()
        {
            btnClose._onClick.AddListener(BtnClickClose);
            btnHelp._onClick.AddListener(BtnClickHelp);
            btnRule._onClick.AddListener(BtnClickRule);
            btnXocDia._onClick.AddListener(BtnClickXocDia);
            btnMauBinh._onClick.AddListener(BtnClickXMauBinh);
            btnPoker._onClick.AddListener(BtnClickPoker);
            btnLieng._onClick.AddListener(BtnClickLieng);
            btnXiTo._onClick.AddListener(BtnClickXiTo);
            btnBaCay._onClick.AddListener(BtnClickBaCay);
            btnXam._onClick.AddListener(BtnClickXam);
            btnTLMN._onClick.AddListener(BtnClickTLMN);
            btnTLMB._onClick.AddListener(BtnClickTLMB);
            btnPhom._onClick.AddListener(BtnClickPhom);
        }

        private void BtnClickClose()
        {
            gameObject.SetActive(false);
        }

        private void BtnClickHelp()
        {

        }

        private void BtnClickRule()
        {

        }

        private void BtnClickXocDia()
        {
            //curentGame = HelpGame.XOCDIA;
        }

        private void BtnClickXMauBinh()
        {
           // curentGame = HelpGame.MAUBINH;
        }

        private void BtnClickPoker()
        {
            //curentGame = HelpGame.POKER;
        }

        private void BtnClickLieng()
        {
           // curentGame = HelpGame.LIENG;
        }

        private void BtnClickXiTo()
        {
            //curentGame = HelpGame.XITO;
        }

        private void BtnClickBaCay()
        {
           // curentGame = HelpGame.BACAY;
        }

        private void BtnClickXam()
        {
           // curentGame = HelpGame.XAM;
        }

        private void BtnClickTLMN()
        {
            //curentGame = HelpGame.TLMN;
        }

        private void BtnClickTLMB()
        {
           // curentGame = HelpGame.TLMB;
        }

        private void BtnClickPhom()
        {
            //curentGame = HelpGame.PHOM;
        }

        public void OpenHelp()
        {
            //curentGame = HelpGame.XOCDIA;
        }
    }
}
