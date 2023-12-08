using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public class CouponIssuanceResponse
    {
        [field: Preserve]
        public int id;
        [field: Preserve]
        public string name;
        [field: Preserve]
        public OrganizationsResponse.Item organization;
        [field: Preserve]
        public string text;
        [field: Preserve]
        public string link;
        [field: Preserve]
        public string expires_at;
    }
}
