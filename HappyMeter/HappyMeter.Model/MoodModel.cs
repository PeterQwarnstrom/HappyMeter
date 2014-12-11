using System;

namespace HappyMeter.Model
{
    public class Mood
    {
        public string Id { get; set; }
		public string Area { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }
        public int MoodScore { get; set; }
    }
}