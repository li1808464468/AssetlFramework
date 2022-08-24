using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System;

namespace CatAsset
{
    public static class Util
    {
        /// <summary>
        /// Base组都是本地资源
        /// </summary>
        public const string DefaultGroup = "Base";
        
        /// <summary>
        /// 导出的目录中为服务器文件，StreamAsset 中的为包中的资源
        /// </summary>
        public const string ManifestFileName = "AssetManifest.json";
        
        /// <summary>
        /// 全部资源信息
        /// </summary>
        public const string AllManifestFileName = "AllAssetManifest.json";

        /// <summary>
        /// 获取在只读区下的完整路径
        /// 使用UnityWebRequest加载资源需要不用拼接file:///
        /// </summary>
        public static string GetReadOnlyPath(string path)
        {
            string result = Path.Combine(Application.streamingAssetsPath, path);
            
            
            return result;
        }

        /// <summary>
        /// 获取在读写区下的完整路径
        /// 使用UnityWebRequest加载资源需要拼接file:///
        /// </summary>
        public static string GetReadWritePath(string path)
        {
            string result = Path.Combine(Application.persistentDataPath, path);
            
            return result;
        }



    }

    

}
