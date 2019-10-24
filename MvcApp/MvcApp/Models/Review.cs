using System;

namespace MvcApp.Models
{
    public class Review
    {
        public int ReviewId { get; set; } // レビューID
        public string Title { get; set; } // レビュータイトル
        public string Body { get; set; } // レビュー本文
        public DateTime CreatedAt { get; set; } // 投稿日
    }
}