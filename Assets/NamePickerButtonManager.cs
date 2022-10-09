using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NamePickerButtonManager : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject namePickerCanvas;
    
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        namePickerCanvas.SetActive(false);
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerLabelManager>().setPlayerName(inputField.text);
    }
}
