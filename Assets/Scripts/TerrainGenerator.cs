using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    [SerializeField] private int m_MaxTerrainCount;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();
    [SerializeField] private Transform m_TerrainHolder;
    
    private List<GameObject> m_CurrentTerrains = new List<GameObject>();
    [HideInInspector] public Vector3 m_CurrentPosition = new Vector3(0,0,0);

    public List<Transform> m_CurrentObstacles = new List<Transform>();


    void Start()
    {
        for (int i = 0; i < m_MaxTerrainCount; i++)
        {
            SpawnTerrain(true, new Vector3(0,0,0));
        }
        m_MaxTerrainCount += m_CurrentTerrains.Count;
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {
        if((m_CurrentPosition.x - playerPos.x < minDistanceFromPlayer) || isStart)
        {
            int whichTerrain = Random.Range(0, terrainDatas.Count); //select which terrain
            int terrainInARow = Random.Range(1, terrainDatas[whichTerrain].maxInARow); //how many of that terrain in a row

            for (int i = 0; i < terrainInARow; i++)
            {
                //spawn terrain with number in a row
                GameObject terrain = Instantiate(terrainDatas[whichTerrain].terrainList[Random.Range(0,terrainDatas[whichTerrain].terrainList.Count)], m_CurrentPosition, Quaternion.identity, m_TerrainHolder);

                // if(GetChildWithTag(terrain, "Obstacle").tag == "Obstacle")
                // {
                //     m_CurrentObstacles.Add(terrain.transform);
                // }
                m_CurrentTerrains.Add(terrain);

                // for (int o = 0; o < m_CurrentTerrains.Count; o++)
                // {
                //     if(GetChildWithTag(m_CurrentTerrains[o], "Obstacle").tag == "Obstacle")
                //     {
                //         m_CurrentObstacles.Add(m_CurrentTerrains[o].transform);
                //     }
                // }
                if(!isStart)
                {
                    if(m_CurrentTerrains.Count > m_MaxTerrainCount) //Removes previous terrain
                    {
                        Destroy(m_CurrentTerrains[0]);
                        m_CurrentTerrains.RemoveAt(0);
                    }
                }
                m_CurrentPosition.x++;
                Debug.Log(m_CurrentObstacles.Count);
            }
        }
    }

    public Transform GetChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        for (int i = 0; i < t.childCount; i++)
        {
            if(t.GetChild(i).tag == tag)
            {
                return t.GetChild(i).gameObject.transform;
            }
        }
        return null;
    }
}
