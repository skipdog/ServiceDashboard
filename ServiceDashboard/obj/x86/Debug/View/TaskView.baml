   M S B A M L   `   `   `  ����  ServiceDashboard    Dashboard.View.TaskView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      y�5      ��   �� Path$  IsAdding�   VisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.��5	      ��   
data_entry#��[ 6   .��5
   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��_Server Name:��6   $��3q�6,    �� Target 67   ��  �� ElementName$ tbServerName���� 5!      -   5         ��Name$ tbServerName��5!       �� CharacterCasing$ Upper=�6,   $��3q�6D   $��1��6O   $	��100��6_   ��6k   �� $  MachineName�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 	LostFocus=��� 5$      $��2��6   $��3q�6/   ��6:   �� $ tbServerName��$%  (Validation.Errors).CurrentItem��� 5&      $��_User Name:��6   $��3q�6*   $��1��65    6B   �� $ 
tbUserName���� 5(      -   5        $ 
tbUserName��5(      $��3q�6*   $��1��65   $��1��6B   $	��100��6R   ��6^   �� $  UserName�$
 PropertyChanged=��� 5+      $��
_Password:��6   $��2��6)   $��3q�66    6A   �� $ 
tbPassword���� 5-      -   5        $ 
tbPassword��5-      $��3q�6*   $��2��65   $��1��6B   $	��100��6R   ��6^   �� $  Password�	 . $
 PropertyChanged=��� 50      $��2��6   $��2��6,   $��3q�6<   ��6G   �� $ 
tbPassword��$%  (Validation.Errors).CurrentItem��� 52      $��3��6   $��3q�6!   $��_Task:��6,    6=   �� $ 
tbTaskName���� 54      -   5         ��Name$ cmbTaskName��54      $��3q�6,   $��3��67   $��1��6D   $	��100��6T   ��6`   �� $  TaskNameList� �� SelectedItem 55      �� $  TaskName�	 . $
 PropertyChanged=��� 57      $��3��6   $��2��6,   $��3q�6<   ��6G   �� $ cmbTaskName��$%  (Validation.Errors).CurrentItem��� 59      $��_Description:��6   $��4��6,   $��3q�69    6D   �� $ tbDescription���� 5;      -   5        $ tbDescription��5;      $��3q�6-   $��4��68   $��1��6E   $	��100��6U   ��6a   �� $  Description�	 . $
 PropertyChanged=��� 5>      $��4��6   $��2��6,   $��3q�6<   ��6G   �� $ tbDescription��$%  (Validation.Errors).CurrentItem��� 5A      $��_Alert Status:��6   $��5��6-   $��3q�6:    6E   �� $ tbAlertStatus���� 5C      -   5        $ tbAlertStatus��5C      $ Upper=�6-   $��3q�6E   $��1��6P   $��5��6`   $	��100��6m   ��6y   �� $  AlertStatus�$
 PropertyChanged=�5F      5G      5H      