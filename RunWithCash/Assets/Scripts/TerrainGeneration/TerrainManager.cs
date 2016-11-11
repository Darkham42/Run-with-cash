﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour
{
    public Transform ReferencePosition;
    public Vector2 Offset = new Vector2();
    public int PregenerateNumber = 5;
    public float DistanceOffset = 7.0f;

    public GameObject CivilianCar;
    public GameObject CopCar;

    private int numberGenerated = 0;
    private Vector3 m_referencePosition;
    private int chunkOffset;
    private float timerCopSpawn = 0;
    private GameObject Player;
    public int nbrCops = 0;

    void Start()
    {
        m_referencePosition = ReferencePosition.position;
        Player = GameObject.Find("Car");

        for (numberGenerated = 0; numberGenerated < PregenerateNumber; ++numberGenerated)
        {
            AddNewChunk(false);
        }
    }

    void AddNewChunk(bool increment)
    {
        GameObject leftObject = GetComponent<BuildingPool>().AskOneObject();
        GameObject rightObject = GetComponent<BuildingPool>().AskOneObject();

        GameObject chunk = GetComponent<ChunkPool>().AskOneObject();
        chunk.GetComponent<Chunk>().LeftPart = leftObject;
        chunk.GetComponent<Chunk>().RightPart = rightObject;

        chunk.GetComponent<Chunk>().Init();

        Vector3 leftPosition = new Vector3(Offset.x, 0, 0);
        Vector3 rightPosition = new Vector3(Offset.y, 0, 0);

        leftObject.transform.localPosition = leftPosition;
        rightObject.transform.localPosition = rightPosition;

        float offset = 0.0f;

        if (chunkOffset > 0)
        {
            offset = 5.0f;
            --chunkOffset;
        }
        if (chunkOffset < 0)
        {
            offset = -5.0f;
            ++chunkOffset;
        }

        chunk.transform.position = m_referencePosition + new Vector3(offset, 0, DistanceOffset * numberGenerated);

        if (Random.Range(0, 50) > 46)
        {
            GameObject tmp = GameObject.Instantiate(CivilianCar) as GameObject;
            tmp.transform.position = chunk.transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0, 0);
        }

        m_chunks.Add(chunk);

        if (increment)
        {
            ++numberGenerated;
        }
    }

    void FreeChunk(GameObject chunk)
    {
        GetComponent<BuildingPool>().FreeObject(chunk.GetComponent<Chunk>().LeftPart);
        GetComponent<BuildingPool>().FreeObject(chunk.GetComponent<Chunk>().RightPart);

        GameObject leftPart = chunk.GetComponent<Chunk>().LeftPart;
        GameObject rightPart = chunk.GetComponent<Chunk>().RightPart;

        leftPart.transform.parent = null;
        rightPart.transform.parent = null;

        GetComponent<ChunkPool>().FreeObject(chunk);

        m_chunks.Remove(chunk);
    }

    void Update()
    {
        timerRandom -= Time.deltaTime;

        if (timerRandom <= 0.0f)
        {
            timerRandom = 5.0f;
            chunkOffset = Random.Range(-5, 5);
            currentOffset = 0;
        }

        m_chunks.ForEach(o =>
        {
            float distance = o.transform.position.z - ReferencePosition.position.z;

            if (distance < -30)
            {
                FreeChunk(o);

                m_needNewChunk = true;
            }
        });

        if (m_needNewChunk)
        {
            m_needNewChunk = false;
            AddNewChunk(true);
        }

        // Génération des voitures de Police
        timerCopSpawn += Time.deltaTime;
        if (timerCopSpawn > 4 && nbrCops <= 5)
        {
            timerCopSpawn = 0;
            GameObject tmp = GameObject.Instantiate(CopCar) as GameObject;
            tmp.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 0, Player.transform.position.z - 30);
            nbrCops++;
            Debug.Log("enemis : " + nbrCops);
        }

        //Debug.Log(numberGenerated);
    }

    int currentOffset = 0;
    float timerRandom = 5.0f;
    bool m_needNewChunk = false;
    List<GameObject> m_chunks = new List<GameObject>();
}
