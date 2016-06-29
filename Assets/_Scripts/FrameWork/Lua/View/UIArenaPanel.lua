local transform;
local gameObject;

UIArenaPanel = {};
local this = UIArenaPanel;
this.lb_FieldList = {};
this.lb_MinTrophyList = {};
this.sp_ArenaHouse={};

--启动事件--
function UIArenaPanel.Awake(obj)
	log("UIArenaPanel.Awake--------->>");
	UIArenaPanel.gameObject = obj;
	transform = obj.transform;
	UIArenaPanel.InitPanel();
	log("Awake lua--->>"..UIArenaPanel.gameObject.name);
end

--初始化面板--
function UIArenaPanel.InitPanel()
	log("UIArenaPanel.InitPanel--------->>");
	UIArenaPanel.sb_SB=transform:FindChild("SV_Arena/VSB"):GetComponent('UIScrollBar');	--scrollbar
	UIArenaPanel.go_Eft=transform:FindChild("C_Eft").gameObject;	--特效
	UIArenaPanel.go_BtnClose=transform:FindChild("S_BottomFrame/S_BtnClose").gameObject; --关闭
	UIArenaPanel.GetFieldNames_MinimumTrophies();
	
	log("UIArenaPanel.InitPanel");
end

function UIArenaPanel.OnUpdate()
	UIArenaCtrl:OnUpdate();
end

--关闭事件--
function UIArenaPanel.OnDestroy()
	logWarn("UIArenaPanel OnDestroy---->>>");
end

--for循环获取竞技场名字,最低入场杯数,房子
function UIArenaPanel.GetFieldNames_MinimumTrophies()
	for i=1, table.getn(TableBattleCtrl.BattleTable.Battle.ID)	do
		this.lb_FieldList[i]=transform:FindChild("SV_Arena/Grid/C_Lv0"..(i-1).."/S_Title/L_Title"):GetComponent('UILabel'); 
		this.lb_MinTrophyList[i]=transform:FindChild("SV_Arena/Grid/C_Lv0"..(i-1).."/S_Title/L_Trophy"):GetComponent('UILabel'); 
		this.sp_ArenaHouse[i]=transform:FindChild("SV_Arena/Grid/C_Lv0"..(i-1).."/S_ArenaHouse"):GetComponent('UISprite'); 
	end
	log("UIArenaPanel.GetFieldNames_MinimumTrophies");
end

