using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public struct RefreshResponse
    {
        [field: Preserve]
        public string access;
    }
}
