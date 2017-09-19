using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameSceneData
{
    public GameSceneId sceneId;
    public GameObject scenePrefab;

    [System.NonSerialized]
    public GameScene sceneObject;
}

public enum GameSceneId
{
    Scene1,
    Scene2,
    Scene3,
}

public enum GameButtonId
{
    GotoScene1,
    GotoScene2,
    GotoScene3,
}

public enum ItemType
{
    N00_Unkown,
    N01_Note,
    N02_Book,
    N03_Camio,
    N04_Cartridge,
    N05_Clock,
    N06_Coins,
    N07_Eagle,
    N08_Globe,
    N09_Rose,
}

[System.Serializable]
public class ItemData
{
    public ItemType itemType;
    public Sprite sprite;
}