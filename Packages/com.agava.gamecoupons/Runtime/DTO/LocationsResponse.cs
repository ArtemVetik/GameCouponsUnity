using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public class LocationsResponse
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
            public string asciiname;
            [field: Preserve]
            public float latitude;
            [field: Preserve]
            public float longitude;
            [field: Preserve]
            public string localized_name;
        }
    }
}
