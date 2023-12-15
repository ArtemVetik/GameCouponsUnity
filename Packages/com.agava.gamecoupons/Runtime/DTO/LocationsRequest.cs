using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public class LocationsRequest
    {
        [field: Preserve]
        public string Name;
        [field: Preserve]
        public string CountryCode;
        [field: Preserve]
        public string LanguageCode;
        [field: Preserve]
        public int Page = 1;
        [field: Preserve]
        public int Size = 50;

        public string CreateQuery()
        {
            var query = $"page={Page}&size={Size}";

            if (string.IsNullOrEmpty(LanguageCode) == false)
                query = $"language_code={LanguageCode}&" + query;

            if (string.IsNullOrEmpty(CountryCode) == false)
                query = $"country_code={CountryCode}&" + query;

            if (string.IsNullOrEmpty(Name) == false)
                query = $"name={Name}&" + query;

            return query;
        }
    }
}
