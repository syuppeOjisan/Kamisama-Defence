using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // ファイルの読み込みに必要
using System.Linq; // LINQでCSVのデータを処理するために必要

public class GridSystem : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;

    public Color gridColor = Color.white;
    public Color occupiedCellColor = Color.red;
    public Color unplaceableCellColor = Color.gray;

    private GameObject[,] gridArray;
    private bool[,] placeableArray;
    private bool showGrid = true;
    private Material lineMaterial;

    [SerializeField]
    private string csvFileName = "unplaceableCells.csv"; // デフォルトCSV

    public Camera miniMapCamera; // ミニマップカメラの参照

    public string CsvFileName
    {
        get { return csvFileName; }
        set
        {
            csvFileName = value;
            LoadUnplaceableCellsFromCSV(); // CSVファイルを再読み込み
        }
    }

    void Start()
    {
        InitializeGrid();
        CreateLineMaterial();
    }

    public void InitializeGrid()
    {
        if (gridArray == null || placeableArray == null)
        {
            gridArray = new GameObject[gridWidth, gridHeight];
            placeableArray = new bool[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    placeableArray[x, z] = true;
                }
            }

            LoadUnplaceableCellsFromCSV();
            Debug.Log("GridSystemが初期化されました。");
        }
        else
        {
            Debug.LogWarning("GridSystemは既に初期化されています。");
        }
    }

    void LoadUnplaceableCellsFromCSV()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, csvFileName);

        if (File.Exists(filePath))
        {
            string[] csvLines = File.ReadAllLines(filePath);

            foreach (string line in csvLines.Skip(1))
            {
                string[] values = line.Split(',');

                if (values.Length >= 2)
                {
                    int x = int.Parse(values[0]);
                    int z = int.Parse(values[1]);

                    if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
                    {
                        SetCellPlaceable(x, z, false);
                    }
                }
            }

            Debug.Log($"CSVファイル '{csvFileName}' から設置不可セルを読み込みました。");
        }
        else
        {
            Debug.LogError("CSVファイルが見つかりません: " + filePath);
        }
    }

    public void SetCellPlaceable(int x, int z, bool canPlace)
    {
        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
        {
            placeableArray[x, z] = canPlace;
        }
    }

    public Vector3 GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int z = Mathf.FloorToInt(worldPosition.z / cellSize);

        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    public bool CanPlaceUnit(Vector3 worldPosition)
    {
        if (gridArray == null || placeableArray == null)
        {
            InitializeGrid();
        }

        Vector3 gridPosition = GetGridPosition(worldPosition);
        int x = Mathf.FloorToInt(gridPosition.x / cellSize);
        int z = Mathf.FloorToInt(gridPosition.z / cellSize);

        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
        {
            return placeableArray[x, z] && gridArray[x, z] == null;
        }
        return false;
    }

    public bool IsOccupied(Vector3 gridPosition)
    {
        int x = Mathf.FloorToInt(gridPosition.x / cellSize);
        int z = Mathf.FloorToInt(gridPosition.z / cellSize);

        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
        {
            return gridArray[x, z] != null;
        }
        return false;
    }

    public GameObject GetUnitAt(Vector3 gridPosition)
    {
        int x = Mathf.FloorToInt(gridPosition.x / cellSize);
        int z = Mathf.FloorToInt(gridPosition.z / cellSize);

        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
        {
            return gridArray[x, z];
        }
        return null;
    }

    public bool IsPlaceable(Vector3 gridPosition)
    {
        int x = Mathf.FloorToInt(gridPosition.x / cellSize);
        int z = Mathf.FloorToInt(gridPosition.z / cellSize);

        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
        {
            return placeableArray[x, z];
        }
        return false;
    }

    public void PlaceUnit(Vector3 worldPosition, GameObject unitPrefab)
    {
        Vector3 gridPosition = GetGridPosition(worldPosition);

        int x = Mathf.FloorToInt(gridPosition.x / cellSize);
        int z = Mathf.FloorToInt(gridPosition.z / cellSize);

        if (CanPlaceUnit(worldPosition))
        {
            GameObject newUnit = Instantiate(unitPrefab, gridPosition, unitPrefab.gameObject.transform.rotation);
            gridArray[x, z] = newUnit;
        }
    }

    public void RemoveUnit(Vector3 worldPosition)
    {
        Vector3 gridPosition = GetGridPosition(worldPosition);

        int x = Mathf.FloorToInt(gridPosition.x / cellSize);
        int z = Mathf.FloorToInt(gridPosition.z / cellSize);

        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
        {
            if (gridArray[x, z] != null)
            {
                Destroy(gridArray[x, z]);
                gridArray[x, z] = null;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (gridArray == null)
        {
            InitializeGrid();
        }

        if (showGrid)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    Vector3 cellPosition = new Vector3(x * cellSize, 0, z * cellSize);

                    if (placeableArray[x, z])
                    {
                        Gizmos.color = (gridArray[x, z] == null) ? gridColor : occupiedCellColor;
                    }
                    else
                    {
                        Gizmos.color = unplaceableCellColor;
                    }

                    Gizmos.DrawWireCube(cellPosition + new Vector3(cellSize / 2, 0, cellSize / 2), new Vector3(cellSize, 0.1f, cellSize));
                }
            }
        }
    }

    void CreateLineMaterial()
    {
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineMaterial = new Material(shader);
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        lineMaterial.SetInt("_ZWrite", 0);
    }

    void OnRenderObject()
    {
        // ミニマップカメラからの描画を無視
        if (Camera.current == miniMapCamera)
        {
            return;
        }

        if (showGrid)
        {
            DrawGridLines();
        }
    }

    void DrawGridLines()
    {
        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(gridColor);

        for (int x = 0; x <= gridWidth; x++)
        {
            for (int z = 0; z <= gridHeight; z++)
            {
                Vector3 start = new Vector3(x * cellSize, 0, 0);
                Vector3 end = new Vector3(x * cellSize, 0, gridHeight * cellSize);

                GL.Vertex(start);
                GL.Vertex(end);

                start = new Vector3(0, 0, z * cellSize);
                end = new Vector3(gridWidth * cellSize, 0, z * cellSize);

                GL.Vertex(start);
                GL.Vertex(end);
            }
        }

        GL.End();
    }
}