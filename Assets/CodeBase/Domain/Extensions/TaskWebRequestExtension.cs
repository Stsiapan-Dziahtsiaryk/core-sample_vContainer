using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Domain.Extensions
{
    public static class TaskWebRequestExtension
    {
        public static async UniTask SendWebRequestSafely(this UnityWebRequest request)
        {
            request.SendWebRequest();
            await UniTask.WaitUntil(() => request.isDone || request.isNetworkError);
        }
    }
}