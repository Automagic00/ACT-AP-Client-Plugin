using HarmonyLib;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace ACTAP
{
    //Button Visuals
    [HarmonyPatch(typeof(SkillTreeButtonFlag), "UpdateDisplay")]
    public class SkillTree
    {
        [HarmonyPostfix]
        public static void DisplayPatch(SkillTreeButtonFlag __instance)
        {
            //Debug.Log( String.Format("Name: {0}, ParentParent Name: {1}", __instance.name, __instance.transform.parent.parent.parent.parent.name));
            bool purchased = false;
            if (__instance.transform.parent.parent.parent.parent.name == "Window" || __instance.transform.parent.parent.parent.parent.name == "Pause Canvas")
            {
                
            }
            else
            {
                
                for (int i = 0; i < __instance.purchaseImages.Length; i++)
                {
                    Debug.Log("Skilltreedataname Disp: " + __instance.selectedData[0].name);
                     purchased = CrabFile.current.GetBool("APSkillPurchase_" + __instance.selectedData[0].name);
                    __instance.purchaseImages[i].color = (purchased ? __instance.purchasedColor : __instance.unpurchasedColor);
                }


                bool preReqPurchased = __instance.selectedData[0].name == "Skill_Shelleport" ? true : CrabFile.current.GetBool("APSkillPurchase_" + __instance.preReq.selectedData[0].name);

                __instance.activeObject.SetActive(preReqPurchased);
                __instance.inactiveObject.SetActive(!preReqPurchased);

                __instance.costObject.SetActive(!purchased);
                __instance.icon.sprite = ItemSwapData.getAPSprite();
            }
        }
    }

    
    [HarmonyPatch(typeof(SkillTreeButtonFlag), "UpdateButton")]
    public class ButtonUpdate
    {
        [HarmonyPostfix]
        public static void UpdateButtonPatch(SkillTreeButtonFlag __instance)
        {
            Debug.Log(__instance.label.text);
        }
    }

    //Display Side Panel
    [HarmonyPatch(typeof(SkillTreeButtonFlag), "Select")]
    public class Select
    {
        //public static PropertyInfo prev = AccessTools.Property(typeof(SkillTreeButtonFlag), "meetsPreviewPrerequisites");
        
        public static PropertyInfo purchasable = AccessTools.Property(typeof(SkillTreeButtonFlag), "meetsPurchasablePrereqs");

        [HarmonyPrefix]
        public static bool SelectPatch(SkillTreeButtonFlag __instance)
        {
            bool prev = __instance.selectedData[0].name == "Skill_Shelleport" ? true : CrabFile.current.GetBool("APSkillPurchase_" + __instance.preReq.selectedData[0].name);
            if (Plugin.debugMode == false && Plugin.connection == null)
            {
                return true;
            }

            if (__instance.transform.parent.parent.parent.parent.name == "Window" || __instance.transform.parent.parent.parent.parent.name == "Pause Canvas")
            {
                return true;
            }

            SkillTreeData replace = (SkillTreeData)ScriptableObject.CreateInstance(typeof(SkillTreeData));
            replace.cost = __instance.selectedData[0].cost;
            //replace.name = __instance.selectedData[0].name;
            replace.skill = __instance.selectedData[0].skill;
            replace.afterUnlockDialogue = __instance.selectedData[0].afterUnlockDialogue;

            if (Plugin.debugMode)
            {
                replace.displayTitle = "AP Test Description";
                replace.buttonLabel = "Archipelago Item";
            }

            else if (Plugin.connection != null)
            {
                string apItemName = CrabFile.current.GetString(Plugin.connection.GetLocationName(LocationDataTable.FindSkillAPID(__instance.selectedData[0].name)) + "ItemName");
                string apPlayerName = CrabFile.current.GetString(Plugin.connection.GetLocationName(LocationDataTable.FindSkillAPID(__instance.selectedData[0].name)) + "PlayerName");
                replace.displayTitle = apItemName;
                replace.buttonLabel = "AP Item for " + apPlayerName +"'s world";
            }
            
            replace.displayImage = ItemSwapData.getAPSprite();

            bool purchased = false;

            __instance.skillTreePanel.SetThumbnail(prev? replace : __instance.lockedData, (bool)purchasable.GetValue(__instance));
            __instance.skillTreePanel.thumbnail.icon.color = (purchased? __instance.purchasedColor : __instance.unpurchasedColor);
            return false;
        }
    }

    //Side Panel Buy Button
    [HarmonyPatch(typeof(SkillTreeWindow),"SetThumbnail")]
    public class Thumbnail
    {
        [HarmonyPrefix]
        public static bool ThumbnailPatch(SkillTreeWindow __instance, ref SkillTreeData data, bool purchasePrereqsMet)
        {
            if (Plugin.debugMode == false && Plugin.connection == null)
            {
                return true;
            }
            bool purchased = false;

            if (data)
            {
                __instance.cost.amountText.text = data.cost.ToString() ?? "";
                if (__instance.isStandalone)
                {
                    Debug.Log("Skilltreedataname Thumb: " + data.skill);
                    purchased = CrabFile.current.GetBool("APSkillPurchase_Skill_" + data.skill);
                    __instance.unlockButton.gameObject.SetActive(!purchased);
                    __instance.prompt.gameObject.SetActive(CrabFile.current.inventoryData.wallet.GetCurrencyAmount(InventoryData.CURRENCY.UmamiCrystals) >= data.cost);
                    __instance.unlockButtonFill.color = ((CrabFile.current.inventoryData.wallet.GetCurrencyAmount(InventoryData.CURRENCY.UmamiCrystals) >= data.cost) ? __instance.unlockButtonUnlockedColor : __instance.unlockButtonLockedColor);
                }
                else
                {
                    __instance.unlockButton.gameObject.SetActive(false);
                }
            }
            else
            {
                __instance.unlockButton.gameObject.SetActive(false);
            }
            __instance.thumbnail.Set(data);
            return false;

        }
    }

    [HarmonyPatch(typeof(SkillTreeButtonFlag), "OnClick")]
    public class Click
    {

        public static FieldInfo dialogue = AccessTools.Field(typeof(SkillTreeButtonFlag), "skillUnlockDialogue");
        public static FieldInfo tut = AccessTools.Field(typeof(SkillTreeButtonFlag), "skillTutorialData");
        //public static MethodInfo PostUp

        [HarmonyPrefix]
        public static bool OnClickPatch(SkillTreeButtonFlag __instance)
        {
            bool purchased = CrabFile.current.GetBool("APSkillPurchase_" + __instance.selectedData[0].name);
            bool preReqPurchased = __instance.selectedData[0].name == "Skill_Shelleport" ? true : CrabFile.current.GetBool("APSkillPurchase_" + __instance.preReq.selectedData[0].name);
            if (Plugin.debugMode == false && Plugin.connection == null)
            {
                return true;
            }
            if (GameManager.instance.IsGodMode())
            {
                __instance.skillTreePanel.isDebug = GameManager.instance.IsGodMode();
            }
            if (!__instance.skillTreePanel.isStandalone && !__instance.skillTreePanel.isDebug)
            {
                return false;
            }
            if (__instance.skillTreePanel.preventUnlockingSkills)
            {
                return false;
            }
            bool flag = true;
            if (purchased)
            {
                if (__instance.skillTreePanel.isDebug)
                {
                    CrabFile.current.unlocks[__instance.selectedData[0].skill].unlocked = false;
                    __instance.skillTreePanel.UpdateAllButtonDisplays(true);
                }
                return false;
            }
            if (__instance.preReq && !preReqPurchased)
            {
                AudioManager.PlayOneShot("UI/UI_Skill_Locked", null, false);
                return false;
            }
            if (SkillTreeButtonFlag.runningUnlockDialogue)
            {
                return false;
            }
            if (CrabFile.current.inventoryData.wallet.GetCurrencyAmount(InventoryData.CURRENCY.UmamiCrystals) < __instance.selectedData[0].cost && !__instance.skillTreePanel.isDebug)
            {
                AudioManager.PlayOneShot("UI/UI_Skill_Locked", null, false);
                return false;
            }
            if (!__instance.skillTreePanel.isDebug)
            {
                CrabFile.current.inventoryData.wallet.AddCurrency(InventoryData.CURRENCY.UmamiCrystals, -__instance.selectedData[0].cost, true);
            }
            AudioManager.PlayOneShot("UI/UI_Level_Up", null, false);
            //__instance.skillUnlockDialogue = "";
            //__instance.skillTutorialData = null;
            /*foreach (SkillTreeData skillTreeData in __instance.selectedData)
            {
                if (string.IsNullOrEmpty(__instance.skillUnlockDialogue))
                {
                    __instance.skillUnlockDialogue = skillTreeData.afterUnlockDialogue;
                }
                if (__instance.skillTutorialData == null)
                {
                    __instance.skillTutorialData = skillTreeData.tutorial;
                }
                skillTreeData.ClickAction(flag, __instance.skillTreePanel.isDebug);
            }*/
            CrabFile.current.SetBool("APSkillPurchase_" + __instance.selectedData[0].name, true);

            if (Plugin.GetConnection() != null && !Plugin.debugMode)
            {
                Plugin.GetConnection().ActivateCheck(LocationDataTable.pickupTable.Find(x => x.skillName.Contains(__instance.selectedData[0].name)).apid);
            }


            __instance.button.animator.SetTrigger("EquipSucceed");
            __instance.skillTreePanel.counter.Refresh();
            GUIManager.instance.HUD.RefreshHUD();
            __instance.skillTreePanel.UpdateAllButtonDisplays(true);
            __instance.Select();
            __instance.PlayPushSound();

            

            /*if (__instance.onClick != null)

            {
                __instance.onClick.Invoke();
            }*/

            /*if (!string.IsNullOrEmpty(__instance.skillUnlockDialogue) || __instance.skillTutorialData != null)
            {
                GameManager.instance.StartCoroutine(__instance.PostUpgradeDialogueRoutine());
            }*/
            return false;
        }
    }
}

