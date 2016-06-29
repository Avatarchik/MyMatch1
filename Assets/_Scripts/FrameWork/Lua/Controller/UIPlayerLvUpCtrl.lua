require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/rename_pb"
require "protobuf"
require "Controller/UIMainCtrl"

Event = require 'events'

UIPlayerLvUpCtrl = {};
local this = UIPlayerLvUpCtrl;

local panel;
local UIPlayerLvUpBH;
local transform;
local gameObject;


--构建函数--
function UIPlayerLvUpCtrl.New()
	logWarn("UIPlayerLvUpCtrl.New--->>");
	return this;
end

function UIPlayerLvUpCtrl.Awake()
	log("UIPlayerLvUpCtrl.Awake--->>");
	uiMgr:ShowUI_LUA(MyFrameWork.E_UIType.UIPlayerLvUpPanel,this.OnCreate,false,false);
end

--启动事件--
function UIPlayerLvUpCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;
	UIPlayerLvUpBH = transform:GetComponent('MyFrameWork.LuaBehaviour');
	this.InitPanel();	
	--loadingMgr:HideLoading();
	log("UIPlayerLvUpCtrl.OnCreate");
end

--初始化面板--
function UIPlayerLvUpCtrl.InitPanel()
	local script = transform:GetComponent('BaseUI');
	script.mUIStyle = MyFrameWork.E_UIStyle.Main;
	script.mUILayertype = MyFrameWork.E_LayerType.MainUI;
	UIPlayerLvUpBH:AddClick(UIPlayerLvUpPanel.go_CloseBtn,this.OnCloseClick);
	--面板显示内容：读取服务器给的等级，查阅表格给值
	UIPlayerLvUpPanel.label_Lv.text=UIMainModule.ServerHomeData.user.lv;
	UIPlayerLvUpPanel.label_MaxCardLv.text=UIMainModule.ServerHomeData.user.lv;
	UIPlayerLvUpPanel.label_S1_Hp.text=TablePlayerAttrCtrl.GetCardSeatAddition(1,"hp",true);
	UIPlayerLvUpPanel.label_S1_Hp_Add.text=TablePlayerAttrCtrl.GetCardSeatAddition(1,"hp")-TablePlayerAttrCtrl.GetCardSeatAddition(1,"hp",true);
	UIPlayerLvUpPanel.label_S1_Atk.text=TablePlayerAttrCtrl.GetCardSeatAddition(1,"atk",true);
	UIPlayerLvUpPanel.label_S1_Atk_Add.text=TablePlayerAttrCtrl.GetCardSeatAddition(1,"atk")-TablePlayerAttrCtrl.GetCardSeatAddition(1,"atk",true);
	UIPlayerLvUpPanel.label_S2_Hp.text=TablePlayerAttrCtrl.GetCardSeatAddition(2,"hp",true);
	UIPlayerLvUpPanel.label_S2_Hp_Add.text=TablePlayerAttrCtrl.GetCardSeatAddition(2,"hp")-TablePlayerAttrCtrl.GetCardSeatAddition(2,"hp",true);
	UIPlayerLvUpPanel.label_S2_Atk.text=TablePlayerAttrCtrl.GetCardSeatAddition(2,"atk",true);
	UIPlayerLvUpPanel.label_S2_Atk_Add.text=TablePlayerAttrCtrl.GetCardSeatAddition(2,"atk")-TablePlayerAttrCtrl.GetCardSeatAddition(2,"atk",true);
	UIPlayerLvUpPanel.label_S3_Hp.text=TablePlayerAttrCtrl.GetCardSeatAddition(3,"hp",true);
	UIPlayerLvUpPanel.label_S3_Hp_Add.text=TablePlayerAttrCtrl.GetCardSeatAddition(3,"hp")-TablePlayerAttrCtrl.GetCardSeatAddition(3,"hp",true);
	UIPlayerLvUpPanel.label_S3_Atk.text=TablePlayerAttrCtrl.GetCardSeatAddition(3,"atk",true);
	UIPlayerLvUpPanel.label_S3_Atk_Add.text=TablePlayerAttrCtrl.GetCardSeatAddition(3,"atk")-TablePlayerAttrCtrl.GetCardSeatAddition(3,"atk",true);
	
	log("UIPlayerLvUpCtrl.InitPanel over");
end

function UIPlayerLvUpCtrl.OnUpdate()
	
end

--关闭事件--
function UIPlayerLvUpCtrl.OnCloseClick(go)
	log("closing   UIPlayerLvUpCtrl.OnCloseClick");
	if	UIMainModule.ArenaUpLater==true	then	--如果竞技场升级和玩家升级同时发生，先玩家升级，再竞技场升级
		UIMainModule.ArenaUpLater=false;
		Game.ShowArena();
		log("showing arena by closing playerlvup................")
	end
	uiMgr:DestroyUI(MyFrameWork.E_UIType.UIPlayerLvUpPanel);
	--UIPlayerLvUpPanel.gameObject:SetActive(false);
	log("UIPlayerLvUpCtrl.OnCloseClick");
end


