using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_DialogData dialogData;

    [SerializeField]
    private Dialog dialog;

    [SerializeField]
    private CanvasGroup NpcDialogPannelCanvasGroup;
    
    [SerializeField]
    private PlayerInput playerInput;
    private void Start()
    {
        dialog.gameObject.SetActive(false);
        NpcDialogPannelCanvasGroup.alpha                     =  0;
    }

    private void OnEnable()
    {
        var interact  = playerInput.actions.FindAction("Interact");
        playerInput.actions.FindAction("Interact").performed += OnInterActionPerformed;
    }

    private void OnDisable()
    {
        playerInput.actions.FindAction("Interact").performed -= OnInterActionPerformed;
    }

    private void OnInterActionPerformed(InputAction.CallbackContext obj)
    {
        if (dialog.IsInDialog())return;
        
        if (dialogHintOpen)
        {
            Debug.Log("NPC open");
            //dialog.OpenDialog();
            PlayDialog();
            
            
        }
    }

    private bool dialogHintOpen ;
    private void OnTriggerEnter2D(Collider2D other)
    {
       //判斷進入的物件是否有characterController
        if (other.gameObject.TryGetComponent(out CharacterController character))
        {
            dialogHintOpen = true;
            ShowDialog();
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterController character))
        {
            dialogHintOpen = false;
            HideDialog();
        }
    }

    [Button("顯示對話提示")]
    private void ShowDialog()
    {
        
        NpcDialogPannelCanvasGroup.DOFade(1, 0.5f);
        
    }
    [Button("隱藏對話提示")]
    private void HideDialog()
    {
        NpcDialogPannelCanvasGroup.DOFade(0, 0.5f);
        
    }
    
    

    [Button("顯示第一段對話")]
    public void PlayDialog()
    {
        dialog.gameObject.SetActive(true);
        dialog.setTexts(dialogData.dialogTexts);
        dialog.PlayDialog();
    }

    [Button("顯示第二段對話")]
    public void PlayNextDialog()
    {
        dialog.setTexts(dialogData.dialogTexts);
        dialog.PlayNextDialog();
    }
    

    [Button("Skip對話")]
    public void SkipDialog()
    {
        dialog.SkipWriter();
    }

    
    
}