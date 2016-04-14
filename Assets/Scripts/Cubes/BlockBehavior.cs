using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Block {
    public int id;
    public GameObject button;
};

public class BlockBehavior : MonoBehaviour {
    public GameObject[] models;
    static float timestamp, step, cd, scale, thick;
    static KeyCode[] keymap;
    Queue<Block>[] queues;
    bool initialized = false;

    // Update is called once per frame
    void Update () {
        if (initialized) {
            for(int i = 0; i < keymap.Length; ++i) {
                if (Input.GetKeyDown(keymap[i]))
                    OnButtonPress(i);
            }
        }
    }

    public void OnBirth (int row, int col, int num_layer) {
        step = Constant.Instance.CoverStepL;
        cd = Constant.Instance.CoverCD;
        scale = Constant.Instance.CoverScale;
        thick = Constant.Instance.CoverOffset;
        queues = new Queue<Block>[row * col];
        keymap = Constant.Instance.KeyMap;
        for (int i = 0; i < col; ++i) {
            for (int j = 0; j < row; ++j) {
                queues[i + j * col] = new Queue<Block>();
                for (int k = 0; k < num_layer; ++k) {
                    int index = Random.Range(0, models.Length - 1);
                    Block block = new Block();
                    block.id = index;
                    block.button = Instantiate(models[index]);
                    block.button.transform.parent = gameObject.transform;
                    block.button.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, -transform.parent.localRotation.z));
                    block.button.transform.localPosition = new Vector3(step * (i - (col - 1) * 0.5f), step * (j - (row - 1) * 0.5f), thick * k);
                    //Debug.Log(block.button.transform.localPosition);
                    block.button.transform.localScale = new Vector3(scale, scale, scale);
                    queues[i + j * col].Enqueue(block);
                }
            }
        }
        initialized = true;
    }

    public void OnDeath () {
        foreach (Queue<Block> blocks in queues) {
            foreach (Block block in blocks) {
                DestroyBlock(block);
            }
        }
        Destroy(gameObject);
    }

    void OnButtonPress (int id) {
        if (Time.time - timestamp < cd)
            return;
        bool isempty = true;
        foreach (Queue<Block> blocks in queues) {
            if (blocks.Count > 0) {
                if (blocks.Peek().id == id) {
                    Block block = blocks.Dequeue();
                    DestroyBlock(block);
                    timestamp = Time.time;
                }
                isempty = false;
            }
        }
        if (isempty) {
            Destroy(gameObject);
        }
    }

    void DestroyBlock (Block block) {
        GameObject.Find("FeedbackController").GetComponent<FeedbackController>().playRightFeedback(block.button);
        Destroy(block.button);
        block.id = -1;
    }
}
