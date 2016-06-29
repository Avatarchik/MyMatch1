require "Common/define"
require "Controller/UILoginCtrl"
require "Controller/UIMainCtrl"
require "Controller/MessageCtrl"
require "Controller/UIRankCtrl"
require "Controller/UINamingCtrl"
require "Controller/UIPlayerLvUpCtrl"
require "Controller/UIArenaCtrl"
require "View/UINamingPanel"
require "View/UIPlayerLvUpPanel"
require "View/UIArenaPanel"

CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

function CtrlManager.Init()
	logWarn("CtrlManager.Init----->>>");
	ctrlList[CtrlNames.UILoginCtrl] = UILoginCtrl.New();
	ctrlList[CtrlNames.UIMainCtrl] = UIMainCtrl.New();
	ctrlList[CtrlNames.UINamingCtrl] = UINamingCtrl.New();
	ctrlList[CtrlNames.UIPlayerLvUpCtrl] = UIPlayerLvUpCtrl.New();
	ctrlList[CtrlNames.UIArenaCtrl] = UIArenaCtrl.New();
	return this;
end

--添加控制器--
function CtrlManager.AddCtrl(ctrlName, ctrlObj)
	ctrlList[ctrlName] = ctrlObj;
	logWarn('AddCtrl:' + ctrlName);
end

--获取控制器--
function CtrlManager.GetCtrl(ctrlName)
	return ctrlList[ctrlName];
end

--移除控制器--
function CtrlManager.RemoveCtrl(ctrlName)
	ctrlList[ctrlName] = nil;
end

--关闭控制器--
function CtrlManager.Close()
	logWarn('CtrlManager.Close---->>>');
end