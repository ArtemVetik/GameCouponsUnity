using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public class CityListResponse
    {
        [field: Preserve]
        public City[] Data;

        [Serializable]
        public class City
        {
            [field: Preserve]
            public string Name;
            [field: Preserve]
            public float Longitude;
            [field: Preserve]
            public float Latidute;
        }
    }
}
