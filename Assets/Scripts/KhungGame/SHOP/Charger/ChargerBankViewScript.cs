using CoreBase;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.Shop
{
    public class ChargerBankViewScript : ViewScript
    {
        [SerializeField] private GameObject goFirst;

        [SerializeField] private InputField ipMoney;
        [SerializeField] private Image imgBankSelectd;
        [SerializeField] private Text txtBankNameSelected;
        [SerializeField] private InputField ipCapcha;
        [SerializeField] private Image imgCapcha;

        [SerializeField] private GameObject goNext;

        [SerializeField] private Text txtBankName;
        [SerializeField] private Text txtSTK;
        [SerializeField] private Text txtCTK;
        [SerializeField] private Text txtMoney;
        [SerializeField] private Text txtContent;

        [SerializeField] private Sprite[] sprBanks;
        [SerializeField] private ItemBanksView[] lsBankItems;
        [SerializeField] private GameObject goPanelSelectBank;

        [SerializeField]
        private CardRateView[] lstCardRatesBank;
        [SerializeField]
        private int[] pricesBank;

        [SerializeField]
        private GameObject objCopySuccess;

        private string stkBank, ctkBank, contentNap;

        private int indexBankSelected;

        //private BankInfo[] bankInfo;

        protected override IController CreateController()
        {
            return new ChargerBankController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();

            if (lsBankItems != null && lsBankItems.Length > 0)
            {
                for (int i = 0; i < lsBankItems.Length; i++)
                {
                    lsBankItems[i].SetSelected(i == indexBankSelected);
                }
            }
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            //StartCoroutine(Utils.DelayAction(RequestBankInfo, 0.01f));
        }

        private void RequestBankInfo()
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestBankInfo");
        }

        private void OnEnable()
        {
            StartCoroutine(Utils.DelayAction(RequestBankInfo, 0.01f));
            StartCoroutine(Utils.DelayAction(RequestCapcha, 0.01f));
        }

        private void RequestAccountBankInfo()
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestAccountBankInfo");
        }

        private void RequestCapcha()
        {
            Controller.OnHandleUIEvent("RequestCapcha");
        }

        private void ProcessShowSelectedBank(int index)
        {
            if (lsBankItems != null && lsBankItems.Length > 0)
            {
                for (int i = 0; i < lsBankItems.Length; i++)
                {
                    lsBankItems[i].SetSelected(i == index);
                }
            }
        }

        private void ActionSelectedBank(int index)
        {
            indexBankSelected = index;
            if (imgBankSelectd) imgBankSelectd.sprite = sprBanks[indexBankSelected];
            ProcessShowSelectedBank(indexBankSelected);
            //if (txtBankNameSelected) txtBankNameSelected.text = string.Format("Đang chọn: <color=yellow>{0}</color>", bankInfo[indexBankSelected].BankName);
        }

        private void RecycleItembanks()
        {
            if (lsBankItems != null && lsBankItems.Length > 0)
            {
                for (int i = 0; i < lsBankItems.Length; i++)
                {
                    lsBankItems[i].gameObject.SetActive(false);
                }
            }
        }

        public void OnBtnChangeBankClicked()
        {
            if (goPanelSelectBank) goPanelSelectBank.SetActive(true);
            ProcessShowSelectedBank(indexBankSelected);
        }

        public void OnBtnRequestCapchaClicked()
        {
            RequestCapcha();
        }


        public void OnBtnContinueClicked()
        {
            if (goFirst) goFirst.SetActive(false);
            if (goNext) goNext.SetActive(true);
            RequestAccountBankInfo();
        }

        public void OnBtnBackClicked()
        {
            if (goFirst) goFirst.SetActive(true);
            if (goNext) goNext.SetActive(false);
            RequestCapcha();
        }

        public void CopyStkBank()
        {
            if (string.IsNullOrEmpty(stkBank)) return;
            Clipboard.SetText(stkBank);
            objCopySuccess.SetActive(true);
            if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
            coroutineCoppy = StartCoroutine(HideCoppy());
        }

        public void CopyCTKBank()
        {
            if (string.IsNullOrEmpty(ctkBank)) return;
            Clipboard.SetText(ctkBank);
            objCopySuccess.SetActive(true);
            if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
            coroutineCoppy = StartCoroutine(HideCoppy());
        }

        public void CopyDescriptionBank()
        {
            if (string.IsNullOrEmpty(contentNap)) return;
            Clipboard.SetText(ctkBank);
            objCopySuccess.SetActive(true);
            if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
            coroutineCoppy = StartCoroutine(HideCoppy());
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowCapcha(object[] param)
        {
            string url = (string)param[0];
            StartCoroutine(LoadCapcha(url));
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowBankInfo(object[] param)
        {
            //bankInfo = (BankInfo[])param[0];
            //if (bankInfo != null)
            //{
            //    for (int i = 0; i < bankInfo.Length; i++)
            //    {
            //        lsBankItems[i].gameObject.SetActive(true);
            //        lsBankItems[i].SetIndex(bankInfo[i].BankId, sprBanks[bankInfo[i].BankId]);
            //        lsBankItems[i].callbackClickSelectBank = null;
            //        lsBankItems[i].callbackClickSelectBank = ActionSelectedBank;
            //    }

            //    ActionSelectedBank(0);
            //}
            DialogExViewScript.Instance.ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowAccountBankInfo(object[] param)
        {
            string bankname = (string)param[0];
            string accountname = (string)param[1];
            string accountid = (string)param[2];
            string tranfercontent = (string)param[3];
            float rate = (float)param[4];

            if (txtBankName) txtBankName.text = bankname;
            if (txtCTK) txtCTK.text = accountname;
            if (txtSTK) txtSTK.text = accountid;
            if (txtContent) txtContent.text = tranfercontent;

            for (int i = 0; i < lstCardRatesBank.Length; i++)
            {
                int index = i;
                lstCardRatesBank[index].ShowData(pricesBank[index], rate);
            }

            DialogExViewScript.Instance.ShowLoading(false);
        }

        private IEnumerator LoadCapcha(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                using (UnityWebRequest wr = new UnityWebRequest(url))
                {
                    DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
                    wr.downloadHandler = texDl;
                    yield return wr.SendWebRequest();
                    if (!(wr.isNetworkError || wr.isHttpError))
                    {
                        if (imgCapcha != null && imgCapcha.gameObject.activeSelf && gameObject.activeSelf && goFirst.activeSelf)
                        {
                            Texture2D t = texDl.texture;
                            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                                                     Vector2.zero, 1f);
                            if (imgCapcha != null && imgCapcha.gameObject.activeSelf) imgCapcha.sprite = s;
                        }
                    }
                    else DialogExViewScript.Instance.ShowNotification("Tải capcha thất bại");
                }
            }
            yield return null;
        }

        Coroutine coroutineCoppy;
        IEnumerator HideCoppy()
        {
            yield return new WaitForSeconds(3f);
            objCopySuccess.SetActive(false);
        }
    }
}