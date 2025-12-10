using GameBase.Network;
using GameBase.Tools;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class CommandInstances
{
    public static void Register()
    {
        Command.Register("sh-connect", () =>
        {
        });

        Command.Register("sh-disconnect", () =>
        {
            ServerHelper.DisConnect();
        });

        Command.Register("sh-stoplisten", () =>
        {
            ServerHelper.StopListen();
        });

        Command.Register("running-backgrond", (string cmd) =>
        {
            var cmdl = cmd.ToLower();
            if (cmdl == "true")
            {
                Application.runInBackground = true;
                XLogger.Instance.Log("已允许unity后台运行");
            }
            else if (cmdl == "false")
            {
                Application.runInBackground = false;
                XLogger.Instance.Log("已禁止unity后台运行");
            }
        });
    }
}
