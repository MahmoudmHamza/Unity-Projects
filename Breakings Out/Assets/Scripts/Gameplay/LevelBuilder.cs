using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject KnifePrefab;
    [SerializeField]
    private GameObject[] BlocksPrefab;
    [SerializeField]
    private GameObject KingPrefab;

    public WeaponBtn weaponBtnPressed { get; set; }

    private GameObject currentWeapon;

    [SerializeField]
    private GameObject spawnPt;
    [SerializeField]
    private GameObject kingspawnPt;

    [SerializeField]
    private int rowNum = 3;

    private int spawnNumber;

    Vector3 spawnLoc = new Vector3(-7, 3, 0);

    void Start () {
        MakeWeapon(KnifePrefab);
        buildBlocks();

        GameObject kingSpawn = Instantiate(KingPrefab);
        kingSpawn.transform.position = kingspawnPt.transform.position;
    }
	
	void Update () {
		
	}

    void buildBlocks()
    {
        for(int k = 0; k < rowNum; k++)
        {
            spawnLoc.y -= 1;
            for (int i = 0; i < 16; i += 2)
            {
                Vector3 newspawnLoc = new Vector3(spawnLoc.x + i, spawnLoc.y, spawnLoc.z);
                makeBlock(newspawnLoc);
            }
        }
    }

    void makeBlock(Vector3 newSpawnLoc)
    {
        spawnNumber = Random.Range(0, 100);
        if(spawnNumber >= 0 && spawnNumber < 75)
        {
            GameObject newBlock = Instantiate(BlocksPrefab[0]);
            newBlock.transform.position = newSpawnLoc;
        }
        else if (spawnNumber >= 75 && spawnNumber < 85)
        {
            GameObject newBlock = Instantiate(BlocksPrefab[2]);
            newBlock.transform.position = newSpawnLoc;
            //freeze
        }
        else if (spawnNumber >= 85 && spawnNumber < 95)
        {
            GameObject newBlock = Instantiate(BlocksPrefab[3]);
            newBlock.transform.position = newSpawnLoc;
            //speed
        }
        else
        {
            GameObject newBlock = Instantiate(BlocksPrefab[1]);
            newBlock.transform.position = newSpawnLoc;
            //bonus
        }
    }

    public void MakeWeapon(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon);
        newWeapon.transform.position = spawnPt.transform.position;
        currentWeapon = newWeapon;
    }

    public void selectWeaponBtn(WeaponBtn weaponBtn)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
            weaponBtnPressed = weaponBtn;
            MakeWeapon(weaponBtnPressed.WeaponObject);
        }
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.WeaponChange);
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.WeaponChange2);
    }
}
