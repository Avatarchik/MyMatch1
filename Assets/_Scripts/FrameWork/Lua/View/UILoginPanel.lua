local transform;
local gameObject;

UILoginPanel = {};
local this = UILoginPanel;
--�����¼�--
function UILoginPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	this.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--��ʼ�����--
function UILoginPanel.InitPanel()
	--��ȡ����¼����ť
	this.LoginBtn=transform:FindChild("Sprite_LoginBtn").gameObject; 
	
	--SDK��ť
	this.go_LoginSdkBtn=transform:FindChild("Sprite_LoginSdkBtn").gameObject; 
	
	--��ȡUIINPUT���
	this.UIInput=transform:FindChild("Simple Input Field").gameObject:GetComponent('UIInput');
end

function UILoginPanel.OnUpdate()
	UILoginCtrl:OnUpdate();
end

--�����¼�--
function UILoginPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

