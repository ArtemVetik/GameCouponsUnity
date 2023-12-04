using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public struct GamesResponse
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
        public struct Item
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
        public struct Genre
        {
            [field: Preserve]
            public int id;
            [field: Preserve]
            public string name;
        }

        [Serializable]
        public struct Platform
        {
            [field: Preserve]
            public int id;
            [field: Preserve]
            public string name;
        }
    }
}
