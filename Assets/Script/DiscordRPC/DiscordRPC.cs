using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_WEBGL_API
using Discord;
#endif
public class DiscordRPC : MonoBehaviour
{
    #if !UNITY_WEBGL_API
    public Discord.Discord discord;
    #endif
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_WEBGL_API

        if (PlayerPrefs.GetString("DiscordRPC") == "true")
        discord = new Discord.Discord(1283149000937046016,((System.UInt64)Discord.CreateFlags.Default));
        if (discord != null)
        {
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            Name = "Abo Flah in his childhood",
            Details = "Farm",
            State = "farming the corns"
            

            
        };

        activityManager.UpdateActivity(activity ,(res)=>{
            switch(res)
            {
                case Discord.Result.Ok:Debug.Log("is setted");break;
                default :Debug.Log("not setted");break;
            }
        });

        }
        
        #endif
        
    }






    // Update is called once per frame
    void Update()
    {
        #if !UNITY_WEBGL_API
        if (discord != null)
            discord.RunCallbacks();
        #endif
        
    }
}
