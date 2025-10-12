using GameBase.Modify;

namespace GameBase.Buffs
{
    public class BuffFactory
    {
        /// <summary>
        /// ¥”info idªÒ»°buff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Buff Get(int id)
        {
            var info = BuffDataBase.Instance[id];
            var buff = BuffSys.Instance.NewEntity();
            if (info.type == BuffType.Common)
            {
                buff.isInfiDuration = false;
                buff.durationSet = info.duration;
                buff.durationRemain = info.duration;
            }
            else if (info.type == BuffType.Equipment)
            {
                buff.isInfiDuration = true;
            }

            buff.id = id;
            return buff;
        }
    }
}
