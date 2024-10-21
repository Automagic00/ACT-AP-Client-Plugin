using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(Enemy), "CalculateClipsToDrop")]
    class MicroplasticDropsPatch
    { 
        [HarmonyPostfix]
        static void clipDropPost(ref int __result)
        {
            float mult = float.Parse(CrabFile.current.GetString("setting_microplasticMod"));
            mult = mult <= 0 ? 1 : mult;
            if (__result > 0 && mult != 1)
            {
                Debug.Log($"Multiplying {__result} by {mult}");
                __result = Mathf.RoundToInt(__result * mult);
            }
        }
    }

    [HarmonyPatch(typeof(SellButtonFlag),"TrySale")]
    class ShopSellPatch
    {
        [HarmonyPrefix]
        static bool SellPre(SellButtonFlag __instance, ref int amount)
        {
            if(__instance.sellItemData is not JunkCollectable)
            {
                return true;
            }
            if(!__instance.sellItemData.name.Contains("Claw"))
            {
                return true;
            }

            if (__instance.sellItemData.GetInventorySlot().amount > 0)
            {
                CrabFile.current.inventoryData.wallet.AddCurrency(InventoryData.CURRENCY.Clips, Mathf.RoundToInt(__instance.cost * amount * float.Parse(CrabFile.current.GetString("setting_microplasticMult"))), true);
            }
            CrabFile.current.inventoryData.AdjustAmount(__instance.sellItemData, -amount);
            if (__instance.cost > 0)
            {
                AudioManager.PlayOneShot("UI/UI_Sell", null, false);
            }
            __instance.shop.OnPurchased(__instance);
            return false;
        }
    }

    /*[HarmonyPatch(typeof(InventoryData.Wallet), nameof(InventoryData.Wallet.AddCurrency))]
    class MicroplasticsPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(InventoryData.CURRENCY c, ref int amt, bool updateCurrencyText = true)
        {
            if (Plugin.connection.session != null)
            {
                int player = Plugin.connection.session.ConnectionInfo.Slot;
                Dictionary<string, object> slotData = Plugin.connection.slotData;
                //object value;

                foreach(var key in Plugin.connection.slotData.Keys)
                {
                    Debug.Log("slotdata: " + key);
                    Debug.Log("value: " + slotData[key]);
                }

                double value = (double)Plugin.connection.slotData["microplastic_multiplier"];
                //slotData.

                //slotData.TryGetValue("Microplastic Multiplier", out value);

                float microplaticMod = 1;

                if (value != null)
                {
                    Debug.Log("Multiplying MicroPlastics by " + value);
                    microplaticMod = (float)value;
                }

                Debug.Log("AddCurr");
                if (c == InventoryData.CURRENCY.Clips && amt > 0)
                {
                    amt = (int)Math.Round(microplaticMod * amt);
                }
            }
            return true;
        }

    }*/
}
