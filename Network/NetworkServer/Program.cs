using NetworkServer;
using NetworkServer.Client;
using NetworkServer.Input;
using NetworkServer.Server;
using System.Net;
using GameBase.Network;
using Google.Protobuf;

var logger = new ConsoleLogger();
var cm = new ClientMgr
{
    Logger = logger,
    IsLogHeartbeat = false
};

var server = new ServerInfo()
{
    IPAddress = IPAddress.Any,
    Port = 8888,
    ServerLogger = logger,
    CM = cm
};

Command.Register("cm", (string[] cmds) =>
{
    if (cmds.Length == 3)
    {
        var cmCmd = cmds[1].ToLower();
        var cmVal = cmds[2];

        if (cmCmd == "boardcast" || cmCmd == "bc")
        {
            var res = cm.Boardcast(cmVal);
            var okTip = res ? " " : " not ";
            logger.Log($"Boardcast message: [{cmVal}]{okTip}OK");
        }
    }
    else if (cmds.Length == 2)
    {
        var cmCmd = cmds[1].ToLower();
        if (cmCmd == "info")
        {
            cm.LogInfo();
        }
        else if (cmCmd == "bct")
        {
            var res = cm.Boardcast(new Notify()
            {
                Info = "hello world"
            }.ToFrame());

            if (res)
            {
                logger.Log("Test text has been boardcast");
            }
            else
            {
                logger.Log("Boardcast fail");
            }
        }
        else if (cmCmd == "toggle-log")
        {
            cm.IsLogHeartbeat = !cm.IsLogHeartbeat;
            if (cm.IsLogHeartbeat)
            {
                logger.Log("Enable heartbeat tip");
            }
            else
            {
                logger.Log("Disable heartbeat tip");
            }
        }
    }
});
Command.Register("exit", () =>
{
    logger.Log("Exiting process...");
    Environment.Exit(0);
});
Command.Register("e", () =>
{
    logger.Log("Exiting process...");
    Environment.Exit(0);
});

new Thread(server.ListenClient).Start();
new Thread(cm.KeepClients).Start();
new Thread(InputMgr.Exec).Start();