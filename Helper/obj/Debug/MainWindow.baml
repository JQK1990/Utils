   M S B A M L   `   `   `  ����
  Helper    Helper.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   U QControlLibrary, Version=2.0.0.0, Culture=neutral, PublicKeyToken=54478c5593697d2e!Customhttp://lanjunze.com/  5      -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��525��6)    � WindowStartupLocation$ CenterScreen=�6R   .��5      � -   5         �Name$
 Grid��5       � ColumnDefinitions 5	   
   �� 5
      $
��100* �6   �� 5      $
��100* �6   �� 5      $
��100* �6   �� 5      $
��100* �6   �� 5      $
��100* �6    � RowDefinitions 5   
   �� 5      $	��11* �6   �� 5      $	��67* �6   �� 5      $��2* �6   .+�5   
   �� -   5        $��	BtnImport��5   x   $��0��6   $��0��6   $��导入数据��6.   $��38��6=   $��4q�6I   $��Center=�6T      RoundButton#��[  6�   �� 5   
   -   5        $��	BtnExport��5   v   $��0��6   $��1��6   $��导出数据��6.   $��38��6=   $��4q�6I   $��Center=�6T   #��[  6�   �� 5   
   -   5        $��BtnClear��5   v   $��0��6   $��2��6   $��清除数据��6.   $��38��6=   $��4q�6I   $��Center=�6T   #��[  6�     ControlLibrary.DataGrid  5   
   -   5        $��DataGrid��5   �   $��5��6   $��0��6.   $��0,0,0,0q�6>   $��1��6O   $��Stretch=�6\   $��Stretch=�6�   &   System.Windows.Controls.DataGrid   AutoGenerateColumns .    RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      ��  �� Property � ��	 �� Value
	 �����63   
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
	 �����6;   5       P� 5!       �@ 
IsSelected . . �5"      ��  � ��
	 �����6;   �� 5#       � ��
	 �  ��6;   5$      5&      5(      �� 5)   
   -   5        $��LblDate��5)   �   $�� ��6   $��0��6   $��3��6(   $��Stretch=�68   $��0,0,0,0q�6W   $��Stretch=�6h   $��Center=�6�   5+      5,      