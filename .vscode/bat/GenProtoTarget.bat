@echo off
chcp 65001

call %cd%/.vscode/bat/UpdateProtoDll.bat
echo --------更新proto依赖的dll完成--------

set "PROTO_PATH=%cd%/Network/ProtoBuf"
set "PROTO_EXE_PATH=%PROTO_PATH%/bin/protoc.exe"
set "PROTO_FILES=%PROTO_PATH%/ProtoFile/ServerMail"

set "SERVER_OUTPUT_DIR=%cd%/Network/NetworkServer/Proto"
set "TEST_CLIENT_OUTPUT_DIR=%cd%/Network/NetworkClient/Proto"
set "CLIENT_OUTPUT_DIR=%cd%/Assets/Plugins/Network/Proto"

for /r "%PROTO_FILES%" %%f in (*.proto) do (
    echo Processing: %%f
    "%PROTO_EXE_PATH%" ^
    --csharp_out="%SERVER_OUTPUT_DIR%" ^
    --csharp_out="%TEST_CLIENT_OUTPUT_DIR%" ^
    --csharp_out="%CLIENT_OUTPUT_DIR%" ^
    --proto_path=%PROTO_PATH% ^
    %%f
)

echo --------proto目标文件生成完成--------

call %cd%/.vscode/bat/SyncNetworkProtoMgr.bat

echo --------用户自定义ProtoMgr同步完成--------