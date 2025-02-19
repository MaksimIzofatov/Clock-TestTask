using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeService
{
    public string Result { get; private set; }
    public bool RequestIsSuccess { get; private set; } = true;
    
    private const string TIME_URL = "https://timeapi.io/api/time/current/zone?timeZone=Europe%2FMinsk";

    public IEnumerator LoadTimeFromServer()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(TIME_URL))
        {
            var operation = request.SendWebRequest();
            yield return new WaitUntil(() => operation.isDone);

            if (request.result != UnityWebRequest.Result.Success)
            {
                RequestIsSuccess = false;
            }
                    
            Result = request.downloadHandler.text;
        }
    }
}