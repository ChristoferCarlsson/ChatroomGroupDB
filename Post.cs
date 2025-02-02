﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
