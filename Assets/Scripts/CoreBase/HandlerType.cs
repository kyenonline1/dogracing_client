using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBase
{
    enum HandlerType
    {
        /// <summary>
        /// Controller handler xử lý các UI Event gửi từ View xuống Controller
        /// </summary>
        EVN_VIEW_HANDLER,

        /// <summary>
        /// Controller handler xử lý các Push Event
        /// </summary>
        EVN_PUSH_HANDLER,

        /// <summary>
        /// View Handler xử lý các Update View gửi từ Controller lên View
        /// </summary>
        EVN_UPDATEUI_HANDLER
    }
}
