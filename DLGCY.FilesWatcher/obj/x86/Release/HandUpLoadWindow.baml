   M S B A M L   `   `   `  ���� Q  MDLGCY.FilesWatcher, Version=1.0.8.20617, Culture=neutral, PublicKeyToken=null3clr-namespace:DotNet.UtilitiesDotNet.Utilities  )    #DLGCY.FilesWatcher.HandUpLoadWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1dotnetclr-namespace:DotNet.Utilities       � WindowStartupLocation$  CenterScreen=� � Title$ HandUpLoadWindow��$	��450��$	��890��.���� $��3q�$
��True��.���� $��10q� � 	Resources  �� 
TargetType �� Property �� Value��%.   '��       �� " ��.���  � ��$	 0,5q�.+��� $��Center=�$��80��$��0,50,0,0q�$	��430��.+��� $��50(�$
��Bold"�$��Center=�$��0,0,0,5q�人工扫码上传�� $
��Wrap=�$��20(�$��Center=����	#FFF70404���� *Configs.tips+�� $��Center=�$	��785��.+���  �� Orientation$ 
Horizontal=�$��Center=�$	��780��.+��� $��30(�拼板码：��  �� TextWrapping$
 Wrap=�$	��510��$��36(����� *Configs.HandBarCode+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=��� $��30(�-�� $
 Wrap=�$	��100��$��36(�${�False������ *Configs.HandBarCode_Son+$ TwoWay=�$	 PropertyChanged=��� $ 
Horizontal=�$��Center=�$��0,30,0,0q�.+��� $��取消��$	��100��$��30��$��	30,10,0,0q�$��18(����� *CancelCommand+�� $��上传��$	��100��$��30��$��	50,10,0,0q�$��18(����� *UpLoadCommand+�� $ 
Horizontal=��� $��36��$ 
Horizontal=�.+��� $
��Wrap=�$��	条码：��$��65��$��20(��� $
��Wrap=�$��65��$��20(�$
��Bold"����� *Configs.ProductBarcode+�� $	��100��$ 
Horizontal=�.+��� $
��Wrap=�$��MES结果：��$��40��$��20(��� $
��Wrap=�$��40��$��20(�$
��Bold"����� *Configs.MESErrorInfo+