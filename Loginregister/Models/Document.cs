﻿using System.ComponentModel.DataAnnotations;
using Loginregister.Models;
namespace Loginregister.Models
{
    public class Document
    {
       

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ? FileName { get; set; }
        public string ? DownloadUrl { get; set; }
        public string ? ViewUrl { get; set; }
      
        
    }
}

