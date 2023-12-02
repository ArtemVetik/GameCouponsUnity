using UnityEngine.Networking;

namespace Agava.GameCoupons
{
    internal static class Extensions
    {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
        {
            return new UnityWebRequestAwaiter(asyncOperation);
        }
    }
}
