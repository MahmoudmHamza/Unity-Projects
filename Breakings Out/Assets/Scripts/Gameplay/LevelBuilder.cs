using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject KnifePrefab;

    [SerializeField]
    private GameObject[] BlocksPrefab;

    [SerializeField]
    private GameObject KingPrefab;

    [SerializeField]
    private GameObject spawnPt;

    [SerializeField]
    private GameObject kingspawnPt;

    [SerializeField]
    private int rowNum = 3;

    public WeaponBtn weaponBtnPressed { get; set; }

    Vector3 initialSpawnLocation = new Vector3(-7, 3, 0);

    private GameObject currentWeapon;

    private int spawnNumber;

    void Start ()
    {
        this.CreateWeapon(this.KnifePrefab);
        this.BuildBlocks();

        var kingSpawn = Instantiate(this.KingPrefab);
        kingSpawn.transform.position = this.kingspawnPt.transform.position;
    }
	
    private void BuildBlocks()
    {
        for(int k = 0; k < rowNum; k++)
        {
            this.initialSpawnLocation.y -= 1;
            for (int i = 0; i < 16; i += 2)
            {
                Vector3 newspawnLoc = new Vector3(this.initialSpawnLocation.x + i, this.initialSpawnLocation.y, this.initialSpawnLocation.z);
                this.CreateBlock(newspawnLoc);
            }
        }
    }

    private void CreateBlock(Vector3 newSpawnLoc)
    {
        spawnNumber = Random.Range(0, 100);
        if(spawnNumber >= 0 && spawnNumber < 75)
        {
            var newBlock = Instantiate(this.BlocksPrefab[0]);
            newBlock.transform.position = newSpawnLoc;
        }
        else if (spawnNumber >= 75 && spawnNumber < 85)
        {
            var newBlock = Instantiate(this.BlocksPrefab[2]);
            newBlock.transform.position = newSpawnLoc;
        }
        else if (spawnNumber >= 85 && spawnNumber < 95)
        {
            var newBlock = Instantiate(this.BlocksPrefab[3]);
            newBlock.transform.position = newSpawnLoc;
        }
        else
        {
            var newBlock = Instantiate(this.BlocksPrefab[1]);
            newBlock.transform.position = newSpawnLoc;
        }
    }

    public void CreateWeapon(GameObject weapon)
    {
        var newWeapon = Instantiate(weapon);
        newWeapon.transform.position = this.spawnPt.transform.position;
        this.currentWeapon = newWeapon;
    }

    public void selectWeaponBtn(WeaponBtn weaponBtn)
    {
        if(currentWeapon != null)
        {
            Destroy(this.currentWeapon.gameObject);
            weaponBtnPressed = weaponBtn;
            this.CreateWeapon(weaponBtnPressed.WeaponObject);
        }

        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.WeaponChange);
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.WeaponChange2);
    }
}
