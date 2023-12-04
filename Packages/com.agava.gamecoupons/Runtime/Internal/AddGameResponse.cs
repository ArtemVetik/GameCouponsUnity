using System;
using UnityEngine.Scripting;

namespace Agava.GameCoupons
{
    [Serializable]
    internal struct AddGameResponse
    {
        [field: Preserve]
        public string name;
        [field: Preserve]
        public int[] genre_ids;
        [field: Preserve]
        public int[] platform_ids;
    }
}
