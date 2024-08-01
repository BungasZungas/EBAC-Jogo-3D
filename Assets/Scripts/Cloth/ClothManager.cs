using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUNGAS.Core.Singleton;

namespace Cloth
{

    public enum ClothType
    {
        TEXTURE,
        SPEED,
        STRONG,
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }

        private void Start()
        {
            SaveManager.Instance.InitializePlayerCloth();
        }
    }

    [System.Serializable]
    public class ClothSetup 
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}