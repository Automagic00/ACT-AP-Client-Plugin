using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(InventoryData.Wallet), nameof(InventoryData.Wallet.AddCurrency) /*new Type[] {typeof(InventoryData.CURRENCY),typeof(int)}*/ )]
    class MicroplasticsPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(InventoryData.CURRENCY c, ref int amt, bool updateCurrencyText = true)
        {
            if (Plugin.connection.session != null)
            {
                int player = Plugin.connection.session.ConnectionInfo.Slot;
                Dictionary<string, object> slotData = Plugin.connection.slotData;
                object value;

                Debug.Log("slotdata" + Plugin.connection.slotData.Keys);

                slotData.TryGetValue("Microplastic Multiplier", out value);

                float microplaticMod = 1;

                if (value != null)
                {
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

    }
}
