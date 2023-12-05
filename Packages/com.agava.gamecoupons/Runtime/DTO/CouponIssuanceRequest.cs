using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public class CouponIssuanceRequest
    {
        [field: Preserve]
        public int longitude;
        [field: Preserve]
        public int latitude;
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
