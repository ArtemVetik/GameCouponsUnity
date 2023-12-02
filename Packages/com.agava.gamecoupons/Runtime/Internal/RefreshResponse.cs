using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    internal struct RefreshResponse
    {
        [field: Preserve]
        public string access;
    }
}
