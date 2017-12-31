﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisInsideFormFlow.Models.Luis
{
    public class LUISOutput
    {
        public string query { get; set; }
        public LUISIntent[] intents { get; set; }
        public LUISEntity[] entities { get; set; }
    }
    public class LUISEntity
    {
        public string Entity { get; set; }
        public string Type { get; set; }
        public string StartIndex { get; set; }
        public string EndIndex { get; set; }
        public float Score { get; set; }
    }
    public class LUISIntent
    {
        public string Intent { get; set; }
        public float Score { get; set; }
    }
}