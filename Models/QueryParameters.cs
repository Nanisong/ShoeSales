﻿namespace ShoeSalesAPI.Models
{
    public class QueryParameters
    {
        const int MaxSize = 100;
        private int _pageSize = 50;//Return Items
        public int Page { get; set; } = 1;

        public int Size
        {
            get { return _pageSize; }
            set { _pageSize = Math.Min(_pageSize, value); }
        }
    }
}
