local transform;
local gameObject;

UIPlayerLvUpPanel = {};
local this = UIPlayerLvUpPanel;
--启动事件--
function UIPlayerLvUpPanel.Awake(obj)
	UIPlayerLvUpPanel.gameObject = obj;
	transform = obj.transform;
	UIPlayerLvUpPanel.InitPanel();
	log("Awake lua--->>"..UIPlayerLvUpPanel.gameObject.name);
end

--初始化面板--
function UIPlayerLvUpPanel.InitPanel()
	--获取“关闭”按钮
	this.go_CloseBtn = transform:FindChild("S_Frame/S_BtnClose").gameObject; 
	
	--获取“等级”Label
	this.label_Lv = transform:FindChild("S_Frame/S_LvUp/L_PlayerLv"):GetComponent('UILabel'); 
	
	--获取“卡牌等级上限”Label
	this.label_MaxCardLv = transform:FindChild("S_Frame/L_CardMaxLv/L_CardLv"):GetComponent('UILabel'); 
	
	--获取“1号卡HP加成”Label
	this.label_S1_Hp = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_HP/L_S1_HP_Bef"):GetComponent('UILabel'); 
	
	--获取“1号卡HP升级增加值（绿）”Label
	this.label_S1_Hp_Add = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_HP/L_S1_HP_Add"):GetComponent('UILabel'); 
	
	--获取“1号卡Atk加成”Label
	this.label_S1_Atk = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_Atk/L_S1_Atk_Bef"):GetComponent('UILabel'); 
	
	--获取“1号卡HP升级增加值（绿）”Label
	this.label_S1_Atk_Add = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_Atk/L_S1_Atk_Add"):GetComponent('UILabel'); 
	
	--获取“2号卡HP加成”Label
	this.label_S2_Hp = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_HP/L_S2_HP_Bef"):GetComponent('UILabel'); 
		
	--获取“2号卡HP升级增加值（绿）”Label
	this.label_S2_Hp_Add = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_HP/L_S2_HP_Add"):GetComponent('UILabel'); 
		
	--获取“2号卡Atk加成”Label
	this.label_S2_Atk = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_Atk/L_S2_Atk_Bef"):GetComponent('UILabel'); 
		
	--获取“2号卡HP升级增加值（绿）”Label
	this.label_S2_Atk_Add = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_Atk/L_S2_Atk_Add"):GetComponent('UILabel'); 
		
	--获取“3号卡HP加成”Label
	this.label_S3_Hp = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_HP/L_S3_HP_Bef"):GetComponent('UILabel'); 
			
	--获取“3号卡HP升级增加值（绿）”Label
	this.label_S3_Hp_Add = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_HP/L_S3_HP_Add"):GetComponent('UILabel'); 
			
	--获取“3号卡Atk加成”Label
	this.label_S3_Atk = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_Atk/L_S3_Atk_Bef"):GetComponent('UILabel'); 
			
	--获取“3号卡HP升级增加值（绿）”Label
	this.label_S3_Atk_Add = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_Atk/L_S3_Atk_Add"):GetComponent('UILabel'); 
				
	log("UIPlayerLvUpPanel.InitPanel");
end

function UIPlayerLvUpPanel.OnUpdate()
	UIPlayerLvUpCtrl:OnUpdate();
end

--关闭事件--
function UIPlayerLvUpPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

