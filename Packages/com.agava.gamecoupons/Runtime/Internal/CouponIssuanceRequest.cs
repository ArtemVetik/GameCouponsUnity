using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    internal class CouponIssuanceRequest
    {
        [field: Preserve]
        public float longitude;
        [field: Preserve]
        public float latitude;
        [field: Preserve]
        public int game_id;
        [field: Preserve]
        public int[] platform_ids = new int[0];
        [field: Preserve]
        public int[] genre_ids = new int[0];
        [field: Preserve]
        public int[] exclude_organization_ids = new int[0];
    }
}
