using System.Runtime.CompilerServices;
using NaughtyAttributes;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_DialogData dialogData;

    [SerializeField]
    private Dialog dialog;

    
    
    
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