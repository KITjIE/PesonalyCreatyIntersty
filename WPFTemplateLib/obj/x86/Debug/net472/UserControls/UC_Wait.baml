   M S B A M L   `   `   `  ����I  EWPFTemplateLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:WPFTemplateLib.UserControlsWPFTemplateLib.UserControls  5      )    #WPFTemplateLib.UserControls.UC_Wait   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:WPFTemplateLib.UserControls     5      -   5        $��Stretch=�5	   7   .��5
      �   � ColumnDefinitions  5   
   �� 5      �� 5      �� 5      �� 5       � 	Resources 5   
    �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType �� Path   DivideConverter �� 	Converter	 �� ConverterParameter��%
  '��       7   �� 5      " ��6   .�5      ��  � �� 6*   ��  ��  Grid$ ActualWidth�8   $	 2��6�   �� 5       � �� 6+   ��  �� *Self+$ Width�6p   �� 5       �@��CornerRadius$	 100��61   6?   5      5      .+�5   
   � $��0��6   .+�5       �� 
����� �
 � LayoutTransform
 5!      �� 5"      -   5        ST1��5"   4   5$      5%      � 5&   
   $��1��6   .+�5'      �� 
�������
 5(      �� 5)      -   5        ST2��5)   4   5+      5,      � 5-   
   $��2��6   .+�5.      �� 
��� ��
 5/      �� 50      -   5        ST3��50   4   52      53      � 54   
   $��3��6   .+�55      �� 
����i��
 56      �� 57      -   5        ST4��57   4   59      5:      5;      5<      