﻿using System.IO;
using UnityEngine;

namespace GameTool
{
    public class SaveUtility<T>
    {
        public T value;

        public void SaveData(eData filename, T vl)
        {
            value = vl;
            string path = Application.persistentDataPath + "/" + ByteString(ToByte(filename.ToString())) + ".dat";
            string json = JsonUtility.ToJson(this);
            File.WriteAllBytes(path, ToByte(json));
        }

        public void LoadData(eData filename, ref T vl)
        {
            string path = Application.persistentDataPath + "/" + ByteString(ToByte(filename.ToString())) + ".dat";
            if (!File.Exists(path))
            {
                SaveData(filename, vl);
            }

            var data = File.ReadAllBytes(path);
            string json = ByteToString(data);
            vl = JsonUtility.FromJson<SaveUtility<T>>(json).value;
        }

        public static byte[] ToByte(string input)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] key = System.Text.Encoding.UTF8.GetBytes(GetUDID());
            for (uint i = 0; i < data.Length; i++)
                data[i] ^= key[i % key.Length];

            return data;
        }

        public static byte[] ToByte(byte[] input)
        {
            byte[] key = System.Text.Encoding.UTF8.GetBytes(GetUDID());
            for (uint i = 0; i < input.Length; i++)
                input[i] ^= key[i % key.Length];
            return input;
        }

        public static string ByteToString(byte[] data)
        {
            return System.Text.Encoding.UTF8.GetString(ToByte(data));
        }

        public static string ByteString(byte[] data)
        {
            string res = "";
            for (int i = 0; i < data.Length; i++)
            {
                res += data[i];
            }

            return res;
        }

        private static string sDeviceUDID = "";
        public static string GetUDID()
        {
            if (sDeviceUDID.Length <= 0)
            {
                sDeviceUDID = SystemInfo.deviceUniqueIdentifier;
            }
            return sDeviceUDID;
        }
    }

    public static class SaveGameManager
    {
        public static void DeleteAllSave()
        {
            if (Directory.Exists(Application.persistentDataPath))
            {
                string path = Application.persistentDataPath;

                DirectoryInfo d = new DirectoryInfo(path);
                foreach (var file in d.GetFiles("*.dat"))
                {
                    File.Delete(file.FullName);
                }
            }
        }
    }
}