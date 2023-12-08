using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public class GamesResponse
    {
        [field: Preserve]
        public Item[] items;
        [field: Preserve]
        public int total;
        [field: Preserve]
        public int page;
        [field: Preserve]
        public int size;
        [field: Preserve]
        public int pages;

        [Serializable]
        public class Item
        {
            [field: Preserve]
            public int id;
            [field: Preserve]
            public string name;
            [field: Preserve]
            public Genre[] genres;
            [field: Preserve]
            public Platform[] platforms;
        }

        [Serializable]
        public class Genre
        {
            [field: Preserve]
            public int id;
            [field: Preserve]
            public string name;
        }

        [Serializable]
        public class Platform
        {
            [field: Preserve]
            public int id;
            [field: Preserve]
            public string name;
        }
    }
}
