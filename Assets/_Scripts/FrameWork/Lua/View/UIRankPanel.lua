
UIRankPanel = {};
local this = UIRankPanel;
--�����¼�--

--��ʼ�����--
function UIRankPanel.InitPanel()
	--��ȡ�����а�-ȫ����ҡ���ť,ѡ��ͼ��Grid
	UIRankPanel.go_AllPlayerBtn=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/AllPlayer/Sprite_Card_Off").gameObject; 
	UIRankPanel.go_AllPlayerTitle=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/AllPlayer/Sprite_Card_On").gameObject; 
	UIRankPanel.go_AllPlayerGrid=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Grid_All").gameObject; 
	UIRankPanel.go_RankItem=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/RankListItem").gameObject; 
	UIRankPanel.go_Top1=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/Rank1Item").gameObject; 
	UIRankPanel.go_Top2=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/Rank2Item").gameObject; 
	UIRankPanel.go_Top3=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/Rank3Item").gameObject; 
	
	--��ȡ�����а�-������ҡ���ť,ѡ��ͼ��Grid
	UIRankPanel.go_FriendPlayerBtn=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/Friends/Sprite_Card_Off1").gameObject; 
	UIRankPanel.go_FriendPlayerTitle=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/Friends/Sprite_Card_On").gameObject; 
	UIRankPanel.go_FriendPlayerGrid=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Grid_Friends").gameObject; 
	
	--��ȡScrollView��Ϸ����
	UIRankPanel.go_SV=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank").gameObject;
	--��ȡScrollbar
	UIRankPanel.small_ScrollBar=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Vertical Scroll Bar").gameObject:GetComponent('UIScrollBar');
end

function UIRankPanel.OnUpdate()

end

--�����¼�--
function UIRankPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

