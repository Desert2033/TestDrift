using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "StaticData/RoomData")]
public class TestRoomData : ScriptableObject
{
    public GameMode GameMode;
    public int SceneIndex;
}
