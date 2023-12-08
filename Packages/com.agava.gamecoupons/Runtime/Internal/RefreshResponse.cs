using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    internal class RefreshResponse
    {
        [field: Preserve]
        public string access;
    }
}
