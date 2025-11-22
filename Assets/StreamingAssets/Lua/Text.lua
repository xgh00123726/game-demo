require("TextData")

local TextMgr = CS.GameBase.Texts.TextMgr
local EquipmentTextMgr = TextMgr.Equipment
local SpellActionTextMgr = TextMgr.SpellAction
local ModifierTextMgr = TextMgr.Modifier

local function Init()
    TextMgr.Lang = "zh-cn"

    local cmd = CS.GameBase.Tools.Command()
    cmd:RegisterStringActionArg("switch-lang", function ( lang )
        TextMgr.Lang = lang
        print("language has been switched to "..lang)
    end)
end

local function GetBuffText( id )
    return ""
end

local function GetSpellActionModifierText( name )
    return SpellActionTextMgr:GetData(name)
end

local function GetModifierText( name )
    return ModifierTextMgr:GetData(name)
end

local function GetEquipmentText( name )
    return EquipmentTextMgr:GetData(name)
end

Text = {
    --- @noarg
    Init = Init,

    --- @arg1 id : int
    GetBuffText = GetBuffText,

    --- @arg1 name : string
    GetSpellActionModifierText = GetSpellActionModifierText,

    --- @arg1 name : string
    GetModifierText = GetModifierText,

    --- @arg1 name : string
    GetEquipmentText = GetEquipmentText,
}