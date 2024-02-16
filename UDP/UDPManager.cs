using MelonLoader;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using Il2CppSystem.Text;
using System.Net;

namespace DualSenseX
{
    public static class UDPManager
    {
        public enum ConnectionStatus { Disconnected, Connected };
        public static ConnectionStatus _connectionStatus = ConnectionStatus.Disconnected;
        private static Settings.ActionMode _currentActionMode = Settings.ActionMode.None;

        private static UdpClient _client;
        private static IPEndPoint _endPointSend;

        private static DateTime _timeSent;        
        
        public static void SetMode(Settings.ActionMode actionMode)
        {
            if (_connectionStatus == ConnectionStatus.Connected && Settings.options.enabled)
            {               
                if(Settings.actionSettings.ContainsKey(actionMode) && _currentActionMode != actionMode)
                {
                    if (Settings.options.enableDebug)
                    {
                        MelonLogger.Msg("Switch mode to " + actionMode);
                    }

                    _currentActionMode = actionMode;

                    SetTriggerUpdate(Trigger.Left, actionMode);
                    SetTriggerUpdate(Trigger.Right, actionMode);

                    SetTriggeThreshold(Trigger.Left, actionMode);
                    SetTriggeThreshold(Trigger.Right, actionMode);
                }
            }
        }
       
        public static void TryConnect()
        {
            if (_connectionStatus == ConnectionStatus.Connected)
            {
                return;
            }          

            _client = new UdpClient();
            _endPointSend = new IPEndPoint(Triggers.localhost, Settings.udpPort);

            if (_endPointSend != null)
            {
                 MelonLogger.Msg("Connected successfully on port: " + Settings.udpPort);
                _connectionStatus = ConnectionStatus.Connected;
                _currentActionMode = Settings.ActionMode.None;
                SetMode(Settings.ActionMode.Default);
                UDPManager.SetRGB(new Color(1,1,1));
            }
            else
            {
                MelonLogger.Msg("Connection to port " + Settings.udpPort + " failed!");
                _connectionStatus = ConnectionStatus.Disconnected;
            }
        }

        public static void Disconnect()
        {
            if (_connectionStatus == ConnectionStatus.Connected)
            {
                UDPManager.SetMode(Settings.ActionMode.Default);
                UDPManager.SetRGB(new Color(1,1,1));
                MelonLogger.Msg("Connection closed");
                _client.Close();
                _connectionStatus = ConnectionStatus.Disconnected;                
            }
        }

        private static void SetTriggerUpdate(Trigger trigger, Settings.ActionMode actionMode)
        {
            if(trigger == Trigger.Left)
            {
                SendInstruction(InstructionType.TriggerUpdate, Settings.actionSettings[actionMode].LeftTriggerPacket);
            }
            else if (trigger == Trigger.Right)
            {
                SendInstruction(InstructionType.TriggerUpdate, Settings.actionSettings[actionMode].RightTriggerPacket);
            }
        }

        public static void VibrateTrigger(Trigger trigger, int vibrateValue)
        {
            if (Settings.options.enabled && _connectionStatus == ConnectionStatus.Connected)
            {
                SendInstruction(InstructionType.TriggerUpdate, new object[] { Settings.options.controllerIndex, trigger, TriggerMode.VibrateTrigger, vibrateValue });
            }
        }

        public static void VibrateTriggerPulse(Trigger trigger)
        {
            if (Settings.options.enabled && _connectionStatus == ConnectionStatus.Connected)
            {
                SendInstruction(InstructionType.TriggerUpdate, new object[] { Settings.options.controllerIndex, trigger, TriggerMode.VibrateTriggerPulse });
            }
        }

        private static void SetTriggeThreshold(Trigger trigger, Settings.ActionMode actionMode)
        {
            if (trigger == Trigger.Left)
            {
                SendInstruction(InstructionType.TriggerThreshold, new object[] { Settings.options.controllerIndex, trigger, Settings.actionSettings[actionMode].LeftTriggerThreshold });
            }
            else if (trigger == Trigger.Right)
            {
                SendInstruction(InstructionType.TriggerThreshold, new object[] { Settings.options.controllerIndex, trigger, Settings.actionSettings[actionMode].RightTriggerThreshold });
            }            
        }

        public static void SetRGB(Color32 newColor)
        {
            if (Settings.options.enabled && _connectionStatus == ConnectionStatus.Connected)
            {
                SendInstruction(InstructionType.RGBUpdate, new object[] { Settings.options.controllerIndex, newColor.r, newColor.g, newColor.b });
            }               
        }

        public static void SetRGB(Color newColor)
        {
            if (Settings.options.enabled && _connectionStatus == ConnectionStatus.Connected)
            {
                Color32 newColor32 = newColor;
                SendInstruction(InstructionType.RGBUpdate, new object[] { Settings.options.controllerIndex, newColor32.r, newColor32.g, newColor32.b });
            }            
        }
        private static void SendInstruction(InstructionType instructionType, object[] instructions)
        {
            if(_connectionStatus == ConnectionStatus.Disconnected) 
            {
                return;
            }

            Packet p = new Packet();
            p.instructions = new Instruction[1];  
            p.instructions[0].type = instructionType;
            p.instructions[0].parameters = instructions;

            SendPacket(p);
        }

        private static void SendPacket(Packet data)
        {
            if (_connectionStatus == ConnectionStatus.Connected)
            {
                var RequestData = Encoding.ASCII.GetBytes(Triggers.PacketToJson(data));
                _client.Send(RequestData, RequestData.Length, _endPointSend);
                _timeSent = DateTime.Now;
            }
        }       
    }
}