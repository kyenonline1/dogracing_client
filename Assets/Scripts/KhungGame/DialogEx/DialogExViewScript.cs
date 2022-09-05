using AssetBundles;
using DG.Tweening;
using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Popup;

namespace View.DialogEx
{
    public class DialogExViewScript : MonoBehaviour
    {
        public static DialogExViewScript Instance;

        [SerializeField]
        private GameObject Loading;
        [SerializeField]
        private GameObject LoadingScreen;
        [SerializeField]
        private GameObject Notify;
        [SerializeField]
        private Text txtNotify;
        [SerializeField]
        private Transform TranPrioritize;
        [SerializeField]
        private Transform TranImportant;
        [SerializeField]
        private Transform TranNomal;
        
        private Coroutine mIEnumNoti = null;
        private Coroutine mIEnumLoading = null;
        private Coroutine mIEnumLoadingScreen = null;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(this);
            Instance = this;
        }

        public enum depth
        {
            nomarl,
            important,
            prioritize,
        }

        /// <summary>
        /// show message dialog with one button
        /// </summary>
        /// <param name="prefab">Prefab Popup</param>
        /// <param name="Msg">message to show</param>
        /// <param name="Tittle">Tittle Dialog</param>
        /// <param name="OkClick">if null, do not show OK button</param>
        /// <param name="CancelClick">if null do not show Cancel button</param>
        /// <param name="Cancelable">if true, press outside popupview can dismiss dialog</param>
        /// <param name="depth">depth dialog</param>
        public void ShowDialog(/*GameObject prefab,*/ string Msg, Action OkClick = null, string Tittle = "THÔNG BÁO", string OkLabel = "ĐỒNG Ý", bool Cancelable = true, depth depth = depth.nomarl, float time = -1f)
        {
            Transform parent = null;
            switch (depth)
            {
                case depth.nomarl:
                    parent = TranNomal;
                    break;
                case depth.important:
                    parent = TranImportant;
                    break;
                case depth.prioritize:
                    parent = TranPrioritize;
                    break;
                default:
                    parent = transform;
                    break;
            }

            var popup = PoolManager.Pools["PopupView"].Spawn("DialogEx", Vector3.zero, Quaternion.identity, parent);

            //LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_KHUNGGAME, "DialogEx", (popup) =>
            //{
            if (popup == null) return;
            popup.transform.SetParent(parent);
            popup.GetComponent<RectTransform>().localScale = Vector3.one;
            popup.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            PopupViewScript script = popup.GetComponent<PopupViewScript>();
            script.WAIT_TIME = time;
            script.txt_Title.text = Tittle;//"THÔNG BÁO";// ClientConfig.Language.GetText(Tittle);
            script.txt_Content.text = Msg;
            script.ButtonOk.gameObject.SetActive(true);
            script.ButtonCancel.gameObject.SetActive(false);
            script.ButtonOkFull.gameObject.SetActive(false);
            script.ButtonOk.GetComponentInChildren<Text>().text = OkLabel;//"ĐỒNG Ý";// ClientConfig.Language.GetText(OkLabel);
            script.acctionOkeClicked = OkClick;
            script.ButtonCancel.gameObject.SetActive(false);
            script.OnShow();
            //}, () =>
            //{
            //    Debug.LogError("LoadAB Popup not found!");
            //});
        }

        /// <summary>
        /// Show pop up Ok Cancel
        /// </summary>
        /// <param name="Msg">Content</param>
        /// <param name="OkClick">func ok </param>
        /// <param name="CancelClick">func cancel</param>
        /// <param name="Tittle">Tittle</param>
        /// <param name="OkLabel">OK text</param>
        /// <param name="CancelLabel">cancel Text</param>
        /// <param name="Cancelable"></param>
        /// <param name="depth">do uu tien</param>
		public void ShowFullPopup(/*GameObject prefab,*/  string Msg, Action OkClick = null, Action CancelClick = null, string Tittle = "THÔNG BÁO", string OkLabel = "HOME_DIALOG_OKE", string CancelLabel = "HOME_DIALOG_CANCEL", bool Cancelable = true, depth depth = depth.nomarl, float time = -1f, Action CallbackNoAction = null)
        {
            Transform parent = null;
            switch (depth)
            {
                case depth.nomarl:
                    parent = TranNomal;
                    break;
                case depth.important:
                    parent = TranImportant;
                    break;
                case depth.prioritize:
                    parent = TranPrioritize;
                    break;
                default:
                    parent = transform;
                    break;
            }

            var popup = PoolManager.Pools["PopupView"].Spawn("DialogEx", Vector3.zero, Quaternion.identity, parent);

            //LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_KHUNGGAME, "DialogEx", (popup) =>
            //{
            if (popup == null) return;
            popup.transform.SetParent(parent);
            popup.GetComponent<RectTransform>().localScale = Vector3.one;
            //popup.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            popup.transform.localPosition = Vector3.zero;

            PopupViewScript script = popup.GetComponent<PopupViewScript>();
            script.CallbackNoAction = CallbackNoAction;
            script.WAIT_TIME = time;
            script.txt_Title.text = Tittle;// "THÔNG BÁO";//ClientConfig.Language.GetText(Tittle);
            script.txt_Content.text = Msg;
            script.ButtonOk.gameObject.SetActive(false);
            script.ButtonOkFull.gameObject.SetActive(true);
            script.ButtonOkFull.GetComponentInChildren<Text>().text = Languages.Language.GetKey(OkLabel);// "ĐỒNG Ý";// ClientConfig.Language.GetText(OkLabel);

            script.acctionOkeClicked = OkClick;
            script.acctionCancelClicked = CancelClick;
            script.ButtonCancel.gameObject.SetActive(true);
            script.ButtonCancel.GetComponentInChildren<Text>().text = Languages.Language.GetKey(CancelLabel); ;// "ĐÓNG";// ClientConfig.Language.GetText(CancelLabel);
           
            script.OnShow();
            //}, () =>
            //{
            //    Debug.LogError("LoadAB Popup not found!");
            //});
        }


        /// <summary>
        /// Show Loading
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowLoading(bool isShow)
        {
            try
            {
                if (mIEnumLoading != null)
                {
                    StopCoroutine(mIEnumLoading);
                    mIEnumLoading = null;
                }
                //if (!isShow) return;
                if (isShow) {
                    mIEnumLoading = StartCoroutine(IECloseLoading());
                }

                Loading.SetActive(isShow);
            }
            catch (System.Exception e)
            {
                Debug.LogError("exception : " + e.StackTrace);
            }
        }

        IEnumerator IECloseLoading()
        {
            yield return new WaitForSeconds(20);
            if (Loading != null)
            {
                Loading.SetActive(false);
            }
        }

        private bool isShowloading;

        /// <summary>
        /// Show Loading
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowLoadingScreen(bool isShow)
        {
            try
            {
                //Debug.Log("ShowLoadingScreen: " + isShow);

                if (mIEnumLoadingScreen != null)
                {
                    StopCoroutine(mIEnumLoadingScreen);
                    mIEnumLoadingScreen = null;
                }
                //if (!isShow) return;
                if (isShow)
                {
                    mIEnumLoadingScreen = StartCoroutine(IECloseLoadingScreen());
                    if (LoadingScreen != null) LoadingScreen.GetComponent<Animator>().Play("AnimShowLoading", 0, 0);
                }
                else
                {
                    if (isShowloading)
                    {
                        if (LoadingScreen != null)
                        {
                            //Loading.SetActive(isShow);
                            LoadingScreen.GetComponent<Animator>().Play("AnimHideLoading", 0, 0);
                        }
                    }
                }

                isShowloading = isShow;
            }
            catch (System.Exception e)
            {
                Debug.LogError("exception : " + e.StackTrace);
            }
        }

        IEnumerator IECloseLoadingScreen()
        {
            yield return new WaitForSeconds(20);
            if (LoadingScreen != null && isShowloading)
            {
                //Loading.SetActive(false);
                LoadingScreen.GetComponent<Animator>().Play("AnimHideLoading", 0, 0);
            }
        }




        /// <summary>
        /// Show notification
        /// </summary>
        /// <param name="Msg"></param>
        public void ShowNotification(string Msg)
        {
            if (Notify != null)
            {
                ShowNotify(Msg);
            }
        }

        private void ShowNotify(string msg)
        {
            if (mIEnumNoti != null)
            {
                StopCoroutine(mIEnumNoti);
                mIEnumNoti = null;
            }
            mIEnumNoti = StartCoroutine(IEShowNotify(msg));
        }

        private IEnumerator IEShowNotify(string msg)
        {
            Notify.gameObject.SetActive(true);
            txtNotify.text = msg;
            Notify.transform.DOScale(1, .3f).SetEase(Ease.InOutBack);
            yield return new WaitForSeconds(4f);
            Notify.gameObject.SetActive(false);
        }
    }
}
