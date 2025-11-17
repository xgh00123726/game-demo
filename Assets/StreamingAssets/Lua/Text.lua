require("TextData")

local TextMgr = CS.GameBase.Texts.TextMgr
local EquipmentTextMgr = TextMgr.Equipment

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

local function GetSpellActionModifierText( id )
    return ""
end

local function GetModifierText( id )
    return ""
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