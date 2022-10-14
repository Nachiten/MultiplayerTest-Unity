#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class PlayerSkins : MonoBehaviour
{
    [SerializeField] private List<Material> skins;

    private List<int> takenSkins = new();

    public static PlayerSkins Instance { get; private set; }

    private void Awake()
    {
        if (skins.Count == 0) Debug.LogError("No skins found!");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int getRandomSkinIndex()
    {
        // Get skins which are not taken
        List<Material> availableSkins = skins.Where((_, index) => !takenSkins.Contains(index)).ToList();

        // Log available skins count
        Debug.Log($"Available skins: {availableSkins.Count}");

        // Random index for available skins
        int randomIndex = Random.Range(0, availableSkins.Count);

        // Choose skin from available
        Material shosenSkin = availableSkins[randomIndex];

        // Choose index of that skin from original list
        int chosenIndex = skins.IndexOf(shosenSkin);

        // Chosen index is added to taken skins
        takenSkins.Add(chosenIndex);

        return chosenIndex;
    }

    public Material getSkin(int index)
    {
        return skins[index];
    }
}