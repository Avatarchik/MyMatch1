local transform;
local gameObject;

UIPlayerLvUpPanel = {};
local this = UIPlayerLvUpPanel;
--�����¼�--
function UIPlayerLvUpPanel.Awake(obj)
	UIPlayerLvUpPanel.gameObject = obj;
	transform = obj.transform;
	UIPlayerLvUpPanel.InitPanel();
	log("Awake lua--->>"..UIPlayerLvUpPanel.gameObject.name);
end

--��ʼ�����--
function UIPlayerLvUpPanel.InitPanel()
	--��ȡ���رա���ť
	this.go_CloseBtn = transform:FindChild("S_Frame/S_BtnClose").gameObject; 
	
	--��ȡ���ȼ���Label
	this.label_Lv = transform:FindChild("S_Frame/S_LvUp/L_PlayerLv"):GetComponent('UILabel'); 
	
	--��ȡ�����Ƶȼ����ޡ�Label
	this.label_MaxCardLv = transform:FindChild("S_Frame/L_CardMaxLv/L_CardLv"):GetComponent('UILabel'); 
	
	--��ȡ��1�ſ�HP�ӳɡ�Label
	this.label_S1_Hp = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_HP/L_S1_HP_Bef"):GetComponent('UILabel'); 
	
	--��ȡ��1�ſ�HP��������ֵ���̣���Label
	this.label_S1_Hp_Add = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_HP/L_S1_HP_Add"):GetComponent('UILabel'); 
	
	--��ȡ��1�ſ�Atk�ӳɡ�Label
	this.label_S1_Atk = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_Atk/L_S1_Atk_Bef"):GetComponent('UILabel'); 
	
	--��ȡ��1�ſ�HP��������ֵ���̣���Label
	this.label_S1_Atk_Add = transform:FindChild("S_Frame/C_CardSeat_1/L_S1_Atk/L_S1_Atk_Add"):GetComponent('UILabel'); 
	
	--��ȡ��2�ſ�HP�ӳɡ�Label
	this.label_S2_Hp = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_HP/L_S2_HP_Bef"):GetComponent('UILabel'); 
		
	--��ȡ��2�ſ�HP��������ֵ���̣���Label
	this.label_S2_Hp_Add = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_HP/L_S2_HP_Add"):GetComponent('UILabel'); 
		
	--��ȡ��2�ſ�Atk�ӳɡ�Label
	this.label_S2_Atk = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_Atk/L_S2_Atk_Bef"):GetComponent('UILabel'); 
		
	--��ȡ��2�ſ�HP��������ֵ���̣���Label
	this.label_S2_Atk_Add = transform:FindChild("S_Frame/C_CardSeat_2/L_S2_Atk/L_S2_Atk_Add"):GetComponent('UILabel'); 
		
	--��ȡ��3�ſ�HP�ӳɡ�Label
	this.label_S3_Hp = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_HP/L_S3_HP_Bef"):GetComponent('UILabel'); 
			
	--��ȡ��3�ſ�HP��������ֵ���̣���Label
	this.label_S3_Hp_Add = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_HP/L_S3_HP_Add"):GetComponent('UILabel'); 
			
	--��ȡ��3�ſ�Atk�ӳɡ�Label
	this.label_S3_Atk = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_Atk/L_S3_Atk_Bef"):GetComponent('UILabel'); 
			
	--��ȡ��3�ſ�HP��������ֵ���̣���Label
	this.label_S3_Atk_Add = transform:FindChild("S_Frame/C_CardSeat_3/L_S3_Atk/L_S3_Atk_Add"):GetComponent('UILabel'); 
				
	log("UIPlayerLvUpPanel.InitPanel");
end

function UIPlayerLvUpPanel.OnUpdate()
	UIPlayerLvUpCtrl:OnUpdate();
end

--�ر��¼�--
function UIPlayerLvUpPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

