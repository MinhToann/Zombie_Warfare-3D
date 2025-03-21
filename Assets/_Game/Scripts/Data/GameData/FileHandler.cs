using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileHandler : MonoBehaviour
{
    
    public static void SaveToJSON<T>(List<T> listToSave, string fileName)
    {
        string content = JsonHelper.ToJson<T>(listToSave.ToArray());
        WriteFile(GetPath(fileName), content);
    }
    public static void SaveToJSON<T>(T toSave, string fileName)
    {
        string content = JsonUtility.ToJson(toSave);
        WriteFile(GetPath(fileName), content);
    }
    public static List<T> ReadFromJSON<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }
        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        return res;
    }
    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter streamWriter = new StreamWriter(fileStream))
        {
            streamWriter.Write(content);
        }
    }
    private static string GetPath(string fileName)
    {
        // Persistent data path ensures cross-platform compatibility
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Check if the file exists; if not, copy the default from Resources
        if (!File.Exists(filePath))
        {
            TextAsset defaultData = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(fileName));

            if (defaultData != null)
            {
                File.WriteAllText(filePath, defaultData.text);
            }
        }
        string editorPath = Path.Combine(Application.dataPath, "Resources", fileName);
        if (!File.Exists(editorPath))
        {
            File.Copy(filePath, editorPath, true);
        }

        return filePath;
    }
    
}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
   
}
