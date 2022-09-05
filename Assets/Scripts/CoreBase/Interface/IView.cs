using UnityEngine;
using System.Collections.Generic;

namespace Interface
{
    public interface IView
    {
        void OnUpdateView(string Function, params object[] Params);
    }
}
