local transform;
local gameObject;

UINamingPanel = {};
local this = UINamingPanel;
--启动事件--
function UINamingPanel.Awake(obj)
	UINamingPanel.gameObject = obj;
	UINamingPanel.transform = obj.transform;
	UINamingPanel.InitPanel();
	log("Awake lua--->>"..UINamingPanel.gameObject.name);
end

--初始化面板--
function UINamingPanel.InitPanel()
	--获取“确定”按钮
	UINamingPanel.NamingBtn = UINamingPanel.transform:FindChild("Sprite_Frame/Sprite_Bg/Sprite_ConfirmBtn").gameObject; 
	
	--获取“提示”游戏物体
	UINamingPanel.go_Hint = UINamingPanel.transform:FindChild("Sprite_Hint_Bg").gameObject; 
	UINamingPanel.go_HintLabel = UINamingPanel.transform:FindChild("Sprite_Hint_Bg/Label_Hint").gameObject;
	
	--获取输入框label
	UINamingPanel.label_Input = UINamingPanel.transform:FindChild("Sprite_Frame/Sprite_Bg/Input Field/Label").gameObject:GetComponent('UILabel');
	--获取input的input
	UINamingPanel.input_Input = UINamingPanel.transform:FindChild("Sprite_Frame/Sprite_Bg/Input Field").gameObject:GetComponent('UIInput');
	--获取关闭按钮
	UINamingPanel.go_CloseBtn = UINamingPanel.transform:FindChild("Sprite_Frame/Sprite_CloseIcon").gameObject;
	
end

function UINamingPanel.OnUpdate()
	UINamingCtrl:OnUpdate();
end

--单击事件--
function UINamingPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

