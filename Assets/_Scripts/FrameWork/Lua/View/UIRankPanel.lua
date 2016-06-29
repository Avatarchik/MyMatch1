
UIRankPanel = {};
local this = UIRankPanel;
--启动事件--

--初始化面板--
function UIRankPanel.InitPanel()
	--获取“排行榜-全部玩家“按钮,选中图，Grid
	UIRankPanel.go_AllPlayerBtn=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/AllPlayer/Sprite_Card_Off").gameObject; 
	UIRankPanel.go_AllPlayerTitle=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/AllPlayer/Sprite_Card_On").gameObject; 
	UIRankPanel.go_AllPlayerGrid=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Grid_All").gameObject; 
	UIRankPanel.go_RankItem=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/RankListItem").gameObject; 
	UIRankPanel.go_Top1=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/Rank1Item").gameObject; 
	UIRankPanel.go_Top2=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/Rank2Item").gameObject; 
	UIRankPanel.go_Top3=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Sample/Rank3Item").gameObject; 
	
	--获取“排行榜-好友玩家“按钮,选中图，Grid
	UIRankPanel.go_FriendPlayerBtn=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/Friends/Sprite_Card_Off1").gameObject; 
	UIRankPanel.go_FriendPlayerTitle=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/ToggleCard/Friends/Sprite_Card_On").gameObject; 
	UIRankPanel.go_FriendPlayerGrid=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Grid_Friends").gameObject; 
	
	--获取ScrollView游戏物体
	UIRankPanel.go_SV=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank").gameObject;
	--获取Scrollbar
	UIRankPanel.small_ScrollBar=UIMainPanel.Gtransform:FindChild("ScrollView_Panel/Grid/Sprite_Rank/RankWhole/RankingContent/Sprite_Frame/Scroll View_Rank/Vertical Scroll Bar").gameObject:GetComponent('UIScrollBar');
end

function UIRankPanel.OnUpdate()

end

--单击事件--
function UIRankPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

