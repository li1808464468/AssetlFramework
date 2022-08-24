using CatAsset;
using CatJson;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util = CatAsset.Util;

public class UpdatableExample : MonoBehaviour
{

    /// <summary>
    /// 加载进度条
    /// </summary>
    public RectTransform Progress;

    public Text ProgressText;

    private long sumTotalLength = 0;
    private long downloadTotalLength = 0;
    private bool loadScenesTag = false;
    
    
    /// <summary>
    /// 资源更新地址前缀
    /// </summary>
    public string UpdateUriPrefix;

    private bool needUpdateByChapter1;
    private bool needUpdateByChapter2;

    private bool inited;

    private void Awake()
    {
        //注意，请先点击CatAsset/WebServer/NetBox2.exe打开资源服务器

        //可更新模式
        //1.先请求最新的资源版本号
        //2.根据平台，整包版本和资源版本设置资源更新uri的前缀
        //3.检查资源版本
        //4.下载指定资源组的所有需要更新的资源

        //请求最新的资源清单版本号
        string versionTxtUri = UpdateUriPrefix + "/version.txt";
        Debug.Log(versionTxtUri);
        UnityWebRequest uwr = UnityWebRequest.Get(versionTxtUri);
        uwr.timeout = 5;
        UnityWebRequestAsyncOperation op = uwr.SendWebRequest();
        op.completed += (obj) =>
        {
            if (op.webRequest.isNetworkError || op.webRequest.isHttpError)
            {
                Debug.Log("读取远端最新版本号失败：" + op.webRequest.error);
                updateFileSuccess();
                return;
            }
            JsonObject jo = JsonParser.ParseJson(op.webRequest.downloadHandler.text);
            int manifestVefsion = (int)jo["ManifestVersion"].Number;
            string platform = "/Android/";

            //根据平台，整包版本和资源版本设置资源更新uri的前缀
            CatAssetManager.UpdateUriPrefix = UpdateUriPrefix + platform + Application.version + "_" + manifestVefsion;
            Debug.Log(CatAssetManager.UpdateUriPrefix);
            Debug.Log("读取远端最新版本号成功");
            
            CatAssetManager.CheckVersion(OnVersionChecked);

        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //检查各个组的资源
            CatAssetManager.CheckVersion(OnVersionChecked);
        }

        if (inited)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                //由于Base组的资源是首包资源，所以这里只更新Chapter1和Chapter2组的

               
                CatAssetManager.UpdateAssets(OnFileDownloaded, "Must");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                CatAssetManager.UpdateAssets(OnFileDownloaded, "Base");
            }
            
        }


        if (sumTotalLength > 0 && !loadScenesTag)
        {
            Progress.localScale = new Vector3(downloadTotalLength * 1.0f / sumTotalLength, 1, 1);
            if (downloadTotalLength == sumTotalLength)
            {
                loadScenesTag = true;
                updateFileSuccess();
            }
            
        }

       


    }

    private void OnVersionChecked(int count, long length)
    {
        inited = true;
        
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("总的需要更新资源数：" + count);
        sb.AppendLine("总大小：" + length);

        Debug.Log(sb.ToString());
        sb.Clear();

        if (count == 0)
        {
            this.updateFileSuccess();
            return;
        }

        
       List<Updater> updaters =  CatAssetManager.GetAllUpdater();
       foreach (Updater updater in updaters)
        {
            sb.AppendLine("需要更新的资源组：" + updater.UpdateGroup);
            sb.AppendLine("更新文件数：" + updater.TotalCount);
            sb.AppendLine("更新大小：" + updater.TotalLength);
            sumTotalLength += updater.TotalLength;
            Debug.Log(sb.ToString());
            sb.Clear();
        }
       
       foreach (Updater updater in updaters)
       {
           CatAssetManager.UpdateAssets(OnFileDownloaded, updater.UpdateGroup);
       }
       


    }

    private void OnFileDownloaded(bool success, int updatedCount, long updatedLength, int totalCount, long totalLength, string fileName, string group)
    {
        if (!success)
        {
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("已更新数量：" + updatedCount);
        sb.AppendLine("已更新大小：" + updatedLength);
        sb.AppendLine("总数量：" + totalCount);
        sb.AppendLine("总大小：" + totalLength);
        sb.AppendLine("资源名：" + fileName);
        sb.AppendLine("资源组：" + group);
        downloadTotalLength += totalLength;

        Debug.Log(sb.ToString());
        sb.Clear();

        if (updatedCount >= totalCount)
        {
            Debug.Log(group + "组的所有资源下载完毕");

            Debug.Log($"请打开{Application.persistentDataPath}查看");
        }
        
        
    }

    private void updateFileSuccess()
    {
        CatAssetManager.CheckPackageManifest((success) => {
            if (!success)
            {
                Debug.LogError("检查资源清单失败");
                return;
            }

            Debug.Log("检查资源清单成功");
            
            loadScenes();
                
        });
    }
    
    private void loadScenes()
    {
        CatAssetManager.LoadScene("Assets/Scenes/GameScene.unity", (success, asset) =>
        {
            if (success)
            {
                SceneManager.LoadScene("GameScene");
            }
                   
        });
    }
    
}
