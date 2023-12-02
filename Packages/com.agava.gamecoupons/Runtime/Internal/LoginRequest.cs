using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    internal struct LoginRequest
    {
        [field: Preserve]
        public int id;
        [field: Preserve]
        public string password;
    }
}
