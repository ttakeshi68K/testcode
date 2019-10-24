using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class Book
    {
        [Key]
        public string Isbn { get; set; }    // ISBNコード
        public string Title { get; set; }   // 書名
        public int? Price { get; set; }     // 価格
        public string Publish { get; set; } // 出版社
        public DateTime Published { get; set; } // 刊行日

        public virtual ICollection<Review> Reviews { get; set; } // レビュー
    }
}