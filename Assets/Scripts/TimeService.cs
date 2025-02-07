using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class TimeService
{
    public async Task<DateTime> LoadTimeFromServer()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(GlobalConstants.Links.TIME_URL);
                    var json = JsonUtility.FromJson<TimeTemplate>(response);
                    var offset = DateTimeOffset.FromUnixTimeMilliseconds(json.time).ToLocalTime();

                    return offset.DateTime;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e.Message);
                return DateTime.Now;
            }
        }
}
