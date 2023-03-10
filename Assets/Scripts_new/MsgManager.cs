using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class MsgManager : MonoSingleton<MsgManager>
{
    [SerializeField] private String currentPalyerId;


    [System.Serializable]
    public class MSG
    {
        [JsonProperty("msgType")]
        public string msgType;
        [JsonProperty("msgBody")]
        public string msgBody;
        [JsonProperty("owner")]
        public string owner;
    }

    public string makeMsgConvertToJson(String msgType , string msgBody ,string owner)
    {
        // Create an instance of the MSG class
        MSG msg = new MSG();
        msg.msgType = msgType;
        msg.msgBody = msgBody;
        msg.owner = owner;

        // Convert the MSG instance to JSON
        string json = JsonUtility.ToJson(msg);

        // Output the JSON string
        Debug.Log(json);
        return json;
    }
    
    public string ConvertToJson(MSG msg)
    {
        // Convert the MSG instance to JSON
        string json = JsonUtility.ToJson(msg);

        // Output the JSON string
        Debug.Log(json);
        return json;
    }
    

    public MSG convertJsonToMsg(string json)
    {
        MSG msgFromJson = JsonUtility.FromJson<MSG>(json);
        return msgFromJson;
    }

    public void handelIncomingMsg(string msgJson)
    {
        MSG msg = convertJsonToMsg(msgJson);
        if (msg.owner == currentPalyerId)
            return; //no need process the message that yours


        Dictionary<string, string> dict;
        switch (msg.msgType)
        {
            case "resFindMatchWait":
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg.msgBody);
                currentPalyerId = dict["yourUserName"];
                Debug.Log("ta in ja omad5");
                break;
            case "MatchFounded":
                Debug.Log("ta in ja omad4");
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg.msgBody);
                Debug.Log(dict);
                break;
                
            default:
                break;
            
            
        }
    }

    
    


    //send Msgs
    public void reqMatchMaking()
    {
        MSG msg = new MSG();
        msg.msgType = "reqTofindMatch";
        msg.msgBody = "";
        msg.owner = "";
        string jsonMsg = ConvertToJson(msg);
        Network.Instance.sendMsg(jsonMsg);

    }
}
