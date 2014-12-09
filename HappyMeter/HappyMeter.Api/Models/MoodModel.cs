﻿using System;

namespace HappyMeter.Api.Models
{
    public class MoodModel
    {
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }
        public int MoodScore { get; set; }
    }
}