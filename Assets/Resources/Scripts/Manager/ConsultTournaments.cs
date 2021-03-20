using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConsultTournaments : MonoBehaviour
{
    public string BaseUrl = "https://api.pubg.com/tournaments";

    public Root RootTournoment;

    private string Accept = "application/vnd.api+json";

    private string Authorization = "Bearer Apikey";

    private string KeyApp = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI5ZTRiZTE2MC02YmJkLTAxMzktNWVjOS0xZmE5ZGJlNTJlMWYiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjE2MjUzNTgyLCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6ImFwaS10b3VybnVtZW50In0.hucCv4QqMBBJLeQcA8IZ3q0Hj_8YwzV5kyu3Gw-7Np8";
    

    private void Awake()
    {
        StartCoroutine(ConsultTournamentsCoroutine());
    }

    IEnumerator ConsultTournamentsCoroutine()
    {   
        using (UnityWebRequest webRequest = UnityWebRequest.Get(BaseUrl))
        {
            webRequest.SetRequestHeader("Accept", Accept);
            webRequest.SetRequestHeader("Authorization", Authorization);
            webRequest.SetRequestHeader("Authorization", KeyApp);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = BaseUrl.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    RootTournoment = JsonUtility.FromJson<Root>(webRequest.downloadHandler.text);
                    Debug.Log(RootTournoment.data.Count);
                    break;
            }
        }
    }
}

[Serializable]
public class Attributes
{
    public string createdAt;
}

[Serializable]
public class Datum
{
    public string type;
    public string id;
    public Attributes attributes;
}

[Serializable]
public class Links
{
    public string self;
}

[Serializable]
public class Meta
{
    string meta;
}

[Serializable]
public class Root
{
    public List<Datum> data;
    public Links links;
    public Meta meta;

    public Root()
    {
        data = new List<Datum>();
    }
}
