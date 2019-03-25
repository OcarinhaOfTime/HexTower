using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour {
    public int width = 16;
    public int height = 16;
    public float hex_size = 1;
    public Transform hexPrefab;

    private void Start() {
        var w = Mathf.Sqrt(3) * hex_size;
        var h = hex_size * 2;
        hexPrefab.localScale = Vector3.one * hex_size * 2;

        for (int i=0; i<width; i++) {
            for (int j = 0; j < height; j++) {
                var hex = Instantiate(hexPrefab);
                float o = w * .5f * ((j + 1) % 2);
                hex.position = new Vector3((i - width / 2) * w + o, (j - height / 2) * h * .75f, 0);
                hex.name = "tile_"+i+":"+j;
                hex.SetParent(transform);
                hex.gameObject.SetActive(true);
            }
        }
    }
}
