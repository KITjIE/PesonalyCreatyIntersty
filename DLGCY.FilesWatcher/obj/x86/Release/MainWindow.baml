   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  I EWPFTemplateLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullaAclr-namespace:WPFTemplateLib.UserControls;assembly=WPFTemplateLibWPFTemplateLib.UserControls O KTemplateClassLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Wclr-namespace:TemplateClassLibrary.ClassComponent.Loading;assembly=TemplateClassLibrary+TemplateClassLibrary.ClassComponent.Loading  DLGCY.FilesWatcher#   DLGCY.FilesWatcher.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  @d2http://schemas.microsoft.com/expression/blend/2008   	  Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	  ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity   	  h
 dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions 
 TuserControlsAclr-namespace:WPFTemplateLib.UserControls;assembly=WPFTemplateLib 
 eLoadingWclr-namespace:TemplateClassLibrary.ClassComponent.Loading;assembly=TemplateClassLibrary 
   � Title$  日志解析程序��$
��Auto��$	��800�� � WindowStartupLocation$ CenterScreen=�   FontAlibaba#��[  .��� -    �Name$ MGrid�� � ColumnDefinitions �� $	��15* ��� $	��21* ��� $	��40* ��� $
��414* ��� $
��310* � � RowDefinitions �� $
��Auto ��� �� $
��Auto �.+�9�  9� 
IsExpanded$
 True��$��	配置区��$��5�� � 	Resources  �� 
TargetType �� Property	 �� Value��%�   '��       '�� 9     �� " ��.���  � ��$	 80����  � ��$	 25���� " ��.���  � ��$		 200����  � ��$	 20��.���� 
 �� Orientation$
 
Horizontal=�$��10q����	#FF81BFA1   DataTemplateExpanderHeader  ExpanderContentGrid  ExpanderContentTextBlock  ExpanderHeaderTextBlock �� BasedOn  ExpanderContentButtonV  Primary  ExpanderContentLabel��%  '9�       &	 n     &	 �     7  &	 �     7  7   &	 7    �� " 9�.���  �@9�ExpandDirection$	 Right=���  �@9�
IsExpanded$	 False����  � ��#	 �  �� " �.���  � ��$
	 Auto����  � ��
	 ���_��� " ��8   �� " ��8   .���  � ��$	 25����  � ��$		 110����  � ��$	 10q���  � ��$	 14(���  � ��8	  �� " ��.���  � ��$	 20����  � ��$	 10q���  � ��$
	 Wrap=���  � ��$	 Center=���  �@��TextAlignment$	 Center=�.+�� -   $ TGrid��#��[ $	��182��$	��568��.+��� $
 
Horizontal=�.+��� $	��437��.+��� $��50��$	��434��$
 
Horizontal=�.+��� $��解析路径：��$	��Top=�$��	0,12,0,10q�$��14(�$��31����  �� 
IsReadOnly$
 True��$��3��$��22������ *Configs.FolderPath+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� � ToolTip �� *Text+ �� RelativeSource ��  �� Mode$
 Self=���   PrimaryButton#��[ $��选择文件夹��$��3��$��26������ *ChooseFolderCommand+�� $��32��$	��434��$
 
Horizontal=�.+��� $��转移路径:��$��26��$��80��$��14(��� $	��346��$��22������ *Configs.FinalPath+$ TwoWay=�$ PropertyChanged=�{��� *Configs.EditMode+ �� *Text+ �� $
 Self=��� $��29��$	��434��$
 
Horizontal=�.+��� $
��URL:��$��80��$��14(���  �� TextWrapping$
 Wrap=�$	��346��$��3��$��2������ *Configs.URLPath+$ TwoWay=�$ PropertyChanged=�{��� *Configs.EditMode+ �� *Text+ �� $
 Self=��� $��69��$	��434�� J� RenderTransformOrigin$ 0.5,0.5&�.+��� $��过站结果:��$	��106��$��3��$��20(�$��1��$��36��$��0,0,0,30q�$
��Left=��� $
��Left=�$
��Wrap=�$	��Top=�$��1��$
��Bold"�$��18(�$	��288��
�������$��3��$��15,-30,0,30q����� *Configs.ProductBarcode+�� $��1q�$��Right=�$	��120��$��60��$��0,-120,25,0q�._��� $��Center=�$��Center=�$��55(����	#FFF9F9F9���� *Configs.UploadResult+���� " �� �� Triggers ��  �� Binding �� *Configs.UploadResult+ �� Value
 成功.a���  � ��
	 � � ���  �� *Configs.UploadResult+
 失败.a���  � ��
	 �  ���� -    ��Name$ 	ChangeBtn��#��[ $��修改配置��$��
5,10,10,10q����� *ChangeConfigCommand+�� #��[ $��保存配置��$��10,10,10,10q����� *SaveConfigCommand+�� #��[ $��重载配置��$��10,10,10,10q����� *LoadConfigCommand+�� $	��180��$	��Top=�$	��217��.+��� $��42��$	��208��$
��Left=�.+�]� -    ]�Name$ Combbox��$��0 10 0 0q�$
��Left=�.+��� $��解析模式�����	#FF12110F$
��Bold"�$��65��$��13(���  	 
PrimaryBox#��� 	 $��80��$��22��$��5q����	#FF108EE9$	��119�� �� DisplayMemberPath$
 Text�� �� SelectedValuePath$	 Key������ *vMTempTest.CombboxList+ �� SelectedItem �� *vMTempTest.CombboxItem+{��� *Configs.EditMode+�� $��31��$
 
Horizontal=�$	��208��$
��Left=�.+��� $��建立电脑�����	#FF1625A2$
��Bold"�$��70��$��13(��� $
 Wrap=�$	��120������ *Configs.Computer+{��� *Configs.EditMode+�� $��31��$
 
Horizontal=�$	��208��$
��Left=�.+��� $��建立用户�����	#FF352CA7$
��Bold"�$��70��$��13(��� $
 Wrap=�$	��120������ *Configs.BuildUser+{��� *Configs.EditMode+�� $��31��$
 
Horizontal=�$	��208��$
��Left=�.+��� $��产品型号�����	#FF352CA7$
��Bold"�$��70��$��13(��� $
 Wrap=�$	��120������ *Configs.PorductModel+{��� *Configs.EditMode+�� $��31��$
 
Horizontal=�$	��208��$
��Left=�.+��� $��机器型号�����	#FF352CA7$
��Bold"�$��71��$��13(��� $
 Wrap=�$	��120������ *Configs.MachineModel+{��� *Configs.EditMode+�� $��77��$��26�� 
 Default#��[
 $��Right=�$	��Top=�$��4��$��	0,5,140,0q�$��导出配置������ *ExportConfigCommand+�� $��77��$��26��#��[
 $��Right=�$	��Top=�$��4��$��	0,5,225,0q�$��导入配置������ *InputConfigCommand+{��� *Configs.EditMode+�� $��77��$��26��#��[
 $��Right=�$	��Top=�$��4��$��0,5,50,0q�$��解析模式说明������ *AnalysCommand+�� $��28��$��26��#��[
 $��Right=�$	��Top=�$��4��$��0,5,10,0q�$��关于������ *AboutCommand+� $��1��$��5��$��0,0,0,20q�$��2�� ��%Z   '��       '�� #     �� " ��.���  � ��$	 20���� " ��.���  � ��$	 20�� �� $
��Auto ��� .+�� $��1��$��10q� �� $��* ��� $
��Auto �.+��� -    ��Name$ TBInfo��$��0��$
��Auto��$
 Wrap=� �� AcceptsReturn$
 True��" �� VerticalScrollBarVisibility$
 Auto=����� *Info+.��.   (System.Windows.Interactivity.Interaction   Triggers 9 
 3Microsoft.Expression.Interactivity.Core.DataTrigger      
Comparison$  NotEqual=�!   Value$! 0��D 
 >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger"   Binding" �� *Text.Length+# �� ElementName$# TBInfo��$   	Behaviors$ ? 
 9Microsoft.Expression.Interactivity.Core.ConditionBehavior  %   	Condition.% C 
 =Microsoft.Expression.Interactivity.Core.ConditionalExpression  &   
Conditions.& A 
 ;Microsoft.Expression.Interactivity.Core.ComparisonCondition  '   Operator$' Equal=�(   RightOperand$
( True��)   LeftOperand) �� *	IsChecked+$# CBIsAutoScroll��.   (System.Windows.Interactivity.TriggerBase*   Actions.* B 
 <Microsoft.Expression.Interactivity.Core.ChangePropertyAction  8	   2System.Windows.Interactivity.TargetedTriggerAction+ 	  
TargetName$+ TBInfo��,   PropertyName$, 
CaretIndex��-   Value- �� *Text.Length+$# TBInfo��>
 
 8Microsoft.Expression.Interactivity.Core.CallMethodAction
  . 
  
MethodName$. ScrollToEnd��.��9� $��1��$��	操作区��#���  / 9� ExpandDirection$
/ Left=�$
 True��.���� $��3q�$
��True��.����    SP  CheckBoxBasic��%K  '��       '�� :     7  &	 I     '�� �     '�� �     7  �� " ��.���  � ��$		 200����  � ��$	 20���� " ��8   �� " ��.���  �@��Orientation$	 
Horizontal=���  � ��$	 25����  � ��$	 5q��� " ��.���  � ��$	 80����  � ��$		 5,0q��� " ��8   .���  � ��$	 Center=�.+��� #��[ .+��� $!��是否显示MES报错框？��$	��130���� $��Center=�0 d� 	IsChecked0 �� *Configs.IsMESerrorWin+  TrueFalseConverter1 �� 	Converter#1 [ $ TwoWay=��� #��[ .+��� $$��是否匹配AOI产品型号？��$	��130���� $��Center=�0 �� *Configs.IsMatchAOIModel+#1 [ $ TwoWay=��� #��[ .+��� $$��是否自动滚动消息框？��$	��130���� -   2 ��Name$2 CBIsAutoScroll��$��Center=�0 �� *Configs.IsAutoScroll+#1 [ $ TwoWay=��� #��[ .+��� $!��消息是否自动减半？��$	��130���� $��Center=�0 �� *Configs.IsAutoHalve+#1 [ $ TwoWay=��� #��[ .+��� $��消息减半阈值：��$��90���� $��60������ *!Configs.AutoHalveThresholdValue+$ TwoWay=�$ PropertyChanged=� �� *Text+ �� $
 Self=��� #��[ .+��� $'��!条码特殊符号是否替换？��$	��140���� $��Center=�0 �� *Configs.IsCodeReplace+#1 [ $ TwoWay=��� #��[ .+��� $��替换前：��$��60���� $��90������ *Configs.IsCodeReplaceBefore+$ TwoWay=�$ PropertyChanged=� �� *Text+ �� $
 Self=��� #��[ .+��� $��替换后：��$��60���� $��90������ *Configs.IsCodeReplaceAfter+$ TwoWay=�$ PropertyChanged=� �� *Text+ �� $
 Self=��� $��Center=�$��清空消息������ *ClearInfoCommand+�� #��[ .+��� $!��是否显示完全路径？��$	��130���� $��Center=�0 �� *Configs.IsShowFullPath+#1 [ $ TwoWay=��� #��[ .+��� $!��是否监控子文件夹？��$	��130���� $��Center=�$ 切换后请重新监控��0 �� *Configs.IsMonitorSubDir+#1 [ $ TwoWay=��� #��[ .+��� $��是否正在监控？��$	��130���� $��Center=�${�False��0 �� *IsMonitoring+#1 [ $ OneWay=��� $��Center=����	#FF48E910$��自动解析【启动】������ *StartWatchCommand+�� $��Center=����	#FFE94210$��自动解析【停止】������ *StopWatchCommand+�� $��Center=�$��手动上传文件������ *HandWatchCommand+�� $��Center=����	#FF9810E9$��人工扫码上传������ *ScanCodeUploadCommand+�� $��Center=����	#FF010102$��测试�����	#FF612424���� *WaitCommand+�� $
 
Horizontal=�$��5,5,5,5q�$��27��.+��� $��已上传：��$��63��$
��Bold"��� $
 Wrap=�$��80��$
 True������ *Configs.AnalysCount+�� $��2��$��5��.@��� ���� *Status+/  )WPFTemplateLib.UserControls.UC_ConfirmBox  $��3��$y�	Collapsed=�3   DialogWidth$	3 500��4   DialogHeight$	4 400��$��5��5 � DataContext5 �� *
DialogVm+6   IsShowDialog6 �� *IsShowDialog+