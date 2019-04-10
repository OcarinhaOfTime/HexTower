using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hexagon : MonoBehaviour {
    private GameObject offsetCoords;
    private GameObject cubeCoords;

    private TMP_Text offset_x_txt;
    private TMP_Text offset_y_txt;

    private TMP_Text cube_x_txt;
    private TMP_Text cube_y_txt;
    private TMP_Text cube_z_txt;

    public Vector3Int coord;

    private SpriteRenderer sr;

    private HexGrid hexGrid;

    private void Awake() {
        offsetCoords = transform.GetChild(0).GetChild(0).gameObject;
        cubeCoords = transform.GetChild(0).GetChild(1).gameObject;

        offset_x_txt = offsetCoords.transform.GetChild(0).GetComponent<TMP_Text>();
        offset_y_txt = offsetCoords.transform.GetChild(1).GetComponent<TMP_Text>();

        cube_x_txt = cubeCoords.transform.GetChild(0).GetComponent<TMP_Text>();
        cube_y_txt = cubeCoords.transform.GetChild(1).GetComponent<TMP_Text>();
        cube_z_txt = cubeCoords.transform.GetChild(2).GetComponent<TMP_Text>();

        sr = GetComponent<SpriteRenderer>();

        hexGrid = GetComponentInParent<HexGrid>();
    }

    public void SetCoordsActive(bool isCubeCoords) {
        offsetCoords.SetActive(!isCubeCoords);
        cubeCoords.SetActive(isCubeCoords);
    }

    public void SetCoords(Vector2Int coord) {
        this.coord = HexGrid.oddr_to_cube(coord);
        offset_x_txt.text = "" + coord.x;
        offset_y_txt.text = "" + coord.y;

        SetCoordsActive(false);
    }

    public void SetCoords(Vector3Int coord) {
        this.coord = coord;
        cube_x_txt.text = "" + coord.x;
        cube_y_txt.text = "" + coord.y;
        cube_z_txt.text = "" + coord.z;

        SetCoordsActive(true);
    }

    public void SetColor(Color color) {
        sr.color = color;
    }

    private void OnMouseDown() {
        print("you've clicked on " + coord);
        hexGrid.OnClick(this);
    }
}
