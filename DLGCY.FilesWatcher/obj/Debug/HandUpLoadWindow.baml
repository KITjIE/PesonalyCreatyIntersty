   M S B A M L   `   `   `  ����Q  MDLGCY.FilesWatcher, Version=1.0.7.21160, Culture=neutral, PublicKeyToken=null3clr-namespace:DotNet.UtilitiesDotNet.Utilities  5   	   )    #DLGCY.FilesWatcher.HandUpLoadWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1dotnetclr-namespace:DotNet.Utilities     5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ HandUpLoadWindow��5   	   $	��450��6"   $	��890��6/   .��5
      �� $��3q�6   $
��True��6   .��5   
   �� $��10q�6    � 	Resources 5       �� 
TargetType �� Property �� Value��%X   '��       �� 5      " ��6   .�5      ��  � ��$	 0,5q�6/   6=   5      5      .+�5      �� $��Center=�6   $��80��66   $��0,50,0,0q�6B   $	��430��6T   .+�5      �� $��50(�6   $
��Bold"�6*   $��Center=�6<   $��0,0,0,5q�6Y   人工扫码上传6r   5      �� 5      $
��Wrap=�6   $��20(�6,   $��Center=�6X   ���	#FFF70404��6:   �� *Configs.tips+6u   �� 5      $��Center=�6   $	��785��66   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $	��780��6S   .+�5      �� $��30(�6    拼板码：64   �� 5       �� TextWrapping$
 Wrap=�6   $	��510��6�   $��36(�6�   ��62   �� *Configs.HandBarCode+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   �� 5      $��30(�6    -61   �� 5      $
 Wrap=�6   $	��100��6�   $��36(�6�   ${�False��6�   ��62   �� *Configs.HandBarCode_Son+$ TwoWay=�$	 PropertyChanged=�6�   5      �� 5       $ 
Horizontal=�6   $��Center=�66   $��0,30,0,0q�6S   .+�5!      �� $��取消��6   $	��100��6L   $��30��6X   $��	30,10,0,0q�6d   $��18(�6w   ��6*   �� *CancelCommand+6w   �� 5#      $��上传��6   $	��100��6L   $��30��6X   $��	50,10,0,0q�6d   $��18(�6x   ��6*   �� *UpLoadCommand+6x   5$      5&      �� 5'      $ 
Horizontal=�6   �� 5(      $��36��6   $ 
Horizontal=�6%   .+�5)      �� $
��Wrap=�6   $��	条码：��60   $��65��6;   $��20(�6G   �� 5*      $
��Wrap=�6   $��65��6X   $��20(�6d   $
��Bold"�6r   ��60   �� *Configs.ProductBarcode+6r   5+      �� 5,      $	��100��6   $ 
Horizontal=�6&   .+�5-      �� $
��Wrap=�6   $��MES结果：��60   $��40��6>   $��20(�6J   �� 5/      $
��Wrap=�6   $��40��6V   $��20(�6b   $
��Bold"�6p   ��60   �� *Configs.MESErrorInfo+6p   50      53      54      56      