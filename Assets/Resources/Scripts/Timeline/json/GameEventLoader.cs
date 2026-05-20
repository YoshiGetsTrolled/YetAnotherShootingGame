using UnityEngine;
using System.IO;
public class GameEventLoader : MonoBehaviour
{
    public GameEventData data;

    public GameEventData Load(string fileName)
    {
        // exe と同じフォルダ
        string dir = Directory.GetParent(Application.dataPath).FullName;

        string path = Path.Combine(dir, fileName + ".json");

        if (!File.Exists(path))
        {
            Debug.LogError("読み込み失敗！！！！！え" + path);
            return null;
        }
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<GameEventData>(json);
        return data;
    }
}
