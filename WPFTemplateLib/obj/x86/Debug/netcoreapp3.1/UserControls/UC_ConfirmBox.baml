   M S B A M L   `   `   `  ����I  EWPFTemplateLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:WPFTemplateLib.UserControlsWPFTemplateLib.UserControls  5      K*clr-namespace:WPFTemplateLib.WpfConvertersWPFTemplateLib.WpfConverters  5      /    )WPFTemplateLib.UserControls.UC_ConfirmBox   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      BuserControls)clr-namespace:WPFTemplateLib.UserControls     5      DwpfConverters*clr-namespace:WPFTemplateLib.WpfConverters     5        � 	Resources  5
      3   -WPFTemplateLib.WpfConverters.VisibleConverter   VisibleConverter5   /WPFTemplateLib.WpfConverters.CollapsedConverter  CollapsedConverter��%:   &	        &	        5   
   6D     5   
   6H   5      .��5      � ���	#905F9EA0.+�5   
   � 
���������5      �� *DialogWidth+ �� RelativeSource ��  �� AncestorType UserControl �� Mode$ TwoWay=���5      �� *DialogHeight+ ��  UserControl$ TwoWay=� � RowDefinitions 5      �� 5      $��28 �6    6.   �� 5      $��28 �6    6.   �� 5      $��* �6    6-   �� 5      $
��Auto �6    60   .+�5      ]� $��28��6   
�����F�.+�5      �� $��26��6a   $
��Auto��6m   
�����F�$��0q�6�   $
��Left=�5      $��Center=�65   
�������$��16(�6c   $|�False��6q    J� IsHitTestVisible$ False��6�   $��False��6�   $��Center=�5      $��2,0,0,0q�6<   ��5      �� *DialogTitle+ �� TargetNullValue$ 注意�� �� FallbackValue$ 注意��.��5   N   5      �� 5      -   5         ��Name$ BtnClose��5      $��26��6�   $��26��6�   $��Right=�6�   $��Center=�6�   $��16(�6  
������ 
�������$��0q�5    E   ��5   +   �� *CloseCommand+y�6L   �� *IsShowButton+	 �� 	Converter#	 [  $ Visible��.��5    Z   X6]   5!      �� 5#      $��1��6   
 �� Orientation$
 
Horizontal=�6&   $��RightToLeft=�6?   .+�5$      �� $��Center=�6   $��16(�6|   
������ 
���P��$��0q�5%      $	��5,0q�6.   ��5$   <   �� *
LeftTime+$ 20��$ 20��5%   =   5&      �� 5(      $��2��6   $��Disabled=�6(   $
��Auto=�6Q   .��5)      �� $��5q�6   $��Center=�6(   .+�5*      �� $��16(�6    $
��Wrap=�6�   $��Center=�5+       $��Center=�6;   ��5*   .   �� *DialogMessage+$o i是否确认操作？是否确认操作？是否确认操作？是否确认操作？是否确认操作？��$ 是否确认操作？��y�5+   X   �� *
IsShowText+#	 [  $ Visible��5,      �� 5.      $��Center=�5/   �   $��50��6�   y�5.   %   �� *IsShowCustom+#	 [  $ 	Collapsed����6�   �� *CustomContent+��5/   %   �� *" CustomContentHorizontalAlignment+$ Center��$ OneWay=�50      51      52      �� 54      $��3��6   $
 
Horizontal=�6�   $��RightToLeft=�6�   $��Center=�6�   $��0, 10q�6�   y�6&   �� *IsShowButton+#	 [  $ Visible��.+�55      �� -   5        $ 
BtnConfirm��55      $��16(�6�   
�����F�
�������$��10, 0q�6�   $	��120��6�   ��6-   �� *ConfirmCommand+��6P   �� *DialogConfirmBtnText+$ 确认��$ 确认�� J� InputBindings 56      �� 57      
 �� Key$ Enter��6%    �� Command 61   �� *ConfirmCommand+59      �� 5:      -   5        $ 	BtnCancel��5:      $��16(�6�   
�����F�
�������$��10, 0q�6�   $	��120��6�   ��6,   �� *CancelCommand+��6N   �� *DialogCancelBtnText+$ 取消��$ 取消��6�   5;      5<      5=      5>      