using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    static SoundMgr GetInstance;
    List<StudioEventEmitter> emitters;

    private static EventInstance soundEvent;

    public SoundItem[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void VolumeTrigger()
    {

    }
    static public void PlayOneShot(string nameOrPath)
    {
        string path = GetPathFromName(nameOrPath);
        if (path != "")
        {
            RuntimeManager.PlayOneShot(path);
        }
    }
    static string GetPathFromName(string name)
    {
        string path = "";
        foreach (SoundItem item in GetInstance.sounds)
        {
            if (item.name == name)
            {
                path = item.path;
                break;
            }
        }
        return path;
    }
}

[System.Serializable]
public struct SoundItem
{
    public string name;
    [EventRef]
    public string path;
}
