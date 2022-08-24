﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAsset
{
    /// <summary>
    /// CatAsset资源清单
    /// </summary>
    public class CatAssetManifest
    {
        /// <summary>
        /// 游戏版本号
        /// </summary>
        public string GameVersion;

        /// <summary>
        /// 清单版本号
        /// </summary>
        public int ManifestVersion;

        /// <summary>
        /// 所有Bundle清单信息
        /// </summary>
        public BundleManifestInfo[] Bundles;

        /// <summary>
        /// 导出AssetMnifest 时，先导出一个全部的资源，导出远端和本地的时候用构造函数copy一个对象出来写入数据
        /// </summary>
        /// <param name="manifest"></param>
        public CatAssetManifest(CatAssetManifest manifest)
        {
            this.GameVersion = manifest.GameVersion;
            this.ManifestVersion = manifest.ManifestVersion;
            Bundles = (BundleManifestInfo[])manifest.Bundles.Clone();
        }

        public CatAssetManifest()
        {
            
        }
    }
}

