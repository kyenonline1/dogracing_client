using CoreBase.Controller;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Setting
{
    public class SettingController : UIController
    {
        public SettingController(IView view) : base(view)
        {
        }

        public override IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            return base.HandlePush(coderun, data);
        }

        public override void StartController()
        {
            base.StartController();
        }

        public override void StopController()
        {
            base.StopController();
        }

        public override void UnregisterPushHandlers()
        {
            base.UnregisterPushHandlers();
        }

        protected override void RegisterMessengers()
        {
            base.RegisterMessengers();
        }

        protected override void RegisterPushHandlers()
        {
            base.RegisterPushHandlers();
        }

        protected override void UnregisterMessengers()
        {
            base.UnregisterMessengers();
        }
    }
}