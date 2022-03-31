using UnityEngine;

public enum GameTileContentType
{
    Empty, Destination
}

public class GameTileContent : MonoBehaviour
{
    [SerializeField] private GameTileContentType type = default;
    [SerializeField] private GameTileContent destinationPrefab = default;
    [SerializeField] private GameTileContent emptyPrefab = default;
    public GameTileContentType Type => type;

    public GameTileContent Get(GameTileContentType type)
    {
        switch (type)
        {
            case GameTileContentType.Destination: return destinationPrefab;
            case GameTileContentType.Empty: return emptyPrefab;
        }
        Debug.Assert(false, "Unsupported type: " + type);
        return null;
    }
}