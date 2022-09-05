using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interface
{
    public interface IController
    {
        void StartController();
        void StopController();
        void OnHandleUIEvent(string Function, params object[] Params);
    }
}
