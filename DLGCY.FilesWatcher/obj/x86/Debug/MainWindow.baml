   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   I EWPFTemplateLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullaAclr-namespace:WPFTemplateLib.UserControls;assembly=WPFTemplateLibWPFTemplateLib.UserControls 5   	   O KTemplateClassLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Wclr-namespace:TemplateClassLibrary.ClassComponent.Loading;assembly=TemplateClassLibrary+TemplateClassLibrary.ClassComponent.Loading 5
   	    DLGCY.FilesWatcher#   DLGCY.FilesWatcher.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  5   	   @d2http://schemas.microsoft.com/expression/blend/2008   	  5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	  5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity   	  5   	   h
 dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions 
 5   	   TuserControlsAclr-namespace:WPFTemplateLib.UserControls;assembly=WPFTemplateLib 
 5   	   eLoadingWclr-namespace:TemplateClassLibrary.ClassComponent.Loading;assembly=TemplateClassLibrary 
 5
   	     � Title$  日志解析程序��5   	   $
��Auto��6   $	��800��6&    � WindowStartupLocation$ CenterScreen=�62      FontAlibaba#��[  5      .��5      � -   5         �Name$ MGrid��5       � ColumnDefinitions 5   
   �� 5      $	��15* �6   �� 5      $	��21* �6   �� 5      $	��40* �6   �� 5      $
��414* �6   �� 5      $
��310* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   9�  9� 
IsExpanded$
 True��6   $��	配置区��6%   $��5��62    � 	Resources 5       �� 
TargetType �� Property	 �� Value��%�   '��       '�� l     �� 5      " ��6   .�5       ��  � ��$	 80��6.   �� 5!       � ��$	 25��6/   5"      �� 5$      " ��6   .�5%      ��  � ��$		 200��6.   �� 5&       � ��$	 20��6/   5'      5(      .��5*      �� 
 �� Orientation$
 
Horizontal=�6   $��10q�62   ���	#FF81BFA1 5+        DataTemplateExpanderHeader  ExpanderContentGrid  ExpanderContentTextBlock  ExpanderHeaderTextBlock �� BasedOn  ExpanderContentButtonV  Primary  ExpanderContentLabel��%I  '9�       &	 �     &	     7  &	 A    7  7   &	     �� 5,      " 9�6   .�5-      ��  �@9�ExpandDirection$	 Right=�6<   �� 5.       �@9�
IsExpanded$	 False��67   �� 5/       � ��#	 �  6;   50      �� 52      " �68   .�53      ��  � ��$
	 Auto��62   �� 54       � ��
	 ���_�67   55      �� 57      " ��6=   8   6]   �� 59      " ��6;   8   .�5:      ��  � ��$	 25��62   �� 5;       � ��$		 110��63   �� 5<       � ��$	 10q�63   �� 5=       � ��$	 14(�65   �� 5>       � ��8	  67   5?      �� 5A      " ��69   .�5B      ��  � ��$	 20��62   �� 5C       � ��$	 10q�63   �� 5D       � ��$
	 Wrap=�69   �� 5E       � ��$	 Center=�6>   �� 5F       �@��TextAlignment$	 Center=�6:   5G      5I      .+�5K      � -   5        $ TGrid��5K      #��[ 6&   $	��182��6S   $	��568��6`   .+�5L      �� $
 
Horizontal=�6!   .+�5O      �� $	��437��6&   .+�5P      �� $��50��6)   $	��434��65   $
 
Horizontal=�6A   .+�5Q   "   �� $��解析路径：��6(   $	��Top=�68   $��	0,12,0,10q�6P   $��14(�6c   $��31��6q   �� 5R   "    �� 
IsReadOnly$
 True��5S   K   $��3��6]   $��22��6m   ��5R   *   �� *Configs.FolderPath+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� � ToolTip 5S      �� *Text+ �� RelativeSource ��  �� Mode$
 Self=�6m   �� 5T   "     PrimaryButton#��[ 6)   $��选择文件夹��6x   $��3��6�   $��26��6�   ��6P   �� *ChooseFolderCommand+6�   5U      �� 5V      $��32��6)   $	��434��65   $
 
Horizontal=�6A   .+�5W   "   �� $��转移路径:��6(   $��26��68   $��80��6D   $��14(�6O   �� 5X   "   $	��346��5Y   e   $��22��6q   ��5X   *   �� *Configs.FinalPath+$ TwoWay=�$ PropertyChanged=�{�6   �� *Configs.EditMode+ 5Y   !   �� *Text+ �� $
 Self=�6q   5Z      �� 5[      $��29��6)   $	��434��65   $
 
Horizontal=�6A   .+�5\   "   �� $
��URL:��6(   $��80��67   $��14(�6B   �� 5]   "    �� TextWrapping$
 Wrap=�6*   $	��346��6>   $��3��5^   f   $��2��6v   ��5]   J   �� *Configs.URLPath+$ TwoWay=�$ PropertyChanged=�{�6�   �� *Configs.EditMode+ 5^   "   �� *Text+ �� $
 Self=�6v   5_      �� 5`      $��69��6)   $	��434��65    J� RenderTransformOrigin$ 0.5,0.5&�6A   .+�5b   "   �� $��过站结果:��6(   $	��106��68   $��3��6D   $��20(�6X   $��1��6f   $��36��6v   $��0,0,0,30q�6�   $
��Left=�6�   �� 5c   "   $
��Left=�6V   $
��Wrap=�6r   $	��Top=�6�   $��1��6�   $
��Bold"�6�   $��18(�6�   $	��288��6�   
�������$��3��6�   $��15,-30,0,30q�6  ��6-   �� *Configs.ProductBarcode+6  �� 5d   "   $��1q�6)   $��Right=�6=   $	��120��6Y   $��60��6e   $��0,-120,25,0q�6q   ._�5e   &   �� $��Center=�6V   $��Center=�6s   $��55(�6�   ���	#FFF9F9F9��60   �� *Configs.UploadResult+��5f   *   �� 5g   .   " ��64    �� Triggers 5h   2   �� 5i   6    �� Binding 6B   �� *Configs.UploadResult+ �� Value
 成功6k   .a�5j   :   ��  � ��
	 � � �6W   5k   7   �� 5l   6    6B   �� *Configs.UploadResult+
 失败6k   .a�5m   :   ��  � ��
	 �  ��6W   5n   7   5p   /   5r   '   5s   #   5t      5u      �� 5w      -   5         ��Name$ 	ChangeBtn��5w   !   #��[ 64   $��修改配置��6�   $��
5,10,10,10q�6�   ��6d   �� *ChangeConfigCommand+6�   �� 5x      #��[ 6!   $��保存配置��6w   $��10,10,10,10q�6�   ��6Q   �� *SaveConfigCommand+6�   �� 5y      #��[ 6!   $��重载配置��6w   $��10,10,10,10q�6�   ��6Q   �� *LoadConfigCommand+6�   5z      5�      �� 5�      $	��180��6   $	��Top=�6*   $	��217��6B   .+�5�      �� $��42��6!   $	��208��6-   $
��Left=�69   .+�5�      ]� -   5         ]�Name$ Combbox��5�   $   $��0 10 0 0q�65   $
��Left=�6G   .+�5�      �� $��解析模式��6$   ���	#FF12110F$
��Bold"�6J   $��65��6\   $��13(�6g   �� 5�       	 
PrimaryBox#��� 	 6'   $��80��6L   $��22��6Z   $��5q�6f   ���	#FF108EE9$	��119��6�    �� DisplayMemberPath$
 Text��5�       �� SelectedValuePath$	 Key��62   ��5�      �� *vMTempTest.CombboxList+ �� SelectedItem 6I   �� *vMTempTest.CombboxItem+{�5�   J   �� *Configs.EditMode+6s   5�      5�      �� 5�      $��31��6!   $
 
Horizontal=�6-   $	��208��6F   $
��Left=�6R   .+�5�      �� $��建立电脑��6    ���	#FF1625A2$
��Bold"�6F   $��70��6X   $��13(�6c   �� 5�      $
 Wrap=�6"   $	��120��66   ��6B   �� *Configs.Computer+{�6d   �� *Configs.EditMode+5�      �� 5�      $��31��6!   $
 
Horizontal=�6-   $	��208��6F   $
��Left=�6R   .+�5�      �� $��建立用户��6    ���	#FF352CA7$
��Bold"�6F   $��70��6X   $��13(�6c   �� 5�      $
 Wrap=�6"   $	��120��66   ��6B   �� *Configs.BuildUser+{�6e   �� *Configs.EditMode+5�      �� 5�      $��31��6!   $
 
Horizontal=�6-   $	��208��6F   $
��Left=�6R   .+�5�      �� $��产品型号��6    ���	#FF352CA7$
��Bold"�6F   $��70��6X   $��13(�6c   �� 5�      $
 Wrap=�6"   $	��120��66   ��6B   �� *Configs.PorductModel+{�6h   �� *Configs.EditMode+5�      �� 5�      $��31��6!   $
 
Horizontal=�6-   $	��208��6F   $
��Left=�6R   .+�5�      �� $��机器型号��6    ���	#FF352CA7$
��Bold"�6F   $��71��6X   $��13(�6c   �� 5�      $
 Wrap=�6"   $	��120��66   ��6B   �� *Configs.MachineModel+{�6h   �� *Configs.EditMode+5�      5�      5�      5�      �� 5�   
   $��77��69   $��26��6D    
 Default#��[
 6P   $��Right=�6q   $	��Top=�6�   $��4��6�   $��	0,5,140,0q�6�   $��导出配置��6�   ��6   �� *ExportConfigCommand+6�   �� 5�   
   $��77��68   $��26��6C   #��[
 6O   $��Right=�6�   $	��Top=�6�   $��4��6�   $��	0,5,225,0q�6�   $��导入配置��6�   ��6   �� *InputConfigCommand+{�6p   �� *Configs.EditMode+6�   �� 5�   
   $��77��63   $��26��6>   #��[
 6J   $��Right=�6k   $	��Top=�6�   $��4��6�   $��0,5,50,0q�6�   $��解析模式说明��6�   ��6   �� *AnalysCommand+6�   �� 5�   
   $��28��62   $��26��6=   #��[
 6I   $��Right=�6j   $	��Top=�6�   $��4��6�   $��0,5,10,0q�6�   $��关于��6�   ��6   �� *AboutCommand+6�   � 5�   
   $��1��6   $��5��6   $��0,0,0,20q�60   $��2��6B    5�      ��%�   '��       '�� H     �� 5�      " ��6   .�5�      ��  � ��$	 20��6/   5�      �� 5�      " ��6   .�5�      ��  � ��$	 20��6/   5�      5�       5�      �� 5�      $
��Auto �6    �� 5�      .+�5�      � $��1��6   $��10q�6     5�      �� 5�      $��* �6'   �� 5�      $
��Auto �6'   .+�5�      �� -   5         ��Name$ TBInfo��5�      $��0��6*   $
��Auto��6:   $
 Wrap=�6H    �� AcceptsReturn$
 True��6\   " �� VerticalScrollBarVisibility$
 Auto=�6q   ��6�   �� *Info+.��6�   .   (System.Windows.Interactivity.Interaction   Triggers 5�      9 
 3Microsoft.Expression.Interactivity.Core.DataTrigger  5�          
Comparison$  NotEqual=�6]   !   Value$! 0��6s   D 
 >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger"   Binding" 6)   �� *Text.Length+# �� ElementName$# TBInfo��$   	Behaviors$ 5�      ? 
 9Microsoft.Expression.Interactivity.Core.ConditionBehavior  5�   "   %   	Condition.% 5�   &   C 
 =Microsoft.Expression.Interactivity.Core.ConditionalExpression  &   
Conditions.& 5�   *   A 
 ;Microsoft.Expression.Interactivity.Core.ComparisonCondition  '   Operator$' Equal=�6   (   RightOperand$
( True��6�   )   LeftOperand) 6A   �� *	IsChecked+$# CBIsAutoScroll��6�   5�   '   5�   #   .   (System.Windows.Interactivity.TriggerBase*   Actions.* 5�      B 
 <Microsoft.Expression.Interactivity.Core.ChangePropertyAction  8	   2System.Windows.Interactivity.TargetedTriggerAction+ 	  
TargetName$+ TBInfo��66   ,   PropertyName$, 
CaretIndex��6J   -   Value- 6d   �� *Text.Length+$# TBInfo��>
 
 8Microsoft.Expression.Interactivity.Core.CallMethodAction
  5�      . 
  
MethodName$. ScrollToEnd��62   5�      .��5�   .   5�      9� 5�      $��1��6   $��	操作区��6+   #���  68   / 9� ExpandDirection$
/ Left=�6v   $
 True��6�   .��5�      �� $��3q�6#   $
��True��6.   .��5�      ��  5�        SP  CheckBoxBasic��%*  '��       '�� m     7  &	 �     '�� 2    '�� �    7  �� 5�   "   " ��6(   .�5�   &   ��  � ��$		 200��6>   �� 5�   &    � ��$	 20��6?   5�   #   �� 5�   "   " ��6(   8   6E   �� 5�   "   " ��63   .�5�   &   ��  �@��Orientation$	 
Horizontal=�6D   �� 5�   &    � ��$	 25��6?   �� 5�   &    � ��$	 5q�6?   5�   #   �� 5�   "   " ��6(   .�5�   &   ��  � ��$	 80��6>   �� 5�   &    � ��$		 5,0q�6?   5�   #   �� 5�   "   " ��6(   8   .�5�   &   ��  � ��$	 Center=�6J   5�   #   5�      .+�5�      �� #��[ 6)   .+�5�   "   �� $!��是否显示MES报错框？��6(   $	��130��6>   �� 5�   "   $��Center=�6�   0 d� 	IsChecked0 6+   �� *Configs.IsMESerrorWin+  TrueFalseConverter1 �� 	Converter#1 [ $ TwoWay=�6�   5�      �� 5�      #��[ 6)   .+�5�   "   �� $$��是否匹配AOI产品型号？��6(   $	��130��6?   �� 5�   "   $��Center=�6�   0 6+   �� *Configs.IsMatchAOIModel+#1 [ $ TwoWay=�6�   5�      �� 5�      #��[ 6)   .+�5�   "   �� $$��是否自动滚动消息框？��6(   $	��130��6=   �� 5�   "   -   5        2 ��Name$2 CBIsAutoScroll��5�   +   $��Center=�6�   0 6C   �� *Configs.IsAutoScroll+#1 [ $ TwoWay=�6�   5�      �� 5�      #��[ 6)   .+�5�   "   �� $!��消息是否自动减半？��6(   $	��130��6<   �� 5�   "   $��Center=�6�   0 6+   �� *Configs.IsAutoHalve+#1 [ $ TwoWay=�6�   5�      �� 5�      #��[ 6)   .+�5�   "   �� $��消息减半阈值：��6(   $��90��6:   �� 5�   "   $��60��6*   ��65   �� *!Configs.AutoHalveThresholdValue+$ TwoWay=�$ PropertyChanged=� 6�   �� *Text+ �� $
 Self=�5�      �� 5�      #��[ 6)   .+�5�   "   �� $'��!条码特殊符号是否替换？��6(   $	��140��6>   �� 5�   "   $��Center=�6�   0 6+   �� *Configs.IsCodeReplace+#1 [ $ TwoWay=�6�   5�      �� 5�      #��[ 6)   .+�5�   "   �� $��替换前：��6(   $��60��67   �� 5�   "   $��90��6*   ��65   �� *Configs.IsCodeReplaceBefore+$ TwoWay=�$ PropertyChanged=� 6�   �� *Text+ �� $
 Self=�5�      �� 5�      #��[ 6)   .+�5   "   �� $��替换后：��6(   $��60��67   �� 5  "   $��90��6*   ��65   �� *Configs.IsCodeReplaceAfter+$ TwoWay=�$ PropertyChanged=� 6�   �� *Text+ �� $
 Self=�5     �� 5     $��Center=�6J   $��清空消息��6g   ��6%   �� *ClearInfoCommand+6g   �� 5     #��[ 6)   .+�5  "   �� $!��是否显示完全路径？��6(   $	��130��6<   �� 5  "   $��Center=�6�   0 6+   �� *Configs.IsShowFullPath+#1 [ $ TwoWay=�6�   5     �� 5	     #��[ 6)   .+�5
  "   �� $!��是否监控子文件夹？��6(   $	��130��6<   �� 5  "   $��Center=�6�   $ 切换后请重新监控��6�   0 6+   �� *Configs.IsMonitorSubDir+#1 [ $ TwoWay=�6�   5     �� 5     #��[ 6)   .+�5  "   �� $��是否正在监控？��6(   $	��130��6:   �� 5  "   $��Center=�6�   ${�False��6�   0 6+   �� *IsMonitoring+#1 [ $ OneWay=�6�   5     �� 5     $��Center=�6K   ���	#FF48E910$��自动解析【启动】��6   ��6%   �� *StartWatchCommand+6   �� 5     $��Center=�6J   ���	#FFE94210$��自动解析【停止】��6~   ��6%   �� *StopWatchCommand+6~   �� 5     $��Center=�6J   $��手动上传文件��6g   ��6%   �� *HandWatchCommand+6g   �� 5     $��Center=�6O   ���	#FF9810E9$��人工扫码上传��6�   ��6%   �� *ScanCodeUploadCommand+6�   �� 5     $��Center=�6E   ���	#FF010102$��测试��6y   ���	#FF612424��6%   �� *WaitCommand+6�   �� 5     $
 
Horizontal=�6)   $��5,5,5,5q�6B   $��27��6S   .+�5  "   �� $��已上传：��6(   $��63��67   $
��Bold"�6B   �� 5  "   $
 Wrap=�6*   $��80��6c   $
 True��6n   ��6>   �� *Configs.AnalysCount+6n   5     5     5     5     5     5      �� 5"  
   $��2��6   $��5��6!   .@�5#     �� ��6   �� *Status+5$     /  )WPFTemplateLib.UserControls.UC_ConfirmBox  5&  
   $��3��6%   $y�	Collapsed=�6W   3   DialogWidth$	3 500��6�   4   DialogHeight$	4 400��6�   $��5��6�   5 � DataContext5 66   �� *
DialogVm+6   IsShowDialog6 6n   �� *IsShowDialog+6�   5+     5-     