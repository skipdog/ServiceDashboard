   M S B A M L   `   `   `  ����  ServiceDashboard"    Dashboard.View.ServiceWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  ServiceWindow��5   	   $	��300��6   $	��300��6,   
������ $��0q�6Q    � AllowsTransparency .  � WindowStyle$
 None=�5   '   .��5      ��    caption_small#��[  6   .��5   
   �  � ColumnDefinitions 5      �� 5	      $
��Auto �6#   �� 5
      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6     � 	Resources 5       �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5      �� $��Italic$�6    
���  ��$��Right=�6D   $	��0,1q�5      ��6*   �� $ ErrorContent�5      5      .+�5      �� $��_Server Name:��6   $��5q�6,    �� Target 67   �� 	 �� ElementName$	 tbServerName���� 5      -   5        
 ��Name$
 tbServerName��5       �� CharacterCasing$ Upper=�6,   $��5q�6D   $��1��6O   $	��100��6_   ��6k   �� $ MachineName� �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=��� 5!      $��1��6   $��2��6,   ��6@   �� $	 tbServerName��$% (Validation.Errors).CurrentItem��� 5$      $��2��6   $��5q�6!   $��	_Service:��6,    6@   �� $	 tbServiceName���� 5&      -   5        $
 tbServiceName��5&      $��5q�6-   $��2��68   $��1��6E   $	��100��6U   $ Upper=�6a   ��5'      �� $ ServiceName� . $ PropertyChanged=��� 5*      $��3��6   $��2��6,   ��6@   �� $	 tbServiceName��$% (Validation.Errors).CurrentItem�5-      5.      6   