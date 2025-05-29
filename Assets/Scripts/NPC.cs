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

    private void Start()
    {
        dialog.gameObject.SetActive(false);
        NpcDialogPannelCanvasGroup.alpha = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        throw new NotImplementedException();
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