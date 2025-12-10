local Vector3 = CS.UnityEngine.Vector3

Todo.DirtyEvents = {}

Todo.DirtyEvents.Parent = function ()
    return true
end

Todo.ToolBarInit = function ()
    for name, item in pairs(UI.ToolBar.Items) do
        Todo.CreateToolBarNode(name, item.Item, Todo.DirtyEvents[name], Vector3(23, 20))
    end
    Todo.Nodes.Parent:DirtyUpdate()
end