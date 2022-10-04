using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    public static PlayerSkins Instance { get; private set; }
    
    public List<Material> skins;

    private void Awake()
    {
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
    
    public int selectRandomSkinIndex(NetworkList<CapsuleNetwork.PlayerSkin> playersSelectedSkins )
    {
        // TODO - BUG - Player selected skins is always empty

        // Get all taken skins
        List<Material> takenSkins = new();
        
        foreach (CapsuleNetwork.PlayerSkin player in playersSelectedSkins)
        {
            Debug.Log("[DEBUG LOG] Player: " + player.clientId + " Skin: " + player.skinIndex);
            takenSkins.Add(skins[player.skinIndex]);
        }
        
        // Filter taken skins from list
        List<Material> availableSkins = skins.Where(skin => !takenSkins.Contains(skin)).ToList();
        
        Debug.Log("AVAILABLE SKIN NAMES: ");
        
        // Print each available skin name
        foreach (Material skin in availableSkins)
        {
            Debug.Log(skin.name);
        }
        
        // Select random skin
        int randomSkinIndex = Random.Range(0, availableSkins.Count);
        
        // Index of original list, for chosen skin
        return skins.IndexOf(availableSkins[randomSkinIndex]);
    }
}
