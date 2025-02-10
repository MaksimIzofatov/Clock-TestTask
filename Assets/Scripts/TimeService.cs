using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class TimeService
{
    public async Task<DateTime> LoadTimeFromServer()
        {
                using (UnityWebRequest client = UnityWebRequest.Get(GlobalConstants.Links.TIME_URL))
                {
                    var send = client.SendWebRequest();
                    while (!send.isDone)
                    {
                        await Task.Yield();
                    }

                    if (client.result == UnityWebRequest.Result.ConnectionError ||
                        client.result == UnityWebRequest.Result.ProtocolError)
                    {
                        return DateTime.Now;
                    }
                    
                    var response = client.downloadHandler.text;
                    var json = JsonUtility.FromJson<TimeTemplate>(response);
                    var offset = DateTimeOffset.FromUnixTimeMilliseconds(json.time).ToLocalTime();

                    return offset.DateTime;
                }
        }
}
