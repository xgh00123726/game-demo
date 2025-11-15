using GameBase.Creatures;
using GameBase.GCamera;
using GameBase.Triggers;
using GameBase.Tools;
using GameBase.Modify;
using UnityEngine;
using GameBase.Texts;
using GameBase.AI;
using GameBase.UI;
using LuaUtil;
public class Initer : MonoBehaviour
{
    void Start()
    {
        Command.Register("gen-creature", (int id) =>
        {
        });

        Command.Register("gen-ai-creature", (int id, int aiID) =>
        {
        });

        Command.Register("gen-many-creature", (int num) =>
        {
        });

        Command.Register("remove-all-commonCreature", () =>
        {
        });
    }
}
