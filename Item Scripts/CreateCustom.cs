using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ACTAP
{
    class CreateCustom : MonoBehaviour
    {
        static GameObject itemPrefab = Plugin.LoadFromAssetBundle("Item");
        public static void CreateItemWhenPossible(Vector3 position, string name, ItemSwapData.ItemEnum itemToGet, MonoBehaviour inst)
        {
            //inst.StartCoroutine(CreateItemWait(position, name, itemToGet));
            CreateItemFromAB(position, name, itemToGet);
        }

        public static IEnumerator CreateItemWait(Vector3 position, string name, ItemSwapData.ItemEnum itemToGet)
        {
            float waitTimeOut = 20.0f;
            float timeWaited = 0;

            for (float i = 0; i < waitTimeOut; i += 0.1f)
            {
                if (Plugin.itemHolder == null)
                {
                    timeWaited += 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    CreateItem(position, name, itemToGet);
                    yield break;
                }
            }
            Debug.Log("ItemTimeOut");
        }

        public static void CreateItem(Vector3 position,string name, ItemSwapData.ItemEnum itemToGet)
        {
            GameObject customItem = GameObject.Instantiate(Plugin.itemHolder);
            customItem.name = name;
            Item itemComponent = customItem.GetComponent<Item>();
            SaveStateKillableEntity save = customItem.GetComponent<SaveStateKillableEntity>();
            DistanceCull cull = customItem.GetComponent<DistanceCull>();
            customItem.transform.position = position;

            itemComponent.item = ScriptableObject.CreateInstance<CollectableItemData>();
            itemComponent.item.name = ItemSwapData.GetItemJson(itemToGet);

            //Replace all item data with masterlist item that matches name
            foreach (var listItem in InventoryMasterList.staticList)
            {
                if (listItem.name == itemComponent.item.name)
                {
                    itemComponent.item = listItem;
                }
            }

            itemComponent.isInstantiated = true;

            customItem.SetActive(true);
            itemComponent.enabled = true;
            save.enabled = true;
            cull.enabled = true;
        }

        public static void CreateItemFromAB(Vector3 position, string name, ItemSwapData.ItemEnum itemToGet)
        {
            GameObject customItem = GameObject.Instantiate(itemPrefab);
            customItem.name = name;
            Item itemComponent = customItem.GetComponent<Item>();
            SaveStateKillableEntity save = customItem.GetComponent<SaveStateKillableEntity>();
            DistanceCull cull = customItem.GetComponent<DistanceCull>();
            customItem.transform.position = position;

            itemComponent.item = ScriptableObject.CreateInstance<CollectableItemData>();
            itemComponent.item.name = ItemSwapData.GetItemJson(itemToGet);


            //Replace all item data with masterlist item that matches name
            foreach (var listItem in InventoryMasterList.staticList)
            {
                if (listItem.name == itemComponent.item.name)
                {
                    itemComponent.item = listItem;
                }
            }

            itemComponent.isInstantiated = true;

            customItem.SetActive(true);
            itemComponent.enabled = true;
            save.enabled = true;
            cull.enabled = true;
        }
    }
}
