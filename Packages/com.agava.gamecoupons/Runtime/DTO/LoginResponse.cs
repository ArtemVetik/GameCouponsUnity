using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    public struct LoginResponse
    {
        [field: Preserve]
        public string access;
        [field: Preserve]
        public string refresh;
    }
}
