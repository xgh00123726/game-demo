require("TextData")

local TextMgr = CS.GameBase.Texts.TextMgr
local EquipmentTextMgr = TextMgr.Equipment

local function Init()
    TextMgr.InitFile(TextData.BuffTextFilePath)
    TextMgr.InitFile(TextData.SpellActionModifierTextPath)
    TextMgr.InitFile(TextData.ModifierTextFilePath)

    EquipmentTextMgr:LoadData("EquipmentText.yaml", "zh-cn")
end

local function GetBuffText( id )
    return TextMgr.Get(TextData.BuffTextFilePath, id)
end

local function GetSpellActionModifierText( id )
    return TextMgr.Get(TextData.SpellActionModifierTextPath, id)
end

local function GetModifierText( id )
    return TextMgr.Get(TextData.ModifierTextFilePath, id)
end

local function GetEquipmentText( name )
    return EquipmentTextMgr:GetData(name)
end

Text = {
    --- @noarg
    Init = Init,

    --- @arg1 id : int
    GetBuffText = GetBuffText,

    --- @arg1 id : int
    GetSpellActionModifierText = GetSpellActionModifierText,

    --- @arg1 id : int
    GetModifierText = GetModifierText,

    --- @arg1 name : string
    GetEquipmentText = GetEquipmentText,
}