using UnityEngine;
using System.Collections;
using System;
using System.IO;

public static class GlobalTools
{
    public static bool IsChild(Transform parent, Transform child)
    {
        if (parent == null || child == null) return false;

        while (child != null)
        {
            if (child == parent) return true;
            child = child.parent;
        }
        return false;
    }

    public static GameObject AddChild(GameObject pParent, GameObject pPrefab, string pName = "")
    {
        GameObject obj = GameObject.Instantiate(pPrefab) as GameObject;

        if (pParent != null)
            obj.transform.SetParent(pParent.transform);
        if (!string.IsNullOrEmpty(pName))
            obj.name = pName;

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        obj.transform.localScale = Vector3.one;

        return obj;
    }

    public static T AddChild<T>(GameObject pParent, GameObject pPrefab, string pName = "")
    {
        GameObject obj = AddChild(pParent, pPrefab, pName);

        T t = obj.GetComponent<T>();
        return t;
    }

    public static GameObject CreateNewChild(string pName = "", GameObject pParent = null)
    {
        GameObject obj = new GameObject(pName);

        if (pParent != null)
            obj.transform.SetParent(pParent.transform);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        obj.transform.localScale = Vector3.one;

        return obj;
    }

    public static void PlayAudio(this GameObject go)
    {
        if (!go.activeInHierarchy)
            return;

        AudioSource audioSource = go.GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public static void PlayAudio(this GameObject go, AudioClip clip)
    {
        if (!go.activeInHierarchy || clip == null)
            return;

        AudioSource audioSource = go.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = go.AddComponent<AudioSource>();

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public static Color ParseColor(string text)
    {
        string[] strings = text.Split(new char[] { ',' });
        if (strings.Length == 4)
        {
            return new Color(float.Parse(strings[0]), float.Parse(strings[1]), float.Parse(strings[2]), float.Parse(strings[3]));
        }
        else if (strings.Length == 3)
        {
            return new Color(float.Parse(strings[0]), float.Parse(strings[1]), float.Parse(strings[2]), 255);
        }
        else
        {
            Debug.LogError("Invalid Color Format : " + text);
            return Color.clear;
        }
    }

    public static Vector3 ParseVector3(string text)
    {
        string[] strings = text.Split(new char[] { ',' });
        if (strings.Length != 3)
        {
            Debug.LogError("Invalid Vector3 Format : " + text);
            return Vector3.zero;
        }

        return new Vector3(float.Parse(strings[0]), float.Parse(strings[1]), float.Parse(strings[2]));
    }

    public static int ParseInt(string text, int defaultValue = 0)
    {
        if (string.IsNullOrEmpty(text))
            return defaultValue;

        return int.Parse(text);
    }

    public static bool ParseBool(string text)
    {
        return !(text == "0");
    }

    public static T ParseEnum<T>(string text)
    {
        int value = ParseInt(text);
        return (T)Enum.ToObject(typeof(T), value);
    }

    public static string ConvertToString(Color color)
    {
        return string.Format("{0},{1},{2},{3}", color.r, color.g, color.b, color.a);
    }

    public static string ConvertToString(Vector3 value)
    {
        return string.Format("{0},{1},{2}", value.x, value.y, value.z);
    }

    public static string ConvertToString(int value)
    {
        return string.Format("{0}", value);
    }

    public static string ConvertToString(bool value)
    {
        return value ? "1" : "0";
    }

    public static void SetPlayerPrefsBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetPlayerPrefsBool(string key, bool defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
            return defaultValue;

        return (PlayerPrefs.GetInt(key) == 1) ? true : false;
    }
}