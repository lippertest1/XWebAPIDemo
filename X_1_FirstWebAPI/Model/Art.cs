﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_1_FirstWebAPI.Model
{
    public class Art
    {
        public string artId { get; set; }
        public string artName { get; set; }
        public string openId { get; set; }
        public string picKey { get; set; }
        public string css { get; set; }
        public int borderId { get; set; }
        public string signKey { get; set; }
        public string uploadDate { get; set; }
        public string score { get; set; }
        public string scoreComment { get; set; }
        public string commentIdList { get; set; }
        public string dialogIdList { get; set; }

    }

    public class scoreCommentList
    {
        public int min { get; set; }
        public int max { get; set; }
        public string scoreComment { get; set; }
    }
}