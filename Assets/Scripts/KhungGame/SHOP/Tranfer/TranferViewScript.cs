using CoreBase;
using CoreBase.Controller;
using GameProtocol.DIS;
using Interface;
using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utilites;

namespace View.Home.Shop
{
    public class TranferViewScript : ViewScript
    {
        [Header("Bán Đại lý")]
        [SerializeField] private InputField ipfUserAgency;
        [SerializeField] private InputField ipfMoneyTranferAgency;
        [SerializeField] private Text txtValueReceivedAgency;
        [SerializeField] private InputField ipfDescriptionAgency;
        [SerializeField] private InputField ipfCapchaAgency;
        [SerializeField] private Image imgCapchaAgency;

        [SerializeField] private Transform parentAgencys;

        /// <summary>
        /// Dsach những user mình sẽ ck đến
        /// </summary>
        private List<string> useridReceiced;
        /// <summary>
        /// Dsach tiền tương ứng ck đến user
        /// </summary>
        private List<int> goldTranfer;
        /// <summary>
        /// Ds nội dung ck
        /// </summary>
        private List<string> descriptions;
        private Distributor[] agencys;
        private float fee;

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        protected override IController CreateController()
        {
            return new TranferController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            //FakeAgengy();
            callbackOpenPopup = RequestListDaiLy;
            useridReceiced = new List<string>();
            goldTranfer = new List<int>();
            descriptions = new List<string>();

            if (ipfUserAgency) ipfUserAgency.onValueChanged.AddListener(OnValueChangeUserAgency);
            if (ipfMoneyTranferAgency) ipfMoneyTranferAgency.onValueChanged.AddListener(OnValueChangeMoneyTranfer);
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        private void OnEnable()
        {
            if (txtValueReceivedAgency) txtValueReceivedAgency.text = "0";
            OpenPopup();
        }

        private void OnDisable()
        {
            RecyclePool();
        }

        private void RequestListDaiLy()
        {
            RequestCapcha();
            if (agencys != null)
            {
                //useridReceiced.Clear();
                //Debug.Log("RequestListDaiLy useridReceiced: " + useridReceiced.Count);
                ShowItemDaiLy();
                return;
            }
            Controller.OnHandleUIEvent("RequestListDaiLy");
        }

        private void RequestCapcha()
        {
            Controller.OnHandleUIEvent("RequestCapcha");
        }

        private void RecyclePool()
        {
            PoolManager.Pools["AgencySmall"].DespawnAll();
        }

        void ShowItemDaiLy()
        {
            for (int i = 0; i < agencys.Length; i++)
            {
                var go = PoolManager.Pools["AgencySmall"].Spawn("ItemAgencySmall", Vector3.zero, Quaternion.identity, parentAgencys);

                ItemDaiLyView itemDaiLyView = go.GetComponent<ItemDaiLyView>();
                if (itemDaiLyView)
                {
                    itemDaiLyView.gameObject.SetActive(true);
                    itemDaiLyView.SetData(i, agencys[i].DistributorName, agencys[i].Phone, agencys[i].Nickname, agencys[i].FacebookUrl,
                         agencys[i].Zalo, agencys[i].Telegram);
                    itemDaiLyView.dlgTranfer = null;
                    itemDaiLyView.dlgTranfer += DlgShowTranfer;
                }
                else PoolManager.Pools["AgencyBig"].Despawn(go.transform);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearData(object[] param)
        {
            useridReceiced.Clear();
            descriptions.Clear();
            goldTranfer.Clear();
            if (ipfUserAgency) ipfUserAgency.text = string.Empty;
            if (ipfCapchaAgency) ipfCapchaAgency.text = string.Empty;
            if (ipfMoneyTranferAgency) ipfMoneyTranferAgency.text = string.Empty;
            if (ipfDescriptionAgency) ipfDescriptionAgency.text = string.Empty;
        }
        
        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowListDaiLy(object[] param)
        {
            ShowLoading(false);
            agencys = (Distributor[])param[0];
            fee = (float)param[1];
            if (agencys != null)
            {
                ShowItemDaiLy();
                return;
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowCapcha(object[] param)
        {
            ShowLoading(false);
            string urlcapcha = (string)param[0];
            StartCoroutine(LoadCapcha(urlcapcha));
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
                        Texture2D t = texDl.texture;
                        Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                                                 Vector2.zero, 1f);
                        imgCapchaAgency.sprite = s;
                    }
                    else ShowNotification(Languages.Language.GetKey("HOME_DIALOG_LOADCAPCHA_ERROR"));
                }
            }
            yield return null;
        }


        private void DlgShowTranfer(string name, string nickname)
        {
            useridReceiced.Clear();
            useridReceiced.Add(nickname);
            if (ipfUserAgency) ipfUserAgency.text = name;
            //Debug.Log("DlgShowTranfer useridReceiced: " + useridReceiced.Count);
        }

        private void OnValueChangeUserAgency(string value)
        {
            //Debug.Log("OnValueChangeUserAgency: " + value);
            useridReceiced.Clear();
            if (!string.IsNullOrEmpty(value))
            {
                useridReceiced.Add(value);
            }
            //Debug.Log("useridReceiced: " + useridReceiced.Count);
        }

        private void OnValueChangeMoneyTranfer(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int money = int.Parse(value);
                if (txtValueReceivedAgency) txtValueReceivedAgency.text = MoneyHelper.FormatNumberAbsolute(Mathf.RoundToInt(money * (fee - 1)));
            }
            else if (txtValueReceivedAgency) txtValueReceivedAgency.text = "0";
        }

        private void ShowLoading(bool isShow)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(isShow);
        }
        
        private void ShowNotification(string message)
        {
            DialogEx.DialogExViewScript.Instance.ShowNotification(message);
        }

        public void OnBtnTranferAgencyClicked()
        {
            //Debug.Log("OnBtnTranferAgencyClicked useridReceiced: " + useridReceiced.Count);
            if (useridReceiced == null || useridReceiced.Count < 1)
            {
                //TODO: Show error
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_USER_RECEICED"));
                return;
            }

            goldTranfer.Clear();
            string value = ipfMoneyTranferAgency.text;
            if (!string.IsNullOrEmpty(value)) goldTranfer.Add(int.Parse(value));

            if (goldTranfer == null && goldTranfer.Count < 1)
            {
                //TODO: Show error
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_MONEY_RECEICED"));
                return;
            }

            descriptions.Clear();
            string des = ipfDescriptionAgency.text;
            if (!string.IsNullOrEmpty(des)) descriptions.Add(des);

            if (descriptions == null && descriptions.Count < 1)
            {
                //TODO: Show error
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_DESCRIPTION_RECEICED"));
                return;
            }

            string capcha = ipfCapchaAgency.text;
            if (string.IsNullOrEmpty(capcha))
            {
                //TODO: Show error
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_CAPCHA"));
                return;
            }

            ShowLoading(true);
            RequestCapcha();
            Controller.OnHandleUIEvent("RequestTranferCoin", new object[] { useridReceiced.ToArray(), goldTranfer.ToArray(), descriptions.ToArray(), capcha});
        }

        public void ShowUserTranfer(string username, float fee, string nickname)
        {
            this.fee = fee;
            if (ipfUserAgency) ipfUserAgency.text = username;
            //Debug.Log("ShowUserTranfer useridReceiced: " + (useridReceiced != null));
            if (useridReceiced != null)
            {
                useridReceiced.Clear();
                useridReceiced.Add(nickname);
            }
            else
            {
                useridReceiced = new List<string>();
                useridReceiced.Add(nickname);
            }
        }

        public void OnBtnCapchaClicked()
        {
            ShowLoading(true);
            RequestCapcha();
        }


        private void FakeAgengy()
        {

            List<string> lstName = new List<string>()
            {
                "Vip Poker Master",
                "Đại Lý Master",
                "Đại lý thần tốc",
                "Hệ thống thần tốc",
                "Tuấn Sài Gòn",
                "Hưng Hà Nội",
                "Vip Toàn Quốc",
                "Vip Đại Lý PokerMaster",
                "Bình Gold",
                "Tập đoàn Hưng Thịnh",
            };

            List<string> lstNickName = new List<string>()
            {
                "pokermaster",
                "dailymaster",
                "dailythantoc",
                "thantoc30s",
                "tuansaigon",
                "hunghanoi",
                "viptoanquoc",
                "msminhtrang",
                "binhgold",
                "tapdoanhungthinh",
            };

            List<string> sdt = new List<string>()
            {
                "094999999",
                "091111111",
                "094222222",
                "096666666",
                "094333333",
                "094444444",
                "094555555",
                "094666666",
                "094777777",
                "094888888",
            };

            List<Distributor> lsDL = new List<Distributor>();

            for (int i = 0; i < 10; i++)
            {
                var dl = new Distributor();
                dl.Nickname = lstNickName[i];
                dl.DistributorName = lstName[i];
                dl.Phone = sdt[i];
                dl.FacebookUrl = "https://www.24h.com.vn/";
                dl.Zalo = "https://www.24h.com.vn/";
                dl.Telegram = "https://www.24h.com.vn/";
                lsDL.Add(dl);
            }
            agencys = lsDL.ToArray();
        }
    }
}
