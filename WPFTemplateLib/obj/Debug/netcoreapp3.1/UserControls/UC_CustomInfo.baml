   M S B A M L   `   `   `  ����I  EWPFTemplateLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:WPFTemplateLib.WpfConvertersWPFTemplateLib.WpfConverters  5      /    )WPFTemplateLib.UserControls.UC_CustomInfo   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      DwpfConverters*clr-namespace:WPFTemplateLib.WpfConverters     5        � 	Resources  5
      3   -WPFTemplateLib.WpfConverters.VisibleConverter   VisibleConverter5   /WPFTemplateLib.WpfConverters.CollapsedConverter  CollapsedConverter  	InfoTitle �� 
TargetType �� Property �� Value �� 	Converter�� 5   
   %�  &	        &	      &	 &     7   '�� �      5      6H     5      6L   �� 5      " ��6&   .�5      ��  � ��$ Stretch=�68   6J   �� 5       � ��$ Center=�66   6G   �� 5       �@��TextAlignment$ Center=�62   6C   �� 5       � ��$ 24(�6-   6:   �� 5       � �� �#8888886A   �� 5       � ��$ 0,0,0,20q�6+   6>   �� 5       � y� 6/   �� *IsShowTitle+8   6|   5      �� 5      " ��6   .�5      ��  � ��$	 150��6.   6<   �� 5       � ��$
 Auto��6*   69   �� 5       � ��$ Stretch=�68   6J   �� 5       � ��$ Center=�66   6G   �� 5       � ��$ Stretch=�6?   6Q   �� 5       � ��$ Center=�6=   6N   5       5!      .��5$      � $��20q�6    � RowDefinitions 5%   
   �� 5&      $
��Auto �6   �� 5'      $��* �6   �� 5(      $
��Auto �6    � ColumnDefinitions 5*   
   �� 5+      $��* �6   �� 5,      $��* �6   .+�5/   
   �� $��0��6   $��0��6!   #��[ 61   ��6T   �� *	TitleLeft+ �� FallbackValue$ 左侧标题���� 50   
   $��0��6   $��1��6!   #��[ 61   ��6T   �� *
TitleRight+$ 右侧标题��y�6�   �� *IsOnlyOneColumn+# [ �� 52   
   $��1��6   $��0��6&   ��66   �� *LeftContent+�� 53   
   $��1��6   $��1��6&   ��66   �� *RightContent+y�6W   �� *IsOnlyOneColumn+# [ �� 55   
   $��2��6   $��0��6!   $��2��61   #��[ 6E   ��6h   �� *
TextBottom+$ 底部文字��y�6�   �� *IsShowBottom+# [  56      57      