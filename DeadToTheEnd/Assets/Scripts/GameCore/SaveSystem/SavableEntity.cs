using System.Collections.Generic;
using Entities.Core;
using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
    public class SavableEntity : MonoBehaviour
   {
      [SerializeField] private string _uniqueIdentifier = System.Guid.NewGuid().ToString();
      private static Dictionary<string, SavableEntity> globalLookThrough = new Dictionary<string, SavableEntity>();
      
      public string GetUniqueIdentifier()
      {
         return _uniqueIdentifier;
      }

      public object CaptureState()
      {
         Dictionary<string, object> state = new Dictionary<string, object>();
      
         foreach (var savable in GetComponents<ISavable>())
         {
            state[savable.GetType().ToString()] = savable.CaptureState();
         }

         return state;
      }
   
      public void RestoreState(object state)
      {
         Dictionary<string, object> restoredState = state as Dictionary<string, object>;

         foreach (var savable in GetComponents<ISavable>())
         {
            string savableSerialize = savable.GetType().ToString();
            if (state is Dictionary<string,object> records)
            {
               if(!restoredState.ContainsKey(savableSerialize)) continue;
               
               savable.RestoreState(restoredState[savableSerialize]);
            }
         }
      }

#if UNITY_EDITOR
      private void Update()
      {
         if(Application.IsPlaying(gameObject)) return;
         if(string.IsNullOrEmpty(gameObject.scene.path)) return;

         SerializedObject serializedObject = new SerializedObject(this);
         SerializedProperty serializedProperty = serializedObject.FindProperty("_uniqueIdentifier");

         if (string.IsNullOrEmpty(serializedProperty.stringValue) || !IsUnique(serializedProperty.stringValue))
         {
            serializedProperty.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
         }

         globalLookThrough[serializedProperty.stringValue] = this;
      }

      private bool IsUnique(string candidate)
      { 
         if(!globalLookThrough.ContainsKey(candidate))
         {
            return true;
         }

         if (globalLookThrough[candidate] == this)
         {
            return true;
         }

         if (globalLookThrough[candidate] == null)
         {
            globalLookThrough.Remove(candidate);
            return true;
         }

         if (globalLookThrough[candidate].GetUniqueIdentifier() != candidate)
         {
            globalLookThrough.Remove(candidate);
            return true;
         }
      
         return false;
      }
#endif
   }
}