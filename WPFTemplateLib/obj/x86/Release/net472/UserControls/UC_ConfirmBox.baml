   M S B A M L   `   `   `  ���� I  EWPFTemplateLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:WPFTemplateLib.UserControlsWPFTemplateLib.UserControls  K*clr-namespace:WPFTemplateLib.WpfConvertersWPFTemplateLib.WpfConverters  /    )WPFTemplateLib.UserControls.UC_ConfirmBox   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     BuserControls)clr-namespace:WPFTemplateLib.UserControls     DwpfConverters*clr-namespace:WPFTemplateLib.WpfConverters       � 	Resources  3   -WPFTemplateLib.WpfConverters.VisibleConverter   VisibleConverter5   /WPFTemplateLib.WpfConverters.CollapsedConverter  CollapsedConverter��%   &	        &	          .��� ���	#905F9EA0.+�� 
����������� *DialogWidth+ �� RelativeSource ��  �� AncestorType UserControl �� Mode$ TwoWay=����� *DialogHeight+ ��  UserControl$ TwoWay=� � RowDefinitions �� $��28 ��� $��28 ��� $��* ��� $
��Auto �.+�]� $��28��
�����F�.+��� $��26��$
��Auto��
�����F�$��0q�$
��Left=�$��Center=�
�������$��16(�$|�False�� J� IsHitTestVisible$ False��$��False��$��Center=�$��2,0,0,0q����� *DialogTitle+ �� TargetNullValue$ 注意�� �� FallbackValue$ 注意��.���� -    ��Name$ BtnClose��$��26��$��26��$��Right=�$��Center=�$��16(�
������ 
�������$��0q����� *CloseCommand+y��� *IsShowButton+	 �� 	Converter#	 [  $ Visible��.��X�� $��1��
 �� Orientation$
 
Horizontal=�$��RightToLeft=�.+��� $��Center=�$��16(�
������ 
���P��$��0q�$	��5,0q����� *
LeftTime+$ 20��$ 20���� $��2��$��Disabled=�$
��Auto=�.���� $��5q�$��Center=�.+��� $��16(�$
��Wrap=�$��Center=�$��Center=����� *DialogMessage+$o i是否确认操作？是否确认操作？是否确认操作？是否确认操作？是否确认操作？��$ 是否确认操作？��y��� *
IsShowText+#	 [  $ Visible���� $��Center=�$��50��y��� *IsShowCustom+#	 [  $ 	Collapsed������ *CustomContent+���� *" CustomContentHorizontalAlignment+$ Center��$ OneWay=��� $��3��$
 
Horizontal=�$��RightToLeft=�$��Center=�$��0, 10q�y��� *IsShowButton+#	 [  $ Visible��.+��� -   $ 
BtnConfirm��$��16(�
�����F�
�������$��10, 0q�$	��120������ *ConfirmCommand+���� *DialogConfirmBtnText+$ 确认��$ 确认�� J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *ConfirmCommand+�� -   $ 	BtnCancel��$��16(�
�����F�
�������$��10, 0q�$	��120������ *CancelCommand+���� *DialogCancelBtnText+$ 取消��$ 取消��