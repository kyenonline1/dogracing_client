using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Custom;

namespace CoreBase
{
    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    public class PopupScript : ViewScript, ICloseable
    {
        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }
        void AttachToStack()
        {
            MonoInstance.Instance.AttachIEscapable(this);
        }

        void DetachFromStack()
        {
			if(MonoInstance.Instance != null)
            MonoInstance.Instance.DetachIEscapable(this);
        }

        public void Show()
        {
            AttachToStack();
            OnShow();
        }

        protected virtual void OnShow()
        {
           // throw new NotImplementedException();
        }

        public bool Close()
        {

            DetachFromStack();
            OnClose();
            return true;
        }

        protected virtual void OnClose()
        {
            throw new NotImplementedException();
        }
    }
}
