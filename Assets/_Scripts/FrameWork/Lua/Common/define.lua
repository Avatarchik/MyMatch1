
CtrlNames = 
{
	UILoginCtrl = "UILoginCtrl",
	UIMainCtrl = "UIMainCtrl",
	UINamingCtrl = "UINamingCtrl",
	Message = "MessageCtrl",
	UIPlayerLvUpCtrl = "UIPlayerLvUpCtrl",
	UIArenaCtrl = "UIArenaCtrl",
}

PanelNames = {
	"UILoginPanel",
	"UIMainPanel",	
	"UIRankPanel",
	"UINamingPanel",
	"MessagePanel",
	"UIPlayerLvUpPanel",
	"UIArenaPanel",
}

ModuleNames = {
	UILoginModule = "UILoginModule",
	UIMainModule = "UIMainModule",
	FightModule = "FightModule",
	UINamingModule = "UINamingModule",
}

Util = MyFrameWork.Util;
AppConst = MyFrameWork.AppConst;
LuaHelper = MyFrameWork.LuaHelper;
ByteBuffer = MyFrameWork.ByteBuffer;

resMgr = LuaHelper.GetResManager();
uiMgr = LuaHelper.GetPanelManager();
soundMgr = LuaHelper.GetMusicManager();
networkMgr = LuaHelper.GetNetManager();
gameMgr = LuaHelper.GetGameManager();
fightDataMgr = LuaHelper.GetFightDataManager();
loadingMgr = LuaHelper.GetLoadingManager();
utilMgr = LuaHelper.GetLuaUtilityManager();

WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;
