@echo off
set "PROTO_PATH=%cd%/Network/ProtoBuf"
set "PROTO_EXE_PATH=%PROTO_PATH%/bin/protoc.exe"
set "PROTO_FILES=%PROTO_PATH%/ServerMail"

set "SERVER_OUTPUT_DIR=%cd%/Network/NetworkServer/Proto"
set "TEST_CLIENT_OUTPUT_DIR=%cd%/Network/NetworkClient/Proto"

for /r "%PROTO_FILES%" %%f in (*.proto) do (
    echo Processing: %%f
    "%PROTO_EXE_PATH%" ^
    --csharp_out="%SERVER_OUTPUT_DIR%" ^
    --csharp_out="%TEST_CLIENT_OUTPUT_DIR%" ^
    --proto_path=%PROTO_PATH% ^
    %%f
)

echo All proto files processed!