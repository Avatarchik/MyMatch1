require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/rename_pb"
require "protobuf"
require "TableCtrl/TableBattleCtrl"

Event = require 'events'

UIArenaCtrl = {};
local this = UIArenaCtrl;
local panel;
local UIArenaBH;
local transform;
local gameObject;
local timer = 0;

--构建函数--
function UIArenaCtrl.New()
	logWarn("UIArenaCtrl.New--------->>");
	return this;
end

function UIArenaCtrl.Awake()
	log("UIArenaCtrl.Awake--------->>");
	uiMgr:ShowUI_LUA(MyFrameWork.E_UIType.UIArenaPanel,this.OnCreate,false,false);
end

--启动事件--
function UIArenaCtrl.OnCreate(obj)
	log("UIArenaCtrl.OnCreate--------->>");
	gameObject = obj;
	transform = obj.transform;
	UIArenaBH = transform:GetComponent('MyFrameWork.LuaBehaviour');
	this.InitPanel();	
end

--初始化面板--
function UIArenaCtrl.InitPanel()
	log("UIArenaCtrl.InitPanel--------->>");
	local script = transform:GetComponent('BaseUI');
	script.mUIStyle = MyFrameWork.E_UIStyle.Main;
	script.mUILayertype = MyFrameWork.E_LayerType.MainUI;
	UIArenaBH:AddClick(UIArenaPanel.go_BtnClose,this.Close);
	UIArenaCtrl.Prepare_Now_Arena(); --根据取得的ID，决定显示战场面板的时候首先显示哪个战场，启动tween
	UIArenaCtrl.ReadTable(); --读表决定每个战场的名字，杯数
	UIArenaCtrl.UnderNow();--当前杯数开不了的竞技场图标都黑化
	log('UIArenaCtrl.InitPanel over');
end

function UIArenaCtrl.OnUpdate()
end


--根据取得的ID，决定显示战场面板的时候高亮哪个战场
function UIArenaCtrl.Prepare_Now_Arena()
	gameObject:SetActive(false);--先隐藏，调整好以后再显示
	local id=TableBattleCtrl.GetIndexByTrophy(UIMainModule.ServerHomeData.user.trophy); --获得当前杯数对应的id
	UIArenaPanel.sb_SB.value=(id-1)*0.123;--根据id设定显示位置
	local nowArena=transform:FindChild("SV_Arena/Grid/C_Lv0"..id-1).gameObject;--获取当前对应战场这个子物体
	--启动房子,卡牌的tween，隐藏卡牌
	local nowhouse=nowArena.transform:FindChild("S_ArenaHouse").gameObject;
	nowhouse:GetComponent('TweenScale').enabled=true;
	local nowcards=nowArena.transform:FindChild("C_Cards").gameObject;
	nowcards:SetActive(false);
	nowcards:GetComponent('TweenScale').enabled=true;
	--特效挂在当前战场子物体上
	--UIArenaPanel.go_Eft:SetActive(true);
	UIArenaPanel.go_Eft.transform.parent=nowArena.transform;
	UIArenaPanel.go_Eft.transform.localPosition=Vector3(0,0,0);
	gameObject:SetActive(true);
end

--关闭事件--
function UIArenaCtrl.Close()
	uiMgr:DestroyUI(MyFrameWork.E_UIType.UIArenaPanel);
end

--读表决定每个战场的名字，最低杯数要求
function UIArenaCtrl.ReadTable()
	for i=1, table.getn(TableBattleCtrl.BattleTable.Battle.ID)	do
		UIArenaPanel.lb_FieldList[i].text=TableBattleCtrl.BattleTable.Battle.Name[i];
		UIArenaPanel.lb_MinTrophyList[i].text=TableBattleCtrl.BattleTable.Battle.Unlocked[i];
	end
	
end

--当前杯数开不了的竞技场图标都黑化
function UIArenaCtrl.UnderNow()
	log('.........................'..TableBattleCtrl.GetIndexByTrophy(UIMainModule.ServerHomeData.user.trophy))
	for i=TableBattleCtrl.GetIndexByTrophy(UIMainModule.ServerHomeData.user.trophy)+1, table.getn(TableBattleCtrl.BattleTable.Battle.ID)	do
		--UIArenaPanel.sp_ArenaHouse[i].color=Color.black;
		UIArenaPanel.sp_ArenaHouse[i].color=Color.New(0.3,0.3,0.3,1); --三个0.5就是Color.grey,第四个参数省略就是1
		
	end
	
end
