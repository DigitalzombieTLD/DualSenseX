using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2Cpp;
using DualSenseX.Utils.Filesystem;
using System.Reflection;


namespace DualSenseX
{
	public class DualSenseXMain : MelonMod
	{
        public static bool auroraFlicker = false;
                
        public override void OnInitializeMelon()
        {            
            Settings.OnLoad();
        }

        public override void OnUpdate()
        {
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.options.hotReload) && Settings.options.enabled)
            {
                UDPManager.Disconnect();
                Settings.GetActionModeSettings();
                UDPManager.TryConnect();
            }         
        }
        public override void OnFixedUpdate()
        {
            if (Settings.options.enabled && Settings.options.enableAuroraLED && UDPManager._connectionStatus == UDPManager.ConnectionStatus.Connected)
            {
                if (GameManager.GetAuroraManager() && GameManager.GetAuroraManager().AuroraIsActive())
                {
                    if(Settings.options.enableAuroraLED)
                    {
                        LightFlicker.SwitchStateLED(true);
                    }                 
                   
                    
                    LightFlicker.UpdateFlicker();
                }
                else
                {
                    LightFlicker.SwitchStateLED(false);                    
                }
            }
            else
            {
                LightFlicker.SwitchStateLED(false);               
            }
        }

        public override void OnApplicationQuit()
        {
            UDPManager.Disconnect();
        }
    }
}