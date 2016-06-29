local lpeg = require "lpeg"

require "Common/define"
require "Common/functions"
require "Logic/LuaClass"
require "Logic/CtrlManager"
require "Logic/ModuleManager"
require "Controller/UILoginCtrl"
require "TableCtrl/TableTutorialCtrl"

--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

function Game.InitViewPanels()
	for i = 1, #PanelNames do
		require ("View/"..tostring(PanelNames[i]))
	end
end

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    AppConst.SocketPort = 5555;
    AppConst.SocketAddress =  "116.228.88.149"; --"games.emagroup.cn" ;--116.228.88.149";--"192.168.10.28";--"116.228.88.149"; --
    networkMgr:SendConnect(); --进入游戏就连接服务器
    --注册LuaView--
    this.InitViewPanels();

    CtrlManager.Init();
	ModuleManager.Init();
    local ctrl = CtrlManager.GetCtrl(CtrlNames.UILoginCtrl);
    if ctrl ~= nil then
        ctrl:Awake();
    end

    log('LuaFramework InitOK--->>>');
end

function Game.ShowUIMain()
    local ctrl = CtrlManager.GetCtrl(CtrlNames.UIMainCtrl);
    if ctrl ~= nil then
        ctrl:Awake();
    end
	
	if fightDataMgr.isRename == true then
		fightDataMgr.isRename = false;
		Game:ShowUINaming();
	end
	logWarn('Game.ShowUIMain');
end

--显示改名按钮
function Game.ShowUINaming()	
    local ctrl = CtrlManager.GetCtrl(CtrlNames.UINamingCtrl);
    if ctrl ~= nil then
        ctrl:Awake();
    end
	logWarn('Game.ShowUINaming');
end


--显示玩家升级Panel
function Game.ShowUIPlayerLvUp()
	local ctrl = CtrlManager.GetCtrl(CtrlNames.UIPlayerLvUpCtrl);
    if ctrl ~= nil then
        ctrl:Awake();
	else
		log('............................');
    end
	logWarn('Game.ShowUIPlayerLvUp');
end

--显示竞技场升级
function Game.ShowArena()	
    local ctrl = CtrlManager.GetCtrl(CtrlNames.UIArenaCtrl);
    if ctrl ~= nil then
        ctrl:Awake();
	else
		log('kong..............');
    end
	logWarn('Game.ShowArena');
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end