using System.Collections;
using AppConfig;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Custom;

namespace CoreBase.Extention
{
    
    public class DialogEx
    {

        private static GameObject mCanvasObj;
        private static GameObject mNotify;
        private static Coroutine mIEnum = null;
        private static GameObject mLoading;// mPopupFull,mPopupDialog;
        static DialogEx()
        {
            ////NGUIDebug.Log("check MonoBehaviourHelper: instance = " + (instance == null) + " --------------------");
            if (mCanvasObj == null)
            {
                mCanvasObj = new GameObject("_PopupCanvasObject");
                
                Canvas _canvas = mCanvasObj.AddComponent<Canvas>();
                _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                _canvas.sortingOrder = 100;
                _canvas.targetDisplay = 0;
                CanvasScaler scaler = mCanvasObj.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(640, 1136);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 1;
                mCanvasObj.AddComponent<GraphicRaycaster>();
                Object.DontDestroyOnLoad(mCanvasObj);
            }
        }

        public delegate void OnButonClick();
        public enum depth
        {
            nomarl,
            important,
            prioritize
        }
        // private OnButonClick onOKClick, onCancelClick;
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
        public static void ShowDialog(/*GameObject prefab,*/ string Msg, OnButonClick OkClick = null,  string Tittle= "popup_tittle_thongbao", string OkLabel = "popup_ok_dongy",  bool Cancelable = true, depth depth =depth.important)
        {
            //instancetiate popup object, set message, onclick event
            //GameObject prefab = new GameObject() ;// prefab of dialog, alpha = 0 by default\
            // if (prefab == null)
            // return;
            GameObject mPopupFull = Resources.Load("Popup") as GameObject;
            if(mPopupFull == null)
            {
                Debug.Log("popup is null");
            }
            //GameObject popup = null;
            //if(mPopupFull != null)
            //{
            //    mPopupFull.transform.SetSiblingIndex((int)depth);
            //    PopupViewScript script = mPopupFull.GetComponent<PopupViewScript>();
            //    script.mtxt_Title.text = ClientConfig.Language.GetText("popup_tittle_thongbao");
            //    script.mtxt_Content.text = Msg;
            //    script.mButtonOk.gameObject.SetActive(true);
            //    script.mButtonOk.GetComponentInChildren<Text>().text = ClientConfig.Language.GetText("popup_ok_dongy");
            //    script.mButtonOk.transform.localPosition = new Vector3(0, -150, 0);
            //    if (OkClick != null)
            //    {
            //        script.mButtonOk.onClick.AddListener(() => OkClick());
            //    }
            //    script.mButtonCancel.gameObject.SetActive(false);
            //    script.Show();
            //}
            //else
            //{
            //Debug.LogError("111111111111111111111111111111");
            //LoadAssetBundle.LoadPrefab("prefab", "Popup", (mPopupFull) => {
                if (mPopupFull == null) return;
                mPopupFull.transform.parent = mCanvasObj.transform;
                mPopupFull.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                mPopupFull.GetComponent<RectTransform>().localScale = Vector3.one;
                mPopupFull.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                mPopupFull.transform.SetSiblingIndex((int)depth);

               // PopupViewScript script = mPopupFull.GetComponent<PopupViewScript>();
               // script.mtxt_Title.text = ClientConfig.Language.GetText(Tittle);
               // script.mtxt_Content.text = Msg;
               // script.mButtonOk.gameObject.SetActive(true);
               // script.mButtonOk.GetComponentInChildren<Text>().text = ClientConfig.Language.GetText(OkLabel);
               // script.mButtonOk.transform.localPosition = new Vector3(0, -150, 0);
               // if (OkClick != null)
               // {
               //     script.mButtonOk.onClick.AddListener(() => OkClick());
               // }
               // script.mButtonCancel.gameObject.SetActive(false);
               //// Debug.LogError("2222222222222222222222222222222");
               // script.Show();
            //});
            //}
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
        public static void ShowFullPopup(/*GameObject prefab,*/  string Msg, OnButonClick OkClick = null, OnButonClick CancelClick = null, string Tittle = "popup_tittle_thongbao", string OkLabel = "popup_ok_dongy", string CancelLabel = "popup_cancel_huy", bool Cancelable = true, depth depth = depth.important)
        {
            // if (prefab == null)
            //    return;
            //GameObject popup = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            // GameObject popup = null;
            //if(mPopupFull != null)
            //{
            //    mPopupFull.transform.SetSiblingIndex((int)depth);

            //    PopupViewScript script = mPopupFull.GetComponent<PopupViewScript>();
            //    script.mtxt_Title.text = ClientConfig.Language.GetText("popup_tittle_thongbao");
            //    script.mtxt_Content.text = Msg;
            //    script.mButtonOk.gameObject.SetActive(true);
            //    script.mButtonOk.GetComponentInChildren<Text>().text = ClientConfig.Language.GetText("popup_ok_dongy");
            //    script.mButtonOk.transform.localPosition = new Vector3(125, -150, 0);
            //    if (OkClick != null)
            //    {
            //        script.mButtonOk.onClick.AddListener(() => OkClick());
            //    }
            //    script.mButtonCancel.gameObject.SetActive(true);
            //    script.mButtonCancel.GetComponentInChildren<Text>().text = ClientConfig.Language.GetText("popup_cancel_huy");
            //    if (CancelClick != null)
            //    {
            //        script.mButtonCancel.onClick.AddListener(() => CancelClick());
            //    }
            //    script.Show();
            //}
            //else
            //{
            GameObject mPopupFull = Resources.Load("Popup") as GameObject;
            if (mPopupFull == null)
            {
                Debug.Log("popup is null");
            }
            //LoadAssetBundle.LoadPrefab("prefab", "Popup", (mPopupFull) =>
              //  {
                    if (mPopupFull == null) return;
                    mPopupFull.transform.parent = mCanvasObj.transform;
                    mPopupFull.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                    mPopupFull.GetComponent<RectTransform>().localScale = Vector3.one;
                    mPopupFull.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    mPopupFull.transform.SetSiblingIndex((int)depth);

                    //PopupViewScript script = mPopupFull.GetComponent<PopupViewScript>();
                    //script.mtxt_Title.text = ClientConfig.Language.GetText(Tittle);
                    //script.mtxt_Content.text = Msg;
                    //script.mButtonOk.gameObject.SetActive(true);
                    //script.mButtonOk.GetComponentInChildren<Text>().text = ClientConfig.Language.GetText(OkLabel);
                    //script.mButtonOk.transform.localPosition = new Vector3(125, -150, 0);
                    //if (OkClick != null)
                    //{
                    //    script.mButtonOk.onClick.AddListener(() => OkClick());
                    //}
                    //script.mButtonCancel.gameObject.SetActive(true);
                    //script.mButtonCancel.GetComponentInChildren<Text>().text = ClientConfig.Language.GetText(CancelLabel);
                    //if (CancelClick != null)
                    //{
                    //    script.mButtonCancel.onClick.AddListener(() => CancelClick());
                    //}
                    //script.Show();
                //});
            //}
            
        }
        /// <summary>
        /// Show Loading
        /// </summary>
        /// <param name="isShow"></param>
        public static void ShowLoading(bool isShow)
        {
			try{
				if(mLoading != null)
				{
					mLoading.SetActive(isShow);
				}
				else
				{
					//LoadAssetBundle.LoadPrefab("prefab", "Loading", (loading) => {
					//	if (loading == null) return;
					//	mLoading = loading;
					//	mLoading.transform.parent = mCanvasObj.transform;
					//	mLoading.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
					//	mLoading.GetComponent<RectTransform>().localScale = Vector3.one;
					//	mLoading.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
					//	mLoading.transform.SetAsFirstSibling();
					//	mLoading.SetActive(isShow);
					//});
				}
			}catch(System.Exception e){
				Debug.LogError ("exception : " + e.StackTrace);
			}
        }
        /// <summary>
        /// Show notification
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowNotification( string Msg)
        {
            if(mNotify != null)
            {
                ShowNotify(Msg);
            }
            else
            {
                //LoadAssetBundle.LoadPrefab("prefab", "Notify", (notify) => {
                //    if (notify == null) return;
                //    mNotify = notify;
                //    mNotify.transform.parent = mCanvasObj.transform;
                //    mNotify.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                //    mNotify.GetComponent<RectTransform>().localScale = Vector3.one;
                //    mNotify.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 700);
                //    ShowNotify(Msg);
                //});
            }
        }
        private static void ShowNotify(string msg)
        {
            if(mIEnum != null)
            {
                MonoInstance.Instance.StopCoroutine(mIEnum);
                mIEnum = null;
            }
            mIEnum = MonoInstance.Instance.StartCoroutine(IEShowNotify(msg));
        }
        private static IEnumerator IEShowNotify(string msg)
        {
            mNotify.gameObject.SetActive(true);
            mNotify.GetComponentInChildren<Text>().text = msg;
            mNotify.transform.DOLocalMoveY(500, .3f);
            yield return new WaitForSeconds(3f);
            mNotify.transform.DOLocalMoveY(700, .3f).OnComplete(()=> { mNotify.gameObject.SetActive(false); });
        }
    }
}
