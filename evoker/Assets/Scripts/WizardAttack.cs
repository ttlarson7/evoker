using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public Spell spellPrefab;  // Reference to the Spell prefab
    public WindSpell windPrefab;  // Reference to the WindSpell prefab
    public Transform spawnPoint;  // Where to spawn the spell
    public float spawnInterval = 4f;  // Time interval for spawning a spell (in seconds)

    // Start is called before the first frame update
    void Start()
    {
        // Start calling the SpawnSpell method every 4 seconds
        InvokeRepeating("SpawnSpell", 0f, spawnInterval);
    }

    // Method to spawn a random spell and set its velocity
    void SpawnSpell()
    {
        // Randomly choose between spellPrefab and windPrefab
        GameObject spellToSpawn = Random.Range(0f, 1f) > 0.5f ? spellPrefab.gameObject : spellPrefab.gameObject;
        Spell spell = spellToSpawn.GetComponent<Spell>();
        spell.velocity = -1;
        // Instantiate the selected spell at the spawn point
        Vector3 spawnPositionWithOffset = new Vector3(spawnPoint.position.x, spawnPoint.position.y - 1.5f, spawnPoint.position.z);

        // Instantiate the selected spell at the new position with the offset
        GameObject spawnedSpell = Instantiate(spellToSpawn, spawnPositionWithOffset, Quaternion.identity);
        
        
    }
}
