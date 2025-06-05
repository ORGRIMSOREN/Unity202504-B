using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPEffects.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private TMPWriter tmpWriter;
    
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private CanvasGroup nextDialoghintCanvasGroup;
    
    [SerializeField]
    CharacterController characterController;
    
    public static Dialog instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tmpWriter.OnFinishWriter.AddListener(OnFinishWriter);
        tmpWriter.OnStartWriter.AddListener(OnStartWriter);
    }
    

    private void OnStartWriter(TMPWriter arg0)
    {
        nextDialoghintCanvasGroup.alpha = 0f;
    }

    private void OnFinishWriter(TMPWriter arg0)
    {
        nextDialoghintCanvasGroup.alpha = 1;
    }

    private void OnEnable()
    {
        var interactAction = playerInput.actions.FindAction("Interact");
        interactAction.performed+= InteractActionOnperformed;
        
    }

    private void OnDisable()
    {
        var interactAction = playerInput.actions.FindAction("Interact");
        interactAction.performed -= InteractActionOnperformed;
    }

    private void InteractActionOnperformed(InputAction.CallbackContext obj)
    {
        
        //如果對話完畢(最後一段話)，則關閉對話框
        if (tmpWriter.IsWriting == false && dialogIndex + 1 == dialogTexts.Count)
        {
           //對話結束回復角色的行動狀態
            characterController.SetCanMove(true); 
            CloseDialog();
            
        }
        //如果還在播放打字機效果，則Skip打字機效果
        if (tmpWriter.IsWriting)
        SkipWriter();
        //如果打字機效果結束，則播放下一段文字
        else if (tmpWriter.IsWriting == false)
        {
            PlayNextDialog();
        }

    }

    /// <summary>
    /// 紀錄對話索引
    /// </summary>
    private int dialogIndex = 0;
    
    public List<string>dialogTexts;

    [Button("播放打字機效果")]
    public void PlayWriter()
    {
        tmpWriter.RestartWriter();
    }

    [Button("跳過目前的打字機效果")]
    public void SkipWriter()
    {
        tmpWriter.SkipWriter();
    }

    public void SetText(string dialogText)
    {
       tmpWriter.SetText(dialogText); 
    }

    public void PlayDialog()
    {
        
        //檢查資料陣列是否有異常
        if (dialogTexts.Count == 0)return;
        dialogIndex = 0; //重置索引
        isInDialog = true;
        
        var dialogText = dialogTexts[dialogIndex];
        SetText(dialogText);
        PlayWriter();
        
        characterController.SetCanMove(false);
    }

    public void PlayNextDialog()
    {
        if (dialogTexts.Count == 0)
        {
            Debug.LogError("錯誤,dialog的陣列資料為空");
            return;
        }

        //檢查索引是否超出陣列
        if (dialogIndex + 1 == dialogTexts.Count) return;
        dialogIndex++;
        var dialogText = dialogTexts[dialogIndex];
        SetText(dialogText);
        PlayWriter();
    }

    public void setTexts(List<string> Texts)
    {
        dialogTexts = Texts;
    }

    [Button("開啟對話框")]
    public void OpenDialog()
    {
        gameObject.SetActive(true);
    }
    [Button("關閉對話框")]
    public void CloseDialog()
    {
        gameObject.SetActive(false);
        isInDialog = false;
    }
    
    private bool isInDialog ;

    public bool IsInDialog()
    {
        return isInDialog;
    }

    public void setDialogPos(Vector3 position)
    {
        transform.position = position + new Vector3(0f, 0f, 0f);
    }
}
