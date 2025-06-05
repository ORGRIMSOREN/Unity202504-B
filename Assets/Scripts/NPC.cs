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
    private CanvasGroup NpcDialogPannelCanvasGroup;
    
    [SerializeField]
    private PlayerInput playerInput;
    private void Start()
    {
        Dialog.instance.gameObject.SetActive(false);
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
        if (Dialog.instance.IsInDialog())return;
        
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
        Dialog.instance.setDialogPos(transform.position);
        Dialog.instance.gameObject.SetActive(true);
        Dialog.instance.setTexts(dialogData.dialogTexts);
        Dialog.instance.PlayDialog();
    }

    [Button("顯示第二段對話")]
    public void PlayNextDialog()
    {
        Dialog.instance.setTexts(dialogData.dialogTexts);
        Dialog.instance.PlayNextDialog();
    }
    

    [Button("Skip對話")]
    public void SkipDialog()
    {
        Dialog.instance.SkipWriter();
    }

    
    
}