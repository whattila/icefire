﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icefire.Models
{
    public class Book
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public List<string> Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public string MediaType { get; set; }
        public DateTime Released { get; set; }
        public List<string> Characters { get; set; }
        public List<string> PovCharacters { get; set; }
    }
}
