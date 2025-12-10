require("TodoData")

local TodoNode = CS.GameBase.UI.TodoNode
local TodoMgr = CS.GameBase.UI.TodoMgr.Instance

local function Init()
    TodoMgr:SetActive(true)
end

local function CreateToolBarNode( nodeName, target, DirtyEvent, offset )
    local view = UI.CreateFree("Prefabs/UI/RedPoint.prefab", UIData.Enum.CanvasType.Root, 5)
    local node = TodoNode(view, target, DirtyEvent, offset)
    TodoMgr:Add(node)
    Todo.Nodes[nodeName] = node
    return node
end

Todo = {
    Init = Init,

    Nodes = {},

    --- @arg1 target : ITodoTarget
    --- @arg2 DirtyEvent : Func<bool>
    --- @arg3 parent : TodoNode
    --- @arg4 offset : Vector3
    CreateToolBarNode = CreateToolBarNode,
}