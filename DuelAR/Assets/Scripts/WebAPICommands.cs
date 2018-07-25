using Assets.Scripts;
using GameServer.Models;
using System;
using UnityEngine;
#if UNITY_WSA && !UNITY_EDITOR
using Windows.Foundation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

#endif
public class WebAPICommands : MonoBehaviour {

    public static WebAPICommands Instance;
	void Start () {
        Instance = this;
    }
    public void PostDataAsync(Uri uri, string jsonRequestBody)
    {
        string url = uri.AbsoluteUri;
        var jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
#if UNITY_WSA && !UNITY_EDITOR
    IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
    async (workItem) =>
    {
        WebRequest webRequest = WebRequest.Create(url);
        webRequest.Method = "POST";
        webRequest.Headers["Content-Type"] = "application/json";
 
        Stream stream = await webRequest.GetRequestStreamAsync();
        stream.Write(jsonBytes, 0, jsonBytes.Length);
        //TODO : get response and update GameState
        WebResponse response = await webRequest.GetResponseAsync();
    }
    );
 
    asyncAction.Completed = new AsyncActionCompletedHandler(PostDataAsyncCompleted);
#endif
    }

#if UNITY_WSA && !UNITY_EDITOR
private void PostDataAsyncCompleted(IAsyncAction asyncInfo, AsyncStatus asyncStatus)
{
}
#endif

    public delegate void OnGetDataCompleted(string id, DateTime timestamp, string json, GameObject gameObject);

    private OnGetDataCompleted handler = (id, timestamp, json, gameObject) =>
    {
        GameStateModel gameState = JsonUtility.FromJson<GameStateModel>(json);
        if(CurrentGameState.SetInstance(gameState, timestamp))
        {
            gameObject.BroadcastMessage("OnGameStateUpdate");
        }
    };

    public void GetDataAsync(string url,string id)
    {
        GetDataAsync(url, id, handler, gameObject);
    }

    private void GetDataAsync(string url, string id, OnGetDataCompleted handler, GameObject gameObject)
    {
#if UNITY_WSA && !UNITY_EDITOR
        IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
            async (workItem) =>
            {
                try
                {
                    WebRequest webRequest = WebRequest.Create(url);
                    webRequest.Method = "GET";
                    webRequest.Headers["Content-Type"] = "application/json";

                    WebResponse response = await webRequest.GetResponseAsync();

                    Stream result = response.GetResponseStream();
                    StreamReader reader = new StreamReader(result);

                    string json = reader.ReadToEnd();

                    handler(id, DateTime.Now, json, gameObject);
                }
                catch (Exception)
                {
                    // handle errors
                }
            }
            );
        asyncAction.Completed = new AsyncActionCompletedHandler(GetDataAsyncCompleted);

#endif
    }

#if UNITY_WSA && !UNITY_EDITOR
    private void GetDataAsyncCompleted(IAsyncAction asyncInfo, AsyncStatus asyncStatus)
    {
    }
#endif
    // Update is called once per frame
    void Update () {
		//do nothing;
	}
}
