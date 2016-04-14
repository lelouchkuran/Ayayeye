using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Block {
    public int id;
    public GameObject button;
};

public class BlockBehavior : MonoBehaviour {
    public GameObject[] models;
    public GameObject cover;
    static float timestamp, xy_off, cd, scale, z_off;
    static KeyCode[] keymap;
    Block[] blocks;
    GameObject[] covers;
    int[] layers;
    bool initialized = false;
    int num_row, num_col, num_layer;

    // Update is called once per frame
    void Update () {
        if (initialized) {
            for (int i = 0; i < keymap.Length; ++i) {
                if (Input.GetKeyDown(keymap[i]))
                    OnButtonPress(i);
            }
        }
    }

    public void OnBirth (int row, int col, int layer) {
        if (!initialized) {
            xy_off = Constant.Instance.CoverStepL;
            cd = Constant.Instance.CoverCD;
            scale = Constant.Instance.CoverScale;
            z_off = Constant.Instance.CoverOffset;
            layers = new int[col * row];
            for (int i = 0; i < layers.Length; ++i)
                layers[i] = num_layer;
            blocks = new Block[col * row];
            keymap = Constant.Instance.KeyMap;
            num_layer = layer;
            num_col = col;
            num_row = row;
            for (int i = 0; i < col; ++i) {
                for (int j = 0; j < row; ++j) {
                    covers[i + j * col] = Instantiate(cover);
                    covers[i + j * col].transform.parent = transform;
                    covers[i + j * col].transform.localPosition = new Vector3(xy_off * (i - (col - 1) * 0.5f), xy_off * (j - (row - 1) * 0.5f));
                    GenerateBlock(blocks[i + j * col], i, j);
                }
            }
        }
        initialized = true;
    }


    public void OnDeath () {
        foreach (Block block in blocks) {
            DestroyBlock(block);
        }
        Destroy(gameObject);
    }

    void OnButtonPress (int id) {
        if (Time.time - timestamp < cd)
            return;
        bool istriggered = false;
        for (int i = 0; i < num_col; ++i) {
            for (int j = 0; j < num_row; ++j) {
                if (layers[i + j * num_col] > 0) {
                    if (blocks[i + j * num_col].id == id) {
                        istriggered = true;
                        if (--layers[i + j * num_col] == 0) {
                            Destroy(covers[i + j * num_col]);
                        }
                    }
                }
            }
        }
        if (istriggered) {
            for (int i = 0; i < num_col; ++i) {
                for (int j = 0; j < num_row; ++j) {
                    if (layers[i] > 0) {
                        GenerateBlock(blocks[i + j * num_col], i, j);
                    }
                    else {
                        DestroyBlock(blocks[i + j * num_col]);
                    }
                }
            }
        }
    }

    void GenerateBlock (Block block, int col, int row) {
        if (block.id != -1)
            DestroyBlock(block);
        int index = Random.Range(0, models.Length - 1);
        block.id = index;
        block.button = Instantiate(models[index]);
        block.button.transform.parent = gameObject.transform;
        block.button.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, -transform.parent.localRotation.z));
        block.button.transform.localPosition = new Vector3(xy_off * (col - (num_col - 1) * 0.5f), xy_off * (row - (num_row - 1) * 0.5f), z_off);
        block.button.transform.localScale = new Vector3(scale, scale, scale);
    }

    void DestroyBlock (Block block) {
        Destroy(block.button);
        block.id = -1;
    }
}
