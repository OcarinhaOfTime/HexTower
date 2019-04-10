using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour {
    public int width = 16;
    public int height = 16;
    public float hex_size = 1;
    public Hexagon hexPrefab;
    public bool useCubeCoords;

    private Dictionary<Vector3Int, Hexagon> hexagons_dict = new Dictionary<Vector3Int, Hexagon>();

    public Color normalColor = Color.white;
    public Color selectedColor = Color.white;
    public Color neighborColor = Color.white;

    private Vector3Int[] cube_directions = {
        new Vector3Int(+1, -1, 0), new Vector3Int(+1, 0, -1), new Vector3Int(0, +1, -1),
        new Vector3Int(-1, +1, 0),new  Vector3Int(-1, 0, +1), new Vector3Int(0, -1, +1)};

    private void Start() {
        Action<Hexagon, Vector2Int> onCreate;
        if (useCubeCoords)
            onCreate = (he, c) => he.SetCoords(oddr_to_cube(c));
        else
            onCreate = (he, c) => he.SetCoords(c);

        var w = Mathf.Sqrt(3) * hex_size;
        var h = hex_size * 2;
        hexPrefab.transform.localScale = Vector3.one * hex_size * 2;

        for (int i = -width; i <= width; i++) {
            for (int j = -height; j <= height; j++) {
                var hex = Instantiate(hexPrefab);
                float o = w * .5f * ((j + height) % 2);
                hex.name = "tile_" + i + ":" + j;
                hex.transform.SetParent(transform);
                hex.transform.localPosition = new Vector3(i * w + o, j * h * .75f, 0);
                hex.gameObject.SetActive(true);

                Vector2Int coord = new Vector2Int(i, -j);
                var ccoord = oddr_to_cube(coord);
                onCreate(hex, coord);
                hexagons_dict.Add(ccoord, hex);
            }
        }
    }

    public static Vector2Int cube_to_oddr(Vector3Int coord) {
        var x = coord.x + (coord.z - (coord.z & 1)) / 2;
        var y = coord.z;

        return new Vector2Int(x, y);
    }

    public static Vector3Int oddr_to_cube(Vector2Int coord) {
        var x = coord.x - (coord.y - (coord.y & 1)) / 2;
        var z = coord.y;
        var y = -x - z;

        return new Vector3Int(x, y, z);
    }

    public List<Hexagon> Neighbors(Hexagon hex) {
        List<Hexagon> neighbors = new List<Hexagon>();

        foreach(var d in cube_directions) {
            var nc = hex.coord + d;
            if (hexagons_dict.ContainsKey(nc))
                neighbors.Add(hexagons_dict[nc]);
        }

        return neighbors;
    }

    public void ResetColors() {
        foreach (var h in hexagons_dict.Values)
            h.SetColor(normalColor);
    }

    public void OnClick(Hexagon hex) {
        ResetColors();

        hex.SetColor(selectedColor);
        foreach (var h in Neighbors(hex))
            h.SetColor(neighborColor);
    }
}
