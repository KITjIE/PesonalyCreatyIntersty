   M S B A M L   `   `   `  ����  DLGCY.FilesWatcher"    DLGCY.FilesWatcher.MsgWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  FullScreenPopupWindow��5   	    � WindowStyle$
 None=�6'    � WindowState$ 	Maximized=�6:    � Topmost$
 True��6R    � 	Resources 5         BlinkStoryboard g� RepeatBehavior �� TargetProperty g� AutoReverse8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime	 �� Value��%�   &	        �� 5   
   $ Forever��6-   .��5      �� $0 *(Panel.Background).(SolidColorBrush.Color)�6+   $
 True��6r   .g�5        $ 0��6&   $		 Red��62     5	      $ 0:0:0.5��6&   $	 Yellow��68   5
      5      5      .��5      � .+�5   
   � 
���  ��
 � RowDefinitions
 5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      � $��0��6   .+�5      �� $��Center=�6   $��Center=�6:   .+�5      �� $��MES上传错误：��6   $��Center=�6,   $��Center=�6I   $	��100(�6d   $
��Wrap=�6s   5      �� 5      $��Center=�6<   $��Center=�6Y   $	��100(�6t   $
��Wrap=�6�   ��6   �� *ProductBarcode+5      �� 5      $��Center=�68   $��Center=�6U   $	��100(�6p   $
��Wrap=�6   ��6   �� *
LogContent+5      5      5      � 5!      $��1��6   $��Center=�6    .+�5"      �� -   5        $��关闭��5"      $	��200��6@   $	��100��6L   $��48(�6Y   5#      5$      5%      5&      