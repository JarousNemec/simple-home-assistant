namespace SimpleHomeAssistantServer.Enums;

public enum MqttOperations
{
    ControlPowerState, //Power0 - msg: off/on/toggle
    SetDefaultPowerState, //PowerOnState - msg: off/on/toggle
    GetAllInfos, //Status0
    GetOneOfInfos, //Status - msg: 0-12
    SetTimezone, //Timezone - msg: -13..+13 / -13:00..+13:00
    SetTimer, //https://tasmota.github.io/docs/Timers/#commands
    SetFriendlyHostname, 
    SetDeviceName
    
}