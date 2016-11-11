using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour
{
    public Transform ReferencePosition;

    public Vector2 Offset = new Vector2();

    public int PregenerateNumber = 5;

    public float DistanceOffset = 7.0f;

    private int numberGenerated = 0;

    private Vector3 m_referencePosition;

    void Start()
    {
        m_referencePosition = ReferencePosition.position;

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

        chunk.transform.position = m_referencePosition + new Vector3(0, 0, DistanceOffset * numberGenerated);

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
        m_chunks.ForEach(o =>
        {
            float distance = o.transform.position.z - ReferencePosition.position.z;

            if (distance < -10)
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

        Debug.Log(numberGenerated);
    }

    bool m_needNewChunk = false;
    List<GameObject> m_chunks = new List<GameObject>();
}
