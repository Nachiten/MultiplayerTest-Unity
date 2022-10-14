using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NamePickerButtonManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject namePickerCanvas;
    
    private Button namePickerButton;

    private void Awake()
    {
        namePickerButton = GetComponent<Button>();
    }

    private void Start()
    {
        namePickerButton.onClick.AddListener(OnNamePick);
    }

    private void OnNamePick()
    {
        namePickerCanvas.SetActive(false);
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerLabelManager>().setPlayerName(inputField.text);
    }
}
