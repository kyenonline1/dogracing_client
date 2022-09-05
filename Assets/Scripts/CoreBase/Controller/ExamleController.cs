using UnityEngine;
using System.Collections;
using Interface;
using System.Collections.Generic;
using Utilities;
using AppConfig;

namespace CoreBase.Controller
{
    public class ExamleController : ScreenController
    {
        public ExamleController(IView view) : base(view)
        {
        }

        public override void StartController()
        {
            base.StartController();

            View.OnUpdateView("TestUpdateView", 1, "name");
            Debug.LogError("ExamleController : START NET WORK NET WORK");
            Network.Network.StartNetwork();
        }

        protected override void HandleNetworkReady(object Msg)
        {
            base.HandleNetworkReady(Msg);
            //UDT0_Request request = new UDT0_Request()
            //{
            //    CellId = "",
            //    Channel = ClientConfig.SoftWare.CHANNEL,
            //    DeviceName = ClientConfig.HardWare.DEVICE,
            //    Imei = ClientConfig.HardWare.IMEI,
            //    Lac = ClientConfig.HardWare.LAC,
            //    Language = ClientConfig.Language.LANG,
            //    MacAddr = ClientConfig.HardWare.MACADDRESS,
            //    Mcc = ClientConfig.HardWare.MCC,
            //    Mnc = ClientConfig.HardWare.MNC,
            //    Platform = ClientConfig.HardWare.PLATFORM,
            //    Utm_Medium = ClientConfig.SoftWare.UTM_MEDIUM,
            //    Utm_Campaign = "",
            //    Utm_Content = "",
            //    Utm_Term = "",
            //    Version = ClientConfig.SoftWare.VERSION,

            //};

            //Network.Network.SendOperation(request, new Listener.DataListener(UDT0_Handler));
        }

        IEnumerator UDT0_Handler(string coderun, Dictionary<byte, object> data)
        {
            yield return null;
        }

        #region UI Event
        [HandleUIEvent(EventType = HandlerType.EVN_VIEW_HANDLER)]
        void TestHandleUIEvent(params object[] parameters)
        {
			
        }
        #endregion

        #region Push
        [HandleUIEvent(EventType = HandlerType.EVN_PUSH_HANDLER, Name = "LGI")]
        IEnumerator TestHandlePush(Dictionary<byte, object> data)
        {
            int i = 0;
            while (i++ < 5)
            {
                LogMng.Log("TestHandlePush", "Count: " + i + ", " + data[1]);
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        #endregion
    }
}
