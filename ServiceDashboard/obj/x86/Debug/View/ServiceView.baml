   M S B A M L   `   `   `  ����  ServiceDashboard     Dashboard.View.ServiceView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      y�5      ��   �� Path$  IsAdding�   VisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.��5	      ��   
data_entry#��[ 6   .��5
   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��_Server Name:��6   $��3q�6,    �� Target 67   ��  �� ElementName$ tbServerName���� 5      -   5         ��Name$ tbServerName��5      $��3q�6,    �� CharacterCasing$ Upper=�67   $��1��6O   $	��100��6_   ��6k   �� $  MachineName�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 	LostFocus=��� 5      $��2��6   $��3q�6/   $
��Auto��6:   ��6G   �� $ tbServerName��$%  (Validation.Errors).CurrentItem��� 5       $��1��6   $��3q�6!   $��	_Service:��6,    6@   �� $ tbServiceName���� 5"      -   5         ��Name$ cmbServiceName��5"      $��3q�6/   $��1��6:   $��1��6G   $	��100��6W   ��6c   �� $  ServiceNameList� �� SelectedItem 5#      �� $  ServiceName�	 . $
 PropertyChanged=��� 5%      $��1��6   $��2��6,   $��3q�6<   ��6G   �� $ cmbServiceName��$%  (Validation.Errors).CurrentItem��� 5'      $��_Description:��6   $��3q�6,   $��2��67    6D   �� $ tbDescription���� 5)      -   5        $ tbDescription��5)      $��2��6-   $��3q�6:   $��1��6E   $	��100��6U   ��6a   �� $  Description�	 . $
 PropertyChanged=��� 5,      $��2��6   $��2��6,   $��3q�6<   ��6G   �� $ tbDescription��$%  (Validation.Errors).CurrentItem��� 5.      $��_Alert Status:��6   $��3q�6-   $��3��68    6E   �� $ tbAlertStatus���� 50      -   5        $ tbAlertStatus��50      $ Upper=�6-   $��3q�6E   $��1��6P   $��3��6`   $	��100��6m   ��6y   �� $  AlertStatus�$
 PropertyChanged=�53      54      55      