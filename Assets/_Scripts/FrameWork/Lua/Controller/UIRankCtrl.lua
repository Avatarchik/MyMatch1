require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/rank_pb"
require "protobuf"
require "TableCtrl/TableBattleCtrl"
require "TableCtrl/TableSkillCtrl"

Event = require 'events'

UIRankCtrl = {};
local this = UIRankCtrl;
--无限卷轴list
UIRankCtrl._lastIndex = 0;
UIRankCtrl._maxIndex = 0;
UIRankCtrl._lastPanelClipY = 0;
UIRankCtrl._panelClipY = 0;
UIRankCtrl.DataMaxCount = 50;
UIRankCtrl.ListMaxCount = 20;
UIRankCtrl.BuffCount = 5;
UIRankCtrl.ForIndex = 1;
UIRankCtrl.GridOffset = 0;

--初始化方法--
function UIRankCtrl.InitPanel()
    
    UIMainCtrl.UIMain:AddClick(UIRankPanel.go_AllPlayerBtn,UIRankCtrl.SendRankAllPlayer);--全体玩家排名
    UIMainCtrl.UIMain:AddClick(UIRankPanel.go_FriendPlayerBtn,UIRankCtrl.SendRankFriendPlayer);--好友之间排名
    
    Event.AddListener(tostring(msg_pb.SERVER_RANK), this.SendRankAllPlayerCBK);
    --Event.AddListener(tostring(msg_pb.SERVER_LOGIN), this.SendLoginCBK);
    grid = UIRankPanel.go_AllPlayerGrid:GetComponent('UIGrid');
    UIRankCtrl.RankAllPlayerList = {};
    UIRankCtrl.removelist = {};
    log('UIRankCtrl.InitPanel over');
end

function UIRankCtrl.OnUpdate()
    EndLessScrollTest();
end

--关闭--
function UIRankCtrl.Close()
    --[[	uiMgr:ClosePanel(CtrlNames.Prompt);--]]
end

--无线卷轴控制
function EndLessScrollTest()
	
    if UIRankPanel ~= nil then
        if UIRankCtrl._lastPanelClipY == (UIRankPanel.go_SV.transform.localPosition.y - UIRankCtrl.GridOffset) then
            return 0;
        end
        UIRankCtrl._lastPanelClipY = (UIRankPanel.go_SV.transform.localPosition.y - UIRankCtrl.GridOffset);
        --log('GridOffset = '..tostring(UIRankCtrl.GridOffset));
        --log('_lastPanelClipY = '..tostring(UIRankCtrl._lastPanelClipY));
        local clipY = UIRankCtrl._lastPanelClipY;
        local mygrid = UIRankPanel.go_AllPlayerGrid:GetComponent('UIGrid');
        local index = math.floor(clipY/mygrid.cellHeight);
        --log('index = '..tostring(index));
        --log('UIRankCtrl._lastIndex = '..tostring(UIRankCtrl._lastIndex));
        if index == UIRankCtrl._lastIndex then
            return 0;
        end
        UIRankCtrl._lastIndex = index;
        --log('UIRankCtrl._lastIndex2 = '..tostring(UIRankCtrl._lastIndex));
        local startIndex = index - UIRankCtrl.BuffCount;
        --log('Curindex = '..tostring(index));
        if startIndex < 1 then
            startIndex = 1;
        end
        --log('startIndex = '..tostring(startIndex));
        local endIndex = startIndex + UIRankCtrl.ListMaxCount;
        --log('endIndex = '..tostring(endIndex));
        local totalNum = table.getn(UIRankModule.ServerRankData.users);
        log('totalNum = '..tostring(totalNum));
        if totalNum < UIRankCtrl.ListMaxCount then
            endIndex = totalNum - UIRankCtrl.BuffCount;
        end
        UIRankCtrl.removelist = {};
        local itemNum = table.getn(UIRankCtrl.RankAllPlayerList);
        for i = 1,itemNum do
            if i < (startIndex) then
                table.insert(UIRankCtrl.removelist,UIRankCtrl.RankAllPlayerList[i]);
                UIRankCtrl.RankAllPlayerList[i] = nil;
                --table.remove(UIRankCtrl.RankAllPlayerList,i);
                --log('table.insert = '..tostring(i));
            end
            if i > (endIndex + UIRankCtrl.BuffCount) then
                table.insert(UIRankCtrl.removelist,UIRankCtrl.RankAllPlayerList[i]);
                UIRankCtrl.RankAllPlayerList[i] = nil;
                --table.remove(UIRankCtrl.RankAllPlayerList,i);
                --log('table.insert = '..tostring(i));
            end
        end
        utilMgr:Des(UIRankCtrl.removelist);
        
        local MinNum = math.min(endIndex + UIRankCtrl.BuffCount,totalNum);
        --log('MinNum = '..tostring(MinNum));
        for i = startIndex,MinNum do
            if UIRankCtrl.RankAllPlayerList[i] == nil then
                UIRankCtrl.ForIndex = i;
                if UIRankCtrl.ForIndex <= totalNum then
                    UIRankCtrl:CreateItem();
                end
            end
        end
    end
end

--排行榜重新排序方法
function UIRankCtrl.ResetRankList()
    UIRankCtrl._lastIndex = 0;
    UIRankCtrl._maxIndex = 0;
    UIRankCtrl._lastPanelClipY = 0;
    UIRankCtrl._panelClipY = 0;
    UIRankCtrl.DataMaxCount = 50;
    UIRankCtrl.ListMaxCount = 15;
    UIRankCtrl.BuffCount = 3;
    UIRankCtrl.ForIndex = 1;
    log('rank list count:'..tostring(table.getn(UIRankModule.ServerRankData.users)));
    local totalNum = table.getn(UIRankModule.ServerRankData.users);
    if totalNum < 15 then
        UIRankCtrl.CoreResetRankList(1,totalNum);
    else
        UIRankCtrl.CoreResetRankList(1,15);
    end
    log('reset rank list!!!');
	 --激活全体排名选卡，休眠好友排名选卡
    UIRankPanel.go_AllPlayerTitle:SetActive(true);
    UIRankPanel.go_AllPlayerGrid:SetActive(true);
    UIRankPanel.go_FriendPlayerTitle:SetActive(false);
    UIRankPanel.go_FriendPlayerGrid:SetActive(false);
    UIRankPanel.go_SV:GetComponent('UIScrollView'):ResetPosition();
    grid:Reposition();
    
    UIRankCtrl.GridOffset = 362;
    --log('UIRankCtrl.GridOffset = '..tostring(UIRankCtrl.GridOffset));
    --log('grid.y = '..tostring(UIRankPanel.go_AllPlayerGrid.transform.localPosition.y));
    --log('UIScrollView.y = '..tostring(UIRankPanel.go_SV.transform.localPosition.y));
    --log('UIRankCtrl.RankAllPlayerList[1].y = '..tostring(UIRankCtrl.RankAllPlayerList[1].transform.localPosition.y));
    --log('UIRankCtrl.RankAllPlayerList[2].y = '..tostring(UIRankCtrl.RankAllPlayerList[2].transform.localPosition.y));
    --log('UIRankCtrl.RankAllPlayerList[3].y = '..tostring(UIRankCtrl.RankAllPlayerList[3].transform.localPosition.y));
    
    log('UIRankCtrl.ResetRankList finished');
end

function UIRankCtrl.CreateItem()
    local obj;
    log('CreateItem(index) = '..tostring(UIRankCtrl.ForIndex));   
    local mygrid = UIRankPanel.go_AllPlayerGrid:GetComponent('UIGrid');
    if	UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].rank <= 3 then
	if UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].rank==1 then
            obj = utilMgr:AddChildToPos(UIRankPanel.go_Top1,grid.transform,UIRankCtrl.ForIndex,mygrid.cellHeight);
	else
            if UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].rank==2 then
		obj = utilMgr:AddChildToPos(UIRankPanel.go_Top2,grid.transform,UIRankCtrl.ForIndex,mygrid.cellHeight);
            else
		obj = utilMgr:AddChildToPos(UIRankPanel.go_Top3,grid.transform,UIRankCtrl.ForIndex,mygrid.cellHeight);
            end
	end
    else 
	obj = utilMgr:AddChildToPos(UIRankPanel.go_RankItem,grid.transform,UIRankCtrl.ForIndex,mygrid.cellHeight);
    end				
    obj.transform:FindChild("Label_Name").gameObject:GetComponent('UILabel').text = UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].nick;
    obj.transform:FindChild("Sprite_Medel/Label_Rank").gameObject:GetComponent('UILabel').text = tostring(UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].rank);
    obj.transform:FindChild("Sprite_Lv/Label_Lv").gameObject:GetComponent('UILabel').text = UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].lv;
    obj.transform:FindChild("Sprite_Cup_Frame/Label_Cups").gameObject:GetComponent('UILabel').text = UIRankModule.ServerRankData.users[UIRankCtrl.ForIndex].trophy;
    obj:SetActive(true);
    UIRankCtrl.RankAllPlayerList[UIRankCtrl.ForIndex] = obj;--�����table
    return obj;
end

--核心排序方法
function UIRankCtrl.CoreResetRankList(Rfrom,Rto)
    log('DestoryListNum = '..tostring(table.getn(UIRankCtrl.RankAllPlayerList)));
    utilMgr:Des(UIRankCtrl.RankAllPlayerList);--ɾ��table������Ԫ��
    UIRankCtrl.RankAllPlayerList={}; --���table
    
    for i = Rfrom, Rto do --table.getn(UIRankModule.ServerRankData.users)
        local obj;
        if	UIRankModule.ServerRankData.users[i].rank <= 3 then
            if UIRankModule.ServerRankData.users[i].rank==1 then
                obj = utilMgr:AddChildToTarget(UIRankPanel.go_Top1,grid.transform,i);
            else
                if UIRankModule.ServerRankData.users[i].rank==2 then
                    obj = utilMgr:AddChildToTarget(UIRankPanel.go_Top2,grid.transform,i);
                else
                    obj = utilMgr:AddChildToTarget(UIRankPanel.go_Top3,grid.transform,i);
                end
            end
        else 
            obj = utilMgr:AddChildToTarget(UIRankPanel.go_RankItem,grid.transform,i);
        end
        log('CoreResetRankList:CreateItem(index) = '..tostring(i));
        obj.transform:FindChild("Label_Name").gameObject:GetComponent('UILabel').text = UIRankModule.ServerRankData.users[i].nick;
        obj.transform:FindChild("Sprite_Medel/Label_Rank").gameObject:GetComponent('UILabel').text = tostring(UIRankModule.ServerRankData.users[i].rank);
        obj.transform:FindChild("Sprite_Lv/Label_Lv").gameObject:GetComponent('UILabel').text = UIRankModule.ServerRankData.users[i].lv;
        obj.transform:FindChild("Sprite_Cup_Frame/Label_Cups").gameObject:GetComponent('UILabel').text = UIRankModule.ServerRankData.users[i].trophy;
        obj:SetActive(true);
        UIRankCtrl.RankAllPlayerList[i] = obj;--�����table
    end
    
    log('CoreResetRankList ends');
end



--向服务器发送消息，更新全体玩家排名
function UIRankCtrl.SendRankAllPlayer()
    local rank = rank_pb.ClientRank();
    local msg = rank:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_RANK);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_RANK)..":   SendRankAllPlayer finished");
end

--服务器回调，更新全体玩家排名
function UIRankCtrl.SendRankAllPlayerCBK(buffer)
    log('SendRankAllPlayerCBK starting');
    local data = buffer:ReadBuffer();
    local msg = rank_pb.ServerRank();
    msg:ParseFromString(data);
    UIRankModule.ServerRankData = msg;
    UIRankCtrl.ResetRankList();
end

function UIRankCtrl.SendRankFriendPlayer()
    
    --点“好友排名”，显示消息提醒
    UIMainCtrl.MessageTip("好友功能暂未开放...");
    --[[local login = auth_pb.ClientLogin();
    login.account = name;
    local msg = login:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_LOGIN);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_LOGIN));--]]
end

function UIRankCtrl.SendRankFriendPlayerCBK(buffer)
    local data = buffer:ReadBuffer();
    local msg = auth_pb.ServerLogin();
    msg:ParseFromString(data);
    UILoginModule.ServerLoginData = msg;
    --激活好友排名选卡，休眠全体排名选卡
    UIRankPanel.go_FriendPlayerTitle:SetActive(true);
    UIRankPanel.go_FriendPlayerGrid:SetActive(true);
    UIRankPanel.go_AllPlayerTitle:SetActive(false);
    UIRankPanel.go_AllPlayerGrid:SetActive(false);
    log(tostring(msg.result)..'SendRankFriendPlayerCBK');
end

