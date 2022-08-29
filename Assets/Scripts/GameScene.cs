using System.Collections;
using System.Collections.Generic;
using CatAsset;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        
        // CatAssetManager.LoadAssetAsync("Assets/Bundes/Dialogs/SettingDialog/Prefabs/PopupSetting.prefab", (success, asset) =>
        // {
        //     if (success)
        //     { 
        //         var settingDialog = Instantiate((GameObject)asset);
        //         settingDialog.transform.SetParent(transform, false);
        //     }
        //            
        // });
        
        
        CatAssetManager.LoadAsset("Assets/Bundes/Dialogs/SettingDialog/Prefabs/PopupSetting.prefab", (success, asset) =>
        {
            if (success)
            { 
                var settingDialog = Instantiate((GameObject)asset);
                settingDialog.transform.SetParent(transform, false);
            }
                   
        });
    }
    
}
