using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class TimeService
{
    public string Result { get; private set; }

    public IEnumerator LoadTimeFromServer()
        {
                using (UnityWebRequest request = UnityWebRequest.Get(GlobalConstants.Links.PROXY + GlobalConstants.Links.TIME_URL))
                {
                    request.SetRequestHeader("Origin", GlobalConstants.Links.YA_URL);
                    
                    
                    var operation = request.SendWebRequest();

                   yield return new WaitUntil(() => operation.isDone);
                   Result = request.downloadHandler.text;


                   // var dateHeader = request.GetResponseHeader("Date");
                   // var date = DateTime.Parse(dateHeader);
                   // return date;

                   // var response = client.downloadHandler.text;
                   // var json = JsonUtility.FromJson<TimeTemplate>(response);
                   // var offset = DateTimeOffset.FromUnixTimeMilliseconds(json.time).ToLocalTime();

                   // return offset.DateTime;

                }
        }
}
