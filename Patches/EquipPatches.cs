using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2Cpp;
using System.Reflection;
using System.ComponentModel.Design;

namespace DualSenseX
{
    /*
    [HarmonyLib.HarmonyPatch(typeof(PlayerManager), "EquipItem")]
    public class EquipPatch
    {
        public static void Postfix(ref PlayerManager __instance, ref GearItem gi, ref bool fromDeserialize)
        {
            if (Settings.options.enabled)
            {
                if (gi != null)
                {
                    if (gi.name.Contains("Bow"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Bow);
                    }
                    else if (gi.name.Contains("KeroseneLamp"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Lantern);
                    }
                    else if (gi.name.Contains("Revolver"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Revolver);
                    }
                    else if (gi.name.Contains("Rifle"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Rifle);
                    }
                    else if (gi.name.Contains("Stone"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Stone);
                    }
                    else if (gi.name.Contains("Torch"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Torch);
                    }
                    else if (gi.name.Contains("Match"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Matches);
                    }
                    else if (gi.name.Contains("FlareGun"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.FlareGun);
                    }
                    else if (gi.name.Contains("Flare"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Flare);
                    }                   
                    else if (gi.name.Contains("Flashlight"))
                    {
                        UDPManager.SetMode(Settings.ActionMode.Flashlight);
                    }                   
                }
            }
        }
    }*/

    [HarmonyLib.HarmonyPatch(typeof(PlayerAnimation), "FirstPersonHandsEnabled", new Type[] { typeof(GearItem) })]
    public class EquipPatch323
    {
        public static void Postfix(ref PlayerAnimation __instance, ref GearItem gearItem)
        {
           if (gearItem != null)
           {
                if (gearItem.name.Contains("Bow"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Bow);
                }
                else if (gearItem.name.Contains("KeroseneLamp"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Lantern);
                }
                else if (gearItem.name.Contains("Revolver"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Revolver);
                }
                else if (gearItem.name.Contains("Rifle"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Rifle);
                }
                else if (gearItem.name.Contains("Stone"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Stone);
                }
                else if (gearItem.name.Contains("Torch"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Torch);
                }
                else if (gearItem.name.Contains("Match"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Matches);
                }
                else if (gearItem.name.Contains("FlareGun"))
                {
                    UDPManager.SetMode(Settings.ActionMode.FlareGun);
                }
                else if (gearItem.name.Contains("Flare"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Flare);
                }
                else if (gearItem.name.Contains("Flashlight"))
                {
                    UDPManager.SetMode(Settings.ActionMode.Flashlight);
                }               
            }
           else
           {
                UDPManager.SetMode(Settings.ActionMode.Default);
           }
        }
    }
}
        