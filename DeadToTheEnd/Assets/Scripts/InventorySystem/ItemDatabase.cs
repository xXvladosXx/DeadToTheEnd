using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "InventorySystem/ItemDatabase")]
    public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [field: SerializeField] public Item[] InventoryItems { get; private set; }
        private Dictionary<int, Item> _idItems = new Dictionary<int, Item>();

        private void OnValidate()
        {
            LoadItems();
            UpdateID();
        }

        private void LoadItems()
        {
            //InventoryItems = FindAssetsByType<Item>().ToArray();
        }

        public List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for( int i = 0; i < guids.Length; i++ )
            {
                string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
                T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
                if( asset != null )
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
        
        public void UpdateID()
        {
            _idItems.Clear();
            
            for (int i = 0; i < InventoryItems.Length; i++)
            {
                if (InventoryItems[i].ItemData.Id != i)
                {
                    InventoryItems[i].ItemData.Id = i;
                }
                _idItems.Add(i, InventoryItems[i]);
            }
        }
        
        public Item GetItemByID(int id)
        {
            _idItems.TryGetValue(id, out var item);

            return item;
        }

        public void OnBeforeSerialize()
        {
            UpdateID();
        }

        public void OnAfterDeserialize()
        {
            UpdateID();
        }
    }
}