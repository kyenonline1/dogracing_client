using AppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CoreBase.Extention;
using Game.Gameconfig;

namespace CoreBase
{
    public class ScreenScript : PopupScript
    {

        //public GameObject UIRoot;
        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            //RegisterUpdateEvent(OnReconnected);
            //RegisterUpdateEvent(HideReconnecting);
            //RegisterUpdateEvent(OpenReconnecting);
            //RegisterUpdateEvent(OpenRetryPopup);
            //RegisterUpdateEvent(OpenLoadingProgress);
            //RegisterUpdateEvent(HideLoadingProgress);
            Show();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }
        protected virtual void OnLoginSuccess(params object[] parameters)
        {
            ClientGameConfig.isAutoReconnect = false;
        }
        protected virtual void OnAutoLoginSuccess(params object[] parameters)
        {
			//Debug.LogError ("OnAutoLoginSuccess");
            //PushNotification.OnCreate();
        }
        protected virtual void OnReconnected(params object[] parameters)
        {
            ClientGameConfig.isAutoReconnect = false;
            //Debug.LogError ("OnReconnected");
            //PushNotification.OnCreate();
        }
        protected virtual void OpenReconnecting(params object[] parameters)
        {

        }
        protected virtual void HideReconnecting(params object[] parameters)
        {

        }
        protected virtual void OpenLoadingProgress(params object[] parameters)
        {

        }
        protected virtual void HideLoadingProgress(params object[] parameters)
        {

        }
        protected virtual void OpenRetryPopup(params object[] parameters)
        {

        }
        protected override void OnShow()
        {
        }

        protected override void OnClose()
        {
        }

        /// <summary>
        /// show message dialog with one or 2 button
        /// </summary>
        /// <param name="Msg">message to show</param>
        /// <param name="OkClick">if null, do not show OK button</param>
        /// <param name="CancelClick">if null do not show Cancel button</param>
        /// <param name="Cancelable">if true, press outside popupview can dismiss dialog</param>
        /*protected void ShowDialog(string Msg, string OkLabel = "OK", EventDelegate.Callback OkClick = null, string CancelLabel = "Cancel", EventDelegate.Callback CancelClick = null, bool Cancelable = true)
        {
            //instancetiate popup object, set message, onclick event
            DialogEx.ShowDialog(UIRoot, Msg, OkLabel, OkClick, CancelLabel, CancelClick, Cancelable);
        }*/
        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
            Close();
        }
    }
}
