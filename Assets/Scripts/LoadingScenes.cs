using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using CatAsset;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class LoadingScenes : MonoBehaviour
    {
        
        public TextMeshProUGUI _Text;
        public RectTransform progress;
        
        // Start is called before the first frame update
        void Start()
        {
            progress.sizeDelta = new Vector2(0, progress.rect.height);
            this.checkManifest();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void checkManifest()
        {
            //需要先调用CatAssetManager.CheckPackageManifest读取SteamingAssets目录内的资源清单文件，然后才能加载资源
            CatAssetManager.CheckPackageManifest((success) => {
                if (!success)
                {
                    _Text.text = "Check Asset Manifest Error";
                    Debug.LogError("单机模式检查资源清单失败");
                    return;
                }

                Debug.Log("单机模式检查资源清单成功");
                
                this.loadScenes();
            });
        }


        private void loadScenes()
        {
            CatAssetManager.LoadScene("Assets/Scenes/GameScene.unity", (success, asset) =>
            {
                if (success)
                {
                    Debug.Log("加载Cube");

                    SceneManager.LoadScene("GameScene");
                }
                   
            });
        }
        
    }

}
