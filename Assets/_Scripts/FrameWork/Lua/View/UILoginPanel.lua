local transform;
local gameObject;

UILoginPanel = {};
local this = UILoginPanel;
--启动事件--
function UILoginPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	this.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function UILoginPanel.InitPanel()
	--获取“登录”按钮
	this.LoginBtn=transform:FindChild("Sprite_LoginBtn").gameObject; 
	
	--SDK按钮
	this.go_LoginSdkBtn=transform:FindChild("Sprite_LoginSdkBtn").gameObject; 
	
	--获取UIINPUT组件
	this.UIInput=transform:FindChild("Simple Input Field").gameObject:GetComponent('UIInput');
end

function UILoginPanel.OnUpdate()
	UILoginCtrl:OnUpdate();
end

--单击事件--
function UILoginPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

