﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BusinessLogic.Core
{

    //Это точно тут должно быть? Мне кажется что это в Domain, но раз ты добавлял, то тебе виднее.
    public class News
    {
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Images { get; set; } = new List<string>();
    }
}
