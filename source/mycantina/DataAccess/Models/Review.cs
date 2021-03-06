﻿using System;

namespace mycantina.DataAccess.Models
{
    public class Review
    {
        public int ConsumerId { get; set; }
        public int BottleId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateModified { get; set; }
        public Bottle Bottle { get; set; }
        public Consumer Consumer { get; set; }
    }
}