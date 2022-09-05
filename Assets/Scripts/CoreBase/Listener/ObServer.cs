using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Listener
{
    /// <summary>
    /// data có thể null or ko, cần check null
    /// </summary>
    /// <param name="data"></param>
    public delegate void ObServer(Texture2D data);
}
