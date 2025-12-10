local Vector3 = CS.UnityEngine.Vector3

UI.ToolBar.Items = {}
for name, _ in pairs(UIData.ToolBar.Items) do
    UI.ToolBar.Items[name] = {}
end

local function ToolBarExpand()
    local itemDatas = UIData.ToolBar.Items
    local items = UI.ToolBar.Items
    local SetSprite = CS.GameBase.Resources.Utils.SetSprite
    local DOMoveTo = CS.GameBase.Tools.Transforms.Move.DOMoveTo
    local parentItem = UI.ToolBar.Items.Parent
    SetSprite(parentItem.Image, UIData.ToolBar.ExpandTextureName)
    for name, item in pairs(items) do
        if (name ~= "Parent") then
            local targetPosition = itemDatas[name].Position
            item.Item:Show()
            DOMoveTo(item.Item.Obj.transform, Vector3(targetPosition.x, targetPosition.y), UIData.ToolBar.ExpandTime)
        end

    end
    parentItem.IsExpand = true
end

local function ToolBarCollapse()
    local itemDatas = UIData.ToolBar.Items
    local items = UI.ToolBar.Items
    local SetSprite = CS.GameBase.Resources.Utils.SetSprite
    local DOMoveTo = CS.GameBase.Tools.Transforms.Move.DOMoveTo
    local parentItem = UI.ToolBar.Items.Parent
    local targetPosition = itemDatas.Parent.Position
    SetSprite(parentItem.Image, UIData.ToolBar.CollapseTextureName)
    for name, item in pairs(items) do
        if (name ~= "Parent") then
            DOMoveTo(item.Item.Obj.transform, Vector3(targetPosition.x, targetPosition.y), UIData.ToolBar.CollapseTime):Then(function ()
                item.Item:Hide()
            end)
        end
    end
    parentItem.IsExpand = false
end

UI.ToolBar.Items.Parent.OnPointerDown = function ( index )
    local parentItem = UI.ToolBar.Items.Parent
    if (parentItem.IsExpand) then
        ToolBarCollapse()
    else
        ToolBarExpand()
    end

    local todoNode = Todo.Nodes["Parent"]
    if todoNode ~= nil then
        todoNode:DirtyUpdate()
    end
end

UI.ToolBar.Items.BackPack.OnPointerDown = function ( index )
    ToggleInventory()
end