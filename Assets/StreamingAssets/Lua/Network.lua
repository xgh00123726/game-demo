local NetworkProtoMgr = CS.GameBase.Network.NetworkProtoMgr

local function Init()
    NetworkProtoMgr.Notify:RegisterRecvEvent(function ( msg )
        print(msg.Info)
    end)
end

Network = {
    Init = Init,
}