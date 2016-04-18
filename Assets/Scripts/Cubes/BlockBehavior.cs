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
    static float timestamp, xy_off, cd, z_off;
    static KeyCode[] keymap;
    Block[] blocks;
    GameObject[] covers;
    int[] layers;
    bool initialized = false;
    int num_row, num_col, num_layer;

    void Start () {
        //OnBirth(3, 3, 2);
    }

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
            z_off = Constant.Instance.CoverOffset;
            num_col = col;
            num_row = row;
            num_layer = layer;
            layers = new int[col * row];
            for (int i = 0; i < layers.Length; ++i)
                layers[i] = num_layer;
            blocks = new Block[col * row];
            covers = new GameObject[col * row];
            keymap = Constant.Instance.KeyMap;
            for (int i = 0; i < col; ++i) {
                for (int j = 0; j < row; ++j) {
                    covers[i + j * col] = Instantiate(cover);
                    covers[i + j * col].transform.parent = transform;
                    covers[i + j * col].transform.localPosition = new Vector3(xy_off * (i - (col - 1) * 0.5f), xy_off * (j - (row - 1) * 0.5f));
                    covers[i + j * col].transform.localRotation = Quaternion.Euler(new Vector3());
                    blocks[i + j * col] = new Block();
                    int index = Random.Range(0, models.Length - 1);
                    Block block = new Block();
                    block.button = Instantiate(models[index]);
                    block.id = index;
                    block.button.transform.parent = gameObject.transform;
                    block.button.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                    block.button.transform.localPosition = new Vector3(xy_off * (i - (col - 1) * 0.5f), xy_off * (j - (row - 1) * 0.5f), z_off);
                    blocks[i + j * col] = block;
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
        if (Time.time - timestamp < cd) return;
        bool istriggered = false;
        for (int i = 0; i < num_col; ++i) {
            for (int j = 0; j < num_row; ++j) {
                if (layers[i + j * num_col] > 0) {
                    if (blocks[i + j * num_col].id == id) {
                        istriggered = true;
                        layers[i + j * num_col]--;
                        if (layers[i + j * num_col] == 0) 
                            Destroy(covers[i + j * num_col]);
                        timestamp = Time.time;
                    }
                }
            }
        }
        if (istriggered) {
            for (int i = 0; i < num_col; ++i) {
                for (int j = 0; j < num_row; ++j) {
                    if (layers[i + j * num_col] > 0) {
                        DestroyBlock(blocks[i + j * num_col]);
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
        int index = Random.Range(0, models.Length - 1);
        block.button = Instantiate(models[index]);
        block.id = index;
        block.button.transform.parent = gameObject.transform;
        block.button.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        block.button.transform.localPosition = new Vector3(xy_off * (col - (num_col - 1) * 0.5f), xy_off * (row - (num_row - 1) * 0.5f), z_off);
    }

    void DestroyBlock (Block block) {
        if (block.id != -1) {
            GameObject.Find("FeedbackController").GetComponent<FeedbackController>().playRightFeedback(block.button.transform);
            block.id = -1;
            Destroy(block.button);
        }
    }
}
