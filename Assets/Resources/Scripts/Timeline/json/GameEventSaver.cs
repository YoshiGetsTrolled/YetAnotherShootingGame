using System.IO;
using UnityEngine;


#region Saver

public class GameEventSaver : MonoBehaviour
{
    public GameEventData data;

    public void SaveJson(string fileName)
    {
        if (data == null)
        {
            Debug.Log("•Û‘¶¸”sI");
            return;
        }
        string json = JsonUtility.ToJson(data,true);

        //‘‚«‚İ
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", json);
        Debug.Log("•Û‘¶Š®—¹: " + fileName);
    }
}

#endregion