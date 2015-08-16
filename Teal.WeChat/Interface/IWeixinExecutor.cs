﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teal.WeChat
{
    public interface IWeixinExecutor
    {
        /// <summary>
        /// 接受消息后返回XML
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string Execute(WeixinMessage message);

    }
}
