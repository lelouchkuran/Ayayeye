using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct Block {
    public int id;
    public GameObject button;
};

public class BlockBehavior : MonoBehaviour {
    public GameObject[] models;
    static float timestamp;
    Queue<Block>[] queues;
    bool initialized = false;
    const float step = 1.0f;
    const float cd = 2.0f;

    void Start () {
        OnBirth(2, 3, 2);
    }

    // Update is called once per frame
    void Update () {
        if (initialized) {
            if (Input.GetKeyDown(KeyCode.W))
                OnButtonPress(0);
            if (Input.GetKeyDown(KeyCode.S))
                OnButtonPress(1);
            if (Input.GetKeyDown(KeyCode.A))
                OnButtonPress(2);
            if (Input.GetKeyDown(KeyCode.D))
                OnButtonPress(3);
        }
    }

    public void OnBirth (int row, int col, int num_layer) {
        queues = new Queue<Block>[row * col];
        for (int i = 0; i < col; ++i) {
            for (int j = 0; j < row; ++j) {
                queues[i + j * col] = new Queue<Block>();
                for (int k = 0; k < num_layer; ++k) {
                    int index = Random.Range(0, models.Length - 1);
                    Vector3 pos = new Vector3(step * i, step * j, step * k);
                    Block block = new Block();
                    block.id = index;
                    block.button = Instantiate(models[index]);
                    block.button.transform.parent = transform;
                    block.button.transform.localPosition = pos;
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

    void DestroyBlock(Block block) {
        Destroy(block.button);
        block.id = -1;
    }
}
