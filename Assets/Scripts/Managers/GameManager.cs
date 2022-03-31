using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int boardSize = new Vector2Int(11, 11);

    [SerializeField]
    private GameBoard board = default;

    private void Awake()
    {
        board.Initialize(boardSize);
    }

    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void OnValidate()
    {
        if (boardSize.x < 2)
        {
            boardSize.x = 2;
        }
        if (boardSize.y < 2)
        {
            boardSize.y = 2;
        }
    }
}