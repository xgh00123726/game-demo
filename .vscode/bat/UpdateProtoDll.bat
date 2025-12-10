@echo off
chcp 65001

set "NUGET_PACKAGES_PATH=C:\Users\hengg\.nuget\packages"
set "UNITY_PLUGINS_PATH=%cd%/Assets/Plugins/Dll/Protobuf"

copy "%NUGET_PACKAGES_PATH%\google.protobuf\3.33.2\lib\netstandard2.0\Google.Protobuf.dll" "%UNITY_PLUGINS_PATH%"
copy "%NUGET_PACKAGES_PATH%\system.runtime.compilerservices.unsafe\4.5.3\lib\netstandard2.0\System.Runtime.CompilerServices.Unsafe.dll" "%UNITY_PLUGINS_PATH%"
copy "%NUGET_PACKAGES_PATH%\system.memory\4.5.3\lib\netstandard2.0\System.Memory.dll" "%UNITY_PLUGINS_PATH%"
copy "%NUGET_PACKAGES_PATH%\system.buffers\4.4.0\lib\netstandard2.0\System.Buffers.dll" "%UNITY_PLUGINS_PATH%"
copy "%NUGET_PACKAGES_PATH%\system.numerics.vectors\4.4.0\lib\netstandard2.0\System.Numerics.Vectors.dll" "%UNITY_PLUGINS_PATH%"