   M S B A M L   `   `   `  ����  ServiceDashboard    Dashboard.View.QueryView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      y�5      ��   �� Path$  IsAdding�   VisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.��5	      ��   
data_entry#��[ 6   .��5
   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��_Description:��6   $��3q�6,    �� Target 67   ��  �� ElementName$ tbDescription���� 5      -   5         ��Name$ tbDescription��5      $��3q�6-    �� CharacterCasing$ Upper=�68   $��1��6P   $	��100��6`   ��6l   �� $  Description�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5      $��2��6   $��3q�6/   $
��Auto��6:   ��6G   �� $ tbDescription��$%  (Validation.Errors).CurrentItem��� 5       $��1��6   $��3q�6!   $��_Query:��6,    6>   �� $ tbQuery���� 5"      -   5        $ tbQuery��5"      $��3q�6'   $ Normal=�62   $��1��6K   $��1��6X   $	��100��6h   ��6t   �� $  Query�	 . $
 PropertyChanged=��� 5%      $��1��6   $��2��6,   $��3q�6<   ��6G   �� $ tbQuery��$%  (Validation.Errors).CurrentItem��� 5'      $��_ConnectionString:��6   $��3q�61   $��2��6<    6I   �� $ tbConnectionString���� 5)      -   5        $ tbConnectionString��5)      $��2��62   $��3q�6?   $��1��6J   $	��100��6Z   ��6f   �� $  ConnectionString�	 . $
 PropertyChanged=��� 5,      $��2��6   $��2��6,   $��3q�6<   ��6G   �� $ tbConnectionString��$%  (Validation.Errors).CurrentItem��� 5.      $��_QueryInterval:��6   $��3q�6.   $��3��69    6F   �� $ tbQueryInterval���� 50      -   5        $ tbQueryInterval��50      $ Normal=�6/   $��3q�6H   $��1��6S   $��3��6c   $	��100��6p   ��6|   �� $  QueryInterval�	 . $
 PropertyChanged=��� 53      $��3��6   $��2��6,   $��3q�6<   ��6G   �� $ tbQueryInterval��$%  (Validation.Errors).CurrentItem�55      56      57      