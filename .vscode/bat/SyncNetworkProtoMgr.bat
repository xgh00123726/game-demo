@echo off
chcp 65001

set "UNITY_NETWORK_PROTOMGR_PATH=%cd%/Assets/Plugins/Network/NetworkProtoMgr"
set "SERVER_NETWORK_PROTOMGR_PATH=%cd%/Network/NetworkServer/NetworkProtoMgr"

xcopy "%SERVER_NETWORK_PROTOMGR_PATH%\*.*" "%UNITY_NETWORK_PROTOMGR_PATH%\" /s /e /y /f
