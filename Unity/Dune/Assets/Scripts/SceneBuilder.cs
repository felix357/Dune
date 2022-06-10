using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Builds the map according to the given configuration
*/
public class SceneBuilder : MonoBehaviour
{

    //public GameObject cityNodePrefab, duneNodePrefab, FlatDuneNodePrefab, FlatRockNodePrefab, rockNodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        initMap();
        createEnemys();

    }



    /**
     * Little Test Method
     */
    public void initMap()
    {
        MapManager nodeManager = MapManager.instance;
        int gridSizeX = 11;
        int gridSizeZ = 11;
        nodeManager.setMapSize(gridSizeX, gridSizeZ);
        for (int x = 0; x < gridSizeZ; x++)
        {
            for (int z = 0; z < gridSizeX; z++)
            {
                if(z + x * gridSizeZ == 7 || x*gridSizeZ + z == gridSizeZ * gridSizeX - 7)
                {
                    nodeManager.UpdateBoard(x, z, false, NodeTypeEnum.CITY, false);
                   
                }
                else if((z+x* gridSizeZ) % 5 == 0)
                {
                    nodeManager.UpdateBoard(x, z, false, NodeTypeEnum.ROCK, false);
                }
                else if((z + x * gridSizeZ) % 7 == 0)
                {
                    nodeManager.UpdateBoard(x, z, false, NodeTypeEnum.DUNE, false);
                }
                else if((z + x * gridSizeZ) % 3 == 0)
                {
                    nodeManager.UpdateBoard(x, z, false, NodeTypeEnum.FLATROCK, false);
                }

                else
                {
                    nodeManager.UpdateBoard(x, z, false, NodeTypeEnum.FLATDUNE, false);
                }


            }



        }


    }

    public void createEnemys()
    {

        CharacterMgr.instance.spawnCharacter(1, CharTypeEnum.NOBLE, 0, 0, 100, 10, 10, 10, false, false);
        CharacterMgr.instance.spawnCharacter(2, CharTypeEnum.BENEGESSERIT, 0, 2, 100, 10, 10, 10, false, false);
        CharacterMgr.instance.spawnCharacter(3, CharTypeEnum.FIGHTER, 0, 4, 100, 10, 10, 10, false, false);
        CharacterMgr.instance.spawnCharacter(4, CharTypeEnum.MENTANT, 0, 6, 100, 10, 10, 10, false, false);
    }

 
}
